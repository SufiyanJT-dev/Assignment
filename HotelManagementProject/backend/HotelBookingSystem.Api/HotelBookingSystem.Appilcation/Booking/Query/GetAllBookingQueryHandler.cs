using HotelBookingSystem.Appilcation.Booking.Dtos;
using HotelBookingSystem.Appilcation.Customer.Dtos;
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
    public class GetAllBookingQueryHandler : IRequestHandler<GetAllBookingQuery, List<Domain.Entities.Booking>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetAllBookingQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
        public async  Task<List<Domain.Entities.Booking>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
        {
            var Booking = await hotelDbContext.Bookings.ToListAsync();
           
            return  Booking;
        }
    }
}
