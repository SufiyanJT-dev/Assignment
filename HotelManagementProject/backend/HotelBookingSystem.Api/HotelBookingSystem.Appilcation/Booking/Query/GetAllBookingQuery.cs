using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Booking.Query
{
    public class GetAllBookingQuery : IRequest<List<Domain.Entities.Booking>>
    {
    }
}
