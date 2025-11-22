using HotelBookingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Booking.Dtos
{
    public class BookinDesplayDataDtos
    {
            public int bookingId { get; set; }
            public string path { get; set; }
            public DateTime checkInDate { get; set; }
            public DateTime checkOutDate { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public BookingStatus status { get; set; }
            public string phoneNumber { get; set; }
            public string roomNumber { get; set; }
            public string pricePerNight { get; set; }
 

    }
}
