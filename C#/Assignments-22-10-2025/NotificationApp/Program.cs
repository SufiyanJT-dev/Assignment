// See https://aka.ms/new-console-template for more information
using NotificationApp;

Console.WriteLine("Enter the name of person to be sended to be sended ");
string name = Console.ReadLine();

EmailNotifier notifier = new EmailNotifier();
AppointmentService appointmentService=new AppointmentService(notifier);

appointmentService.bookAppointment(name );