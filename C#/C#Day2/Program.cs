// See https://aka.ms/new-console-template for more information
using Day_2_Assigments;

Console.WriteLine();
Console.WriteLine("Print first 20 numbers using for loop\r\n");

N_NaturalNumbers ObjNaturalNumber = new N_NaturalNumbers();
Console.WriteLine();
Console.WriteLine("Print odd numbers less than 50 using while loop");
OddNumber obj1 = new OddNumber();
Console.WriteLine();
Console.WriteLine("Large amount 3 numbers");
    LargestOf3Numbers obj2 = new LargestOf3Numbers();
Console.WriteLine();
Console.WriteLine("Reverse of a number");
Console.WriteLine();
Console.WriteLine("Number is 234 and revesed number is");
ReverseOfNumber obj3 = new ReverseOfNumber(234);
Console.WriteLine();
Console.WriteLine("Sum of the digits of a number\r\n");
Console.WriteLine();
Console.WriteLine("Number is 234 and sum of digit number is");
SumOfDigits obj4 = new SumOfDigits(234);
Console.WriteLine("Print all prime numbers below 100\r\n");
Console.WriteLine();
PrimeNumberBelow100 obj5 = new PrimeNumberBelow100();
Console.WriteLine("Check prime number\r\n");
Console.WriteLine("Number to be chcked is 5");
Console.WriteLine(obj5.CheckPrime(5));


Console.WriteLine("Fibonacci series\r\n");
FibonacciSeries objfrb = new FibonacciSeries();
Console.WriteLine("Tax calculation program, input the amount and display the tax\r\n");
TaxCaculation objTaxCal = new TaxCaculation();
Console.WriteLine("Input a character from console and display the sports name corresponding to it\r\n");
SportsCat obj = new SportsCat();