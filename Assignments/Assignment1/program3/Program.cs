using System;

namespace checksign
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("enter a number: ");
            int num = Convert.ToInt32(Console.ReadLine());


            if (num > 0)
            {
                Console.WriteLine("the number is positive.");
            }
            else if (num < 0)
            {
                Console.WriteLine("the number is negative.");
            }
            else
            {
                Console.WriteLine("the number is zero.");
            }
            Console.ReadLine();

        }
    }
}