using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_App
{
    internal class CurrentAccount : IAccount
    {
        public static double Balance=0.0;
        public void Deposit(double amount)
        {
            Balance += amount;
        }
        public void Withdraw(double amount) 
        {
            if (Balance > amount)
            {
                Balance = Balance - amount;
                Console.WriteLine("withdraw successfully");
            }
            else
            {
                Console.WriteLine("Balnce is low");
            }
        }
        public double Getbalance()
        {
            return Balance;
        }


    }
}
