using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_App
{
    public  class PaymentProcessor
    {
        private  IPaymentService paymantService;
        public PaymentProcessor(IPaymentService paymantService)
        {
            this.paymantService = paymantService;
        }
        public void  ProcessPayment(double amount)
        {
            paymantService.MakePayment(amount);
        }

    }
}
