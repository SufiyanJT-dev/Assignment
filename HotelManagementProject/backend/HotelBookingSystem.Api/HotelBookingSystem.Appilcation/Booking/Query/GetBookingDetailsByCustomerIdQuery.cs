using HotelBookingSystem.Appilcation.Booking.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Booking.Query
{
    public class GetBookingDetailsByCustomerIdQuery:IRequest<List<BookinDesplayDataDtos>>
    {
        public int id { get; set; }
    }
}
