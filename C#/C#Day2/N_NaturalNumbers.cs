using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_2_Assigments
{
    public partial class N_NaturalNumbers
    {
        public N_NaturalNumbers()
        {
            Console.WriteLine("Enter the Range of numbers");
            string RangeN = "20";
            if (int.TryParse(RangeN, out int RangeOfNatural))
            {
                for (int i = 0; i <= RangeOfNatural; i++)
                {
                    Console.Write(i + ",");
                }
            }
            else
            {
                Console.WriteLine("Invalid Number");
            }
        }
    }
}
