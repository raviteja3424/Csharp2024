using System;

namespace swapping
{
    class Program
    {
        public static void Main(string[] args)
        {
            int num1, num2, temp;

            Console.WriteLine("enter first number: ");
            num1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter second number:");
            num2 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Before swapping:");
            Console.WriteLine("first number:" + num1);
            Console.WriteLine("second number:" + num2);

            temp = num1;
            num1 = num2;
            num2 = temp;

            Console.WriteLine("after swapping:");
            Console.WriteLine("first number:" + num1);
            Console.WriteLine("second number:" + num2);
            Console.ReadLine();

        }
    }
}