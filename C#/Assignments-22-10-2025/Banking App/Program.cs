// See https://aka.ms/new-console-template for more information
using Banking_App;

SavingsAccount savings = new SavingsAccount();
savings.Deposit(1000);
savings.Withdraw(200);
Console.WriteLine($"Savings Balance: {savings.Getbalance()}");

CreditCardPayment paymentService = new CreditCardPayment();
PaymentProcessor paymentProcessor = new PaymentProcessor(paymentService);
paymentProcessor.ProcessPayment(500);

