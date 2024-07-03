using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment4
{
    public class Stringutility
    {
        public static int CountOccurrences(string input, char target)
        {
            int count = 0;
            foreach (char c in input)
            {
                if (c == target)
                    count++;
            }
            return count;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter a string:");
            string inputString = Console.ReadLine();

            Console.WriteLine("Enter the character to count:");
            char targetChar = Console.ReadKey().KeyChar;
            Console.WriteLine();


            int count = Stringutility.CountOccurrences(inputString, targetChar);
            Console.WriteLine("Number of occurrences of " + targetChar + " in " + inputString + ": " + count);
            Console.ReadLine();

        }
    }
}