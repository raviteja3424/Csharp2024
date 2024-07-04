using System;

public interface IStudent
{
    int StudentId { get; set; }
    string Name { get; set; }
    int Fees { get; set; }
    void ShowDetails();
}

public class Dayscholar : IStudent
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public int Fees { get; set; }

    public Dayscholar(int studentId, string name, int fees)
    {
        StudentId = studentId;
        Name = name;
        Fees = fees;
    }

    public void ShowDetails()
    {
        Console.WriteLine($"Student ID: {StudentId}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Fees: {Fees}");
        Console.WriteLine("Type: Dayscholar");
    }
}

public class Resident : IStudent
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public int Fees { get; set; }

    public Resident(int studentId, string name, int fees)
    {
        StudentId = studentId;
        Name = name;
        Fees = fees;
    }

    public void ShowDetails()
    {
        Console.WriteLine($"Student ID: {StudentId}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Fees: {Fees}");
        Console.WriteLine("Type: Resident");
        Console.Read();
    }
}

class Program
{
    static void Main()
    {
        IStudent dayscholar = new Dayscholar(101, "ravi", 20000);
        IStudent resident = new Resident(201, "teja", 30000);

        Console.WriteLine("Dayscholar Details:");
        dayscholar.ShowDetails();
        

        Console.WriteLine("Resident Details:");
        resident.ShowDetails();
    }
} 
