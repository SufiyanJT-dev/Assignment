using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assignments_22_10_2025.CustomExtention;

namespace Assignments_22_10_2025
{
    public class Extensions
    {
        public Extensions()
        {
            string k;
            do {
                Console.WriteLine("Enter A string with first small letter");
                 k = Console.ReadLine();
                if (string.IsNullOrEmpty(k))
                {
                    k = "default";
                }
            }
            while (k.IsUpper());
            Console.WriteLine(k.ToTitleCase());
            List<int> l = new List<int>();
            Console.WriteLine("Enter 10 values into the list");
            for (int i = 0; i < 10; i++) { 
                string numberstring =Console.ReadLine();
                if(int.TryParse(numberstring,out int  number))
                {
                    l.Add(number);

                }
            }
            Console.WriteLine("the average value for this list without including zero is "+l.AverageExceptZero());
        }
        
    }
}
