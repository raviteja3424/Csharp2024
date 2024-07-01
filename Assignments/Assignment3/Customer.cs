using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3
{

    class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public Customer()
        { }
        public Customer(int customerId, string name, int age, string city)
        {
            CustomerId = customerId;
            Name = name;
            Age = age;
            Phone = Phone;
            City = city;
        }
        public void DisplayCustomer()
        {
            Console.WriteLine($"Customer Id: {CustomerId}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Phone: {Phone}");
            Console.WriteLine($"City: {City}");

        }
        ~Customer()
        {
            Console.WriteLine("Destructor called for Customer");
        }

    }

}