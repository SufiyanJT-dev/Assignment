

namespace Day_2_Assigments
{
    public class PrimeNumberBelow100
    {
        public PrimeNumberBelow100()
        {
            int number = 100;
            for (int i = 0; i <= number; i++)
            {
                if (CheckPrime(i))
                {
                    Console.Write(i + " ");
                }

            }
            
        }

        public bool CheckPrime(int NumberPrime)
        {
            if (NumberPrime <= 1) return false;
            if (NumberPrime == 2) return true;
            if (NumberPrime %2== 0) return false;
            for (int i = 3; i <= Math.Sqrt(NumberPrime); i += 2)
            {
                if (NumberPrime % i == 0)
                {
                    return false; 
                }
            }
            return true; 
        }
                    
        }
    }


