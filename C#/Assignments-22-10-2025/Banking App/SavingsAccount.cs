using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Banking_App
{
    public  class SavingsAccount: IAccount
    {
        public static double Balance = 0.0;
        public void Deposit(double amount)
        {
            Balance += amount;
        }
        public void Withdraw(double amount)
        {
            if (Balance > amount)
            {
                Balance -= amount;
                Console.WriteLine("successfully");
            }
            else
            {
                Console.WriteLine("low Balance");
            }
        }
        public double Getbalance()
        {
            return Balance;
        }
    }
}
