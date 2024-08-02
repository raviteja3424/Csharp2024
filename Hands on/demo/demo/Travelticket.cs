using System;

namespace ConcessionApp
{
    class Travelticket
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter your age:");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Invalid age entered. Please enter a valid age.");
                return;
            }

            // Create instance of ConcessionCalculator
            ConcessionLibrary.ConcessionCalculator calculator = new ConcessionLibrary.ConcessionCalculator();

            // Calculate concession based on age
            string concessionMessage = calculator.CalculateConcession(age);

            // Display results
            Console.WriteLine($"Hello {name}, {concessionMessage}");

            Console.ReadLine(); // To keep console window open
        }
    }
}
