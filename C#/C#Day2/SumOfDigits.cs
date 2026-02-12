using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day_2_Assigments
{
    public class SumOfDigits
    {
        public SumOfDigits(int Number)
        {
            int sum = 0;
            while (Number > 0)
            {
                sum = sum + (Number % 10);
                Number /= 10;


            }
            Console.WriteLine(sum);
        }

    }
}


