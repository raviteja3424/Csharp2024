using System;

public delegate int CalculatorOperation(int a, int b);

class Calculator
{
    static void Main()
    {

        (string name, CalculatorOperation operation)[] 
         operations = new (string, CalculatorOperation)[]
        {
            ("Addition", (a, b) => a + b),
            ("Subtraction", (a, b) => a - b),
            ("Multiplication", (a, b) => a * b)
        };

        Console.WriteLine("Enter the first number:");
        int num1 = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter the second number:");
        int num2 = Convert.ToInt32(Console.ReadLine());

        foreach (var op in operations)
        {
            int result = PerformOperation(op.operation, num1, num2);
            Console.WriteLine($"{op.name} result: {result}");
            Console.ReadLine();
        }
    }

    static int PerformOperation(CalculatorOperation operation, int a, int b)
    {
        return operation(a, b);
    }
}
