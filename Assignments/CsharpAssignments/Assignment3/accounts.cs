using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment3
{

    class Accounts
    {
        public int AccountNo { get; set; }
        public string CustomerName { get; set; }
        public string AccountType { get; set; }
        public char TransactionType { get; set; }
        public int Amount { get; set; }
        public int Balance { get; set; }
        public Accounts(int accountNo, string customerName, string accountType, int balance)
        {
            AccountNo = accountNo;
            CustomerName = customerName;
            AccountType = accountType;
            Balance = balance;
        }
        public void UpdateBalance(char transactionType, int amount)
        {
            if (transactionType == 'd')
            {
                Credit(amount);
            }
            else if (transactionType == 'w')
            {
                Debit(amount);
            }
        }
        private void Credit(int amount)
        {
            Balance += amount;

        }
        private void Debit(int amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                Console.WriteLine("Insufficient funds");
            }
        }
        public void ShowData()
        {
            Console.WriteLine($"Account No: {AccountNo}");
            Console.WriteLine($"Customer Name: {CustomerName}");
            Console.WriteLine($"Account Type: {AccountType}");
            Console.WriteLine($"Balance: {Balance}");
        }
    }

}