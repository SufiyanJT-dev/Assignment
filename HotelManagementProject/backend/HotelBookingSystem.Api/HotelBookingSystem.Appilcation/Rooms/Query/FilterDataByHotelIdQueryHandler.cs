using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotelBookingSystem.Domain.Entities.Rooms;

namespace HotelBookingSystem.Appilcation.Rooms.Query
{
    public class FilterDataByHotelIdQueryHandler : IRequestHandler<FilterDataByHotelIdQuery, List<Domain.Entities.Rooms>>
    {
        private readonly HotelDbContext hotelDbContext;
        public FilterDataByHotelIdQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
        public async Task<List<Domain.Entities.Rooms>> Handle(FilterDataByHotelIdQuery request, CancellationToken cancellationToken)
        {
            var bookedRoomIds = hotelDbContext.Bookings
           .Where(b => request.checkInDate < b.CheckOutDate && request.checkOutDate > b.CheckInDate && b.Status== BookingStatus.Confirmed)
           .Select(b => b.RoomId)
           .ToList();

            
            var rooms = await hotelDbContext.Room
         .Where(r => r.HotelId == request.HotelId &&r.Status!= RoomStatus.UnderMaintenance && !bookedRoomIds.Contains(r.Id))
         .Include(r => r.hotel)    
         .Include(r => r.Bookings)
         .ToListAsync(cancellationToken);

            return rooms;
        }

    }
}
