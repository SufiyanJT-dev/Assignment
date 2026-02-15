using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NotificationApp
{
    public  class AppointmentService
    {
       private INotificationService NotificationService;
        public AppointmentService( INotificationService notificationService) {
            NotificationService=notificationService;

        }
        public void bookAppointment(string name)
        {
            string message = "dear" + name + "you Apointment is booked ";
            NotificationService.Notify(message);
        }
    }
}
