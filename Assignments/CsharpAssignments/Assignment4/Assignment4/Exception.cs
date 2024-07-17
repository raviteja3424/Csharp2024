using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment4
{

    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException() : base("Insufficient balance in the account.")
        {
        }
    }

    public class BankAccount
    {
        private decimal balance;

        public BankAccount(decimal initialBalance)
        {
            balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > balance)
            {
                throw new InsufficientBalanceException();
            }
            balance -= amount;
        }

        public decimal GetBalance()
        {
            return balance;
        }
    }

    public class MainProgram
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter initial balance:");
            decimal initialBalance = decimal.Parse(Console.ReadLine());

            BankAccount account = new BankAccount(initialBalance);

            try
            {
                Console.WriteLine("Enter amount to withdraw:");
                decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                account.Withdraw(withdrawAmount);
                Console.WriteLine("Withdrawal successful!");
                Console.Read();
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
        }
    }


}