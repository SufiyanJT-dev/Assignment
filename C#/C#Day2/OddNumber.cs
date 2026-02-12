namespace Day_2_Assigments
{
    public class OddNumber
    {                                   
        public OddNumber()
        {
            int N = 1;
            while (N < 50)
            {
                if (N%2 != 0)
                {
                    Console.Write(N+" ");
                }
                N++;
            }
        }
    }

}
