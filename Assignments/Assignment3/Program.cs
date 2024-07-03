using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            Accounts acc = new Accounts(90594, "ravi", "Savings", 2000);
            acc.UpdateBalance('d', 600);
            acc.ShowData();
            acc.UpdateBalance('w', 300);
            acc.ShowData();

            Student student = new Student(1, "teja", "10th", "1st", "Social");
            student.GetMarks();
            student.DisplayResult();
            student.DisplayData();

            saledetails sale = new saledetails(1001, 2001, 15.5, 8, DateTime.Now);
            sale.Sales();
            sale.ShowData();

            Customer customer = new Customer(201, "ravi teja", 20, "Guntur");
            customer.DisplayCustomer();



        }
    }
}