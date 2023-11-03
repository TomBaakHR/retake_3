
using System.Data;

class Solution
{
    
    public static void Q1(ExamContext db, string Name)
    {
        var customers = db.Customers.Where(cs => cs.FirstName.Contains(Name) || cs.LastName.Contains(Name)).ToList();
        
        foreach (var item in customers)
        {
            Console.WriteLine($"ID: {item.ID}, Firstname: {item.FirstName}, Lastname: {item.LastName}");
        }
    }

    public static void Q2(ExamContext db, int ProductID)
    {
       var productInfo = from prod in db.Products
                        join comp in db.Companies on prod.CompanyID equals comp.ID
                        where prod.ID == ProductID
                         select new {
                            companyName = comp.Name,
                            name = prod.Name,
                            price = prod.Price
                         };  
        
        var product = productInfo.FirstOrDefault();

        Console.WriteLine($"Company: {product.companyName}, Prodcut: {product.name}, Price: {product.price}");
    }

    public static void Q3(ExamContext db)
    {
        
        var countries = db.Companies
                        .GroupBy(c => c.Country == null ? "NULL" : c.Country.ToUpper())
                        .Select(group => new {
                            Country = group.Key,
                            Total = group.Count(),
                            CompanyInfo = group.Select(group => new {
                                group.ID,
                                group.Name
                            })
                        }).ToList();

        foreach (var item in countries)
        {
            Console.WriteLine($"{item.Country}, {item.Total}");

            foreach (var info in item.CompanyInfo)
            {
                Console.WriteLine($"{info.ID}, {info.Name}");
            }

        }

    }

    public static void Q4(ExamContext db, int OrderID)
    {
       
    }

    public static void Q5(ExamContext db)
    {
 
    }

    public static void Q6(ExamContext db)
    {

    }
    public static void Q7(ExamContext db, string Country, decimal fraction)
    {


    }
    public static void Q8(ExamContext db, int OrderID)
    {
    
    }

}