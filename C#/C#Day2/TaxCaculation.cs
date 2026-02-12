using System.Transactions;

namespace Day_2_Assigments
{
    public class TaxCaculation

    {
        public TaxCaculation()
        {
            Console.WriteLine("Enter YOur Salary ");
            string StringSalary=Console.ReadLine();
            if(int.TryParse(StringSalary,out int salary))
            {
                TaxCaculationFun(salary);
            }        
        }
        public void TaxCaculationFun(int n) {
            double tax = 1.0;
            if (n <= 10000)
            {
                tax = (n * (5.0 / 100.0));
                Console.WriteLine(" Your Current Income = " + n + "Tax is =" + tax);
            }
            else if (n > 10000.0 && n <= 15000.0)
            {
                tax = (n * (7.5 / 100));
                Console.WriteLine(" Your Current Income = " + n + "Tax is =" + tax);
            }
            else if (n > 15000 && n <= 20000)
            {
                tax = (n * (10.0 / 100.0));
                Console.WriteLine(" Your Current Income = " + n + "Tax is =" + tax);
            }
            else if (n > 20000 && n <= 25000)
            {
                tax = (n * (12.5 / 100.0));
                Console.WriteLine(" Your Current Income = " + n + "Tax is =" + tax);
            }
            else
            {
                tax = n * (15.0 / 100.0);
                Console.WriteLine(" Your Current Income = " + n + "  Tax is = " + tax);
            }
        }
    }
}
