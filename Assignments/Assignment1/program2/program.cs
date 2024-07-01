using System;

namespace arthimeticoperations
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("enter first integer: ");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter second integer:");
            int num2 = Convert.ToInt32(Console.ReadLine());

            int sum = num1 + num2;
            int difference = num1 - num2;
            int product = num1 * num2;
            int division = num1 / num2;

            Console.WriteLine("sum:" + sum);
            Console.WriteLine("difference:" + difference);
            Console.WriteLine("product:" + product);
            Console.WriteLine("division:" + division);
            Console.ReadLine();
        }
    }
}