using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_App
{
    public  interface  IPaymentService 
    {
        void MakePayment(double amount);
    }
    public class CreditCardPayment : IPaymentService
    {
        public void MakePayment(double amount)
        {
            Console.WriteLine("AMMOUNT {0}  DONE BY Credit Card Payment",amount);

        }
      
    }
    public class UPIPayment : IPaymentService
    {
        public void MakePayment(double amount)
        {
            Console.WriteLine("AMMOUNT {0}  DONE BY UPI Payment", amount);

        }

    }
    public class NetBankingPayment: IPaymentService
    {
        public void MakePayment(double amount)
        {
            Console.WriteLine("AMMOUNT {0}  DONE BY NetBanking Payment",amount);

        }
    }
}
