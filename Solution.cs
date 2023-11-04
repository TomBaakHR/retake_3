
using System.Data;

class Solution
{
    
    public static void Q1(ExamContext db, string Name)
    {
        var customers = db.Customers.Where(cs => cs.FirstName.Contains(Name) || cs.LastName.Contains(Name)).ToList();
        
        foreach (var item in customers)
        {
            Console.WriteLine($"ID: {item.ID}, FirstName: {item.FirstName}, LastName: {item.LastName}");
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
        
        var result = from c in db.Companies
                        group c by c.Country == null ? "NULL" :  c.Country.ToUpper();

        foreach(var group in result){
            Console.WriteLine($"{group.Key}, {group.Count()}");
            foreach (var c in group)
            {
                System.Console.WriteLine($"{c.ID}, {c.Name}, {c.Country}");
            }
        }

    }

    public static void Q4(ExamContext db, int OrderID)
    {
        var order = (from o in db.Orders
                    join c in db.Customers on o.CustomerID equals c.ID
                    where o.ID == OrderID
                    select new {
                        ID = o.ID,
                        CustomerName = c.FirstName + " " + c.LastName,
                        Date = o.dateTime
                    }).FirstOrDefault();

        var shoppingCartList = (from sc in db.ShoppingCarts
                                join p in db.Products on sc.ProductID equals p.ID
                                where sc.OrderID == OrderID
                                select new {
                                    ProductName = p.Name,
                                    Quantity = sc.Quantity,
                                    UnitPrice = p.Price,
                                    CartPrice = p.Price * sc.Quantity
                                }).ToList();
        
        Console.WriteLine($"Customer : {order.CustomerName}\nOrderID: {order.ID}");

        decimal total = 0;

        foreach (var item in shoppingCartList)
        {
            Console.WriteLine($"{item.ProductName}, {item.UnitPrice} x {item.Quantity} = {item.CartPrice}");
            total += item.CartPrice;
        }

        Console.WriteLine($"Grand total: {total}");
    }

    public static void Q5(ExamContext db)
    {
        var sold_products = (from sc in db.ShoppingCarts
                            group sc by sc.ProductID into products
                            select products.Key).ToList();

        var unsold_products = db.Products.Where(p => !sold_products.Contains(p.ID)).ToList();

        foreach (var item in unsold_products)
        {
             Console.WriteLine($"Product:{item.ID},{item.Name},{item.Price}");
        }
    }

    public static void Q6(ExamContext db)
    {
    }

    public static void Q7(ExamContext db, string Country, decimal fraction)
    {
        List<int> companiesIDS = db.Companies
            .Where(c => (c.Country == null ? "null" : c.Country.ToUpper()) == Country.ToUpper())
            .Select(c => c.ID).ToList();

        List<Product> productsToUpdate = db.Products.Where(p => companiesIDS.Contains(p.CompanyID)).ToList();

        foreach (var p in productsToUpdate)
        {
            p.Price *= (1 + fraction);
        }

        int recordsChanged = db.SaveChanges();
        System.Console.WriteLine($"{recordsChanged} records got updated");
    }
    public static void Q8(ExamContext db, int OrderID)
    {
        Order? orderToDelete = db.Orders.Where(o => o.ID == OrderID).FirstOrDefault();

        if(orderToDelete == null){
            System.Console.WriteLine($"Order {OrderID} not found");
        }else{
            db.Orders.Remove(orderToDelete);
            int recordsChanged = db.SaveChanges();
            System.Console.WriteLine($"{recordsChanged} records got deleted");
            System.Console.WriteLine($"Order {OrderID} from Customer {orderToDelete.CustomerID} at {orderToDelete.dateTime} deleted");
        }

    }

}