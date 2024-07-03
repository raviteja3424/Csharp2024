using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3
{
    class saledetails
    {
        public int SalesNo { get; set; }
        public int ProductNo { get; set; }
        public double Price { get; set; }
        public DateTime DateOfSale { get; set; }
        public int Qty { get; set; }
        public double TotalAmount { get; set; }
        public saledetails(int salesNo, int productNo, double price, int qty, DateTime dateOfSale)
        {
            SalesNo = salesNo;
            ProductNo = productNo;
            Price = price;
            Qty = qty;
            DateOfSale = dateOfSale;
        }
        public void Sales()
        {
            TotalAmount = Qty * Price;
        }
        public void ShowData()
        {
            Console.WriteLine($"Sales No: {SalesNo}");
            Console.WriteLine($"Product No: {ProductNo}");
            Console.WriteLine($"Price: {Price}");
            Console.WriteLine($"Quantity: {Qty}");
            Console.WriteLine($"Date of Sale: {DateOfSale.ToShortDateString()}");
            Console.WriteLine($"Total Amount: {TotalAmount}");
        }
    }
}