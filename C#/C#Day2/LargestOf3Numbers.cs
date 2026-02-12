namespace Day_2_Assigments
{

    public partial class LargestOf3Numbers
    {
        public LargestOf3Numbers()
        {
            Console.WriteLine("Enter 3 Number to find Largest Number");
            String StringNumberOne = Console.ReadLine(), StringNumberTwo = Console.ReadLine(), StringNumberThree = Console.ReadLine();
            if (int.TryParse(StringNumberOne, out int NumberOne) && int.TryParse(StringNumberTwo, out int NumberTwo) && int.TryParse(StringNumberThree, out int NumberThree))
            {
                if (NumberOne > NumberTwo && NumberOne > NumberThree)
                {
                    Console.WriteLine("Largest Number is" + NumberOne);
                }
                else if (NumberTwo > NumberOne && NumberTwo > NumberThree)
                {
                    Console.WriteLine("Largest Number is" + NumberTwo);

                }
                else
                {
                    Console.WriteLine("Largest Number is" + NumberThree);
                }



            }
            else
            {
                Console.WriteLine("Enter a Valid Input");
            }

        }
    }
}

