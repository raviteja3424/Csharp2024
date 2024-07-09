
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcessionCaculator
{
    public class ConcessionCalculator
    {
        private const double TotalFare = 500;

        public void CalculateConcession(string name, int age)
        {
            if (age <= 5)
            {
                Console.WriteLine($"{name} - Little Champs - Free Ticket");

            }
            else if (age > 60)
            {
                double concession = 0.5 * TotalFare;
                double discountedFare = TotalFare - concession;
                Console.WriteLine($"{name} - Senior Citizen - Discounted Fare: {discountedFare}");

            }
            else
            {
                Console.WriteLine($"{name} - Ticket Booked - Fare: {TotalFare}");
            }
        }
    }
}
