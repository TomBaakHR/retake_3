
class hr1058829
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
       
    }

    public static void Q3(ExamContext db)
    {
        
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