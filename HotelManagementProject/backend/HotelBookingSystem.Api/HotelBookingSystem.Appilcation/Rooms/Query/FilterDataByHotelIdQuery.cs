using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Rooms.Query
{
    public class FilterDataByHotelIdQuery:IRequest<List<Domain.Entities.Rooms>>
    {
        public int HotelId { get; set; }
        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }
    }
}
