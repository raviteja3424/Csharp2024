using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment4
{
    class Name
    {
        public class NameProcessor
        {
            private string FirstName { get; }
            private string LastName { get; }

            public NameProcessor(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            public static void Display(NameProcessor nameProcessor)
            {
                Console.WriteLine(nameProcessor.FirstName.ToUpper());
                Console.WriteLine(nameProcessor.LastName.ToUpper());
                Console.Read();
            }
        }

        public class Program
        {
            public static void Main(string[] args)
            {
                Console.WriteLine("Enter First Name:");
                string firstName = Console.ReadLine();

                Console.WriteLine("Enter Last Name:");
                string lastName = Console.ReadLine();

                NameProcessor processor = new NameProcessor(firstName, lastName);
                NameProcessor.Display(processor);


            }
        }
    }

}
