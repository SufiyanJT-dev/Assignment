using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_App
{

    public interface IAccount
    {

        void Deposit(double amount);
        void Withdraw(double amount);
        double Getbalance();
    }
    public class Account : IAccount
    {
        public static double TotalBalance { get; set; }

        public void Deposit(double amount)
        {
            TotalBalance = TotalBalance + amount;
        }
        public double Getbalance()
        {
            Console.WriteLine(TotalBalance);
            return TotalBalance;
        }
        public void Withdraw(double amount)
        {
            if (TotalBalance > amount)
            {
                TotalBalance = TotalBalance - amount;
                Console.WriteLine("withdraw successfully");
            }
            else
            {
                Console.WriteLine("Balnce is low");
            }
        }
    }

}
