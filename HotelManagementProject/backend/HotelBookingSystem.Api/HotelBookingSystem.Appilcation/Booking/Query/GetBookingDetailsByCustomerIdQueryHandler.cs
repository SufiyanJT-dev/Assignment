using HotelBookingSystem.Appilcation.Booking.Dtos;
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
    public class GetBookingDetailsByCustomerIdQueryHandler : IRequestHandler<GetBookingDetailsByCustomerIdQuery, List<BookinDesplayDataDtos>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetBookingDetailsByCustomerIdQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
       async Task<List<BookinDesplayDataDtos>> IRequestHandler<GetBookingDetailsByCustomerIdQuery, List<BookinDesplayDataDtos>>.Handle(GetBookingDetailsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
      
                     
                var bookings = await hotelDbContext.Bookings
                    .Include(b => b.Room)
                    .ThenInclude(r => r.RoomType)
                    .Include(b => b.Room.hotel)
                    .Where(b => b.CustomerId == request.id )
                    .ToListAsync(cancellationToken);

                var result = bookings.Select(b => new BookinDesplayDataDtos
                {
                    bookingId = b.Id,
                    status=b.Status,
                    checkInDate = b.CheckInDate,
                    checkOutDate = b.CheckOutDate,
                    name = b.Room.hotel.Name,
                    address = b.Room.hotel.Address,
                    phoneNumber = b.Room.hotel.PhoneNumber,
                    path=b.Room.hotel.path,
                    
                    roomNumber = b.Room.RoomNumber,
                    pricePerNight = b.Room.PricePerNight.ToString()
                }).ToList();

                return result;
            }
       

    
}
}
