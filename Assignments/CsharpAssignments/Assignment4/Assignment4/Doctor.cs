using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment4
{
    class Doctor
    {
        private int RegnNo { get; set; }
        private string Name { get; set; }
        private decimal FeesCharged { get; set; }

        public Doctor(int regnNo, string name, decimal feesCharged)
        {
            RegnNo = regnNo;
            Name = name;
            FeesCharged = feesCharged;
        }

        public void DisplayDetails()
        {
            Console.WriteLine("Registration Number: " + RegnNo);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Fees Charged: " + FeesCharged);
            Console.ReadLine();
        }
    }

    public class doctor
    {
        static void Main()
        {
            Console.WriteLine("Enter Registration Number:");
            int regnNo = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Doctor's Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Fees Charged:");
            decimal feesCharged = decimal.Parse(Console.ReadLine());

            Doctor doctor = new Doctor(regnNo, name, feesCharged);
            doctor.DisplayDetails();
        }
    }
}