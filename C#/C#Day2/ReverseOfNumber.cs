namespace Day_2_Assigments
{


    public class ReverseOfNumber
    {
        public ReverseOfNumber(int NumberToReverse) {
            int ReversedNumber=0;
            while (NumberToReverse > 0)
            {

                ReversedNumber = (ReversedNumber*10)+NumberToReverse % 10;
                NumberToReverse /= 10;
            }
            Console.WriteLine(ReversedNumber);
        }

    }
}


