using System;
using System.Collections.Generic;

namespace Day_2_Assigments
{
    public class FibonacciSeries
    {
       
      

        public FibonacciSeries()
        {
            Console.WriteLine("Enter the Range Of series");
            string stringRange = Console.ReadLine();

            if (int.TryParse(stringRange, out int numberRange))
            {
                
                Console.WriteLine("Fibonacci Series:");
                for (int i = 0; i < numberRange; i++)
                {
                    Console.Write(MemoFebSeries(i) + " ");
                }
                Console.WriteLine(); 
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        
        public int MemoFebSeries(int n)
        {
            
            if (n <= 1)
            {
                return n;
            }

            
          

            
            int result = MemoFebSeries(n - 1) + MemoFebSeries(n - 2);

            
           

            return result;
        }

       
        public static void Main(string[] args)
        {
            new FibonacciSeries();
        }
        
    }
}
