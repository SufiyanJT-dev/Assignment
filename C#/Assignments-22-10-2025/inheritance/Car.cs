using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inheritanceAssigment
{
    public class Car:Vehicle
    {
        public new void ShowType()
        {
            Console.WriteLine("This is a car");
        }
    }
}
