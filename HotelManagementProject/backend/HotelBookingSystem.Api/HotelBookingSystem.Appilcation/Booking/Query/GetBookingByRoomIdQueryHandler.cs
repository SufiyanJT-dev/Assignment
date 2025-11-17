using Azure.Core;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Booking.Query
{
    public class GetBookingByRoomIdQueryHandler : IRequestHandler<GetBookingByRoomIdQuery, List<Domain.Entities.Booking>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetBookingByRoomIdQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
       async Task<List<Domain.Entities.Booking>> IRequestHandler<GetBookingByRoomIdQuery, List<Domain.Entities.Booking>>.Handle(GetBookingByRoomIdQuery request, CancellationToken cancellationToken)
        {
            var bookings = await hotelDbContext.Bookings.Where(b => b.RoomId == request.roomId).ToListAsync();
            return bookings;
        }
    }

}
