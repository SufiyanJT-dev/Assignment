public class SportsCat
{
    public SportsCat()
    {
        Console.WriteLine("Enter A Category Code From c , f ,h ,t ,b ");
        String CatString = Console.ReadLine();
        if(char.TryParse(CatString,out Char CatChar))
        {
            switch (CatChar) {
                case ('c'):
                    Console.WriteLine("Cricket");
                    break;
                case ('f'):
                    Console.WriteLine("Football");
                    break;
                case ('h'):
                    Console.WriteLine("Hockey");
                    break;
                case ('t'):
                    Console.WriteLine("Tennis");
                    break;
                case ('b'):
                    Console.WriteLine("Badminton");
                    break;
                default:
                    Console.WriteLine("Enter A Valid code");
                    break;
            }
           

        }
        else
        {
            Console.WriteLine("Enter A valid Char");
        }
    }
}