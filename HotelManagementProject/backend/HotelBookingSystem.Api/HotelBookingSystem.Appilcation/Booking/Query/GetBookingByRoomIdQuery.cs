using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Booking.Query
{
    public class GetBookingByRoomIdQuery : IRequest<List<Domain.Entities.Booking>>
    {
        public int roomId { get; set; }
        

      
    }
}
