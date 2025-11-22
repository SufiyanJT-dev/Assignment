using HotelBookingSystem.Appilcation.Booking.Dtos;
using HotelBookingSystem.Appilcation.Customer.Dtos;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Booking.Query
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery,ActionResult<AddBookingDtos>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetBookingByIdQueryHandler(HotelDbContext hotelDbContext) {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<AddBookingDtos>> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Booking bookingdetails = await hotelDbContext.Bookings.FirstOrDefaultAsync(b => b.Id == request.Id);
            if (bookingdetails == null) {
                return new NotFoundObjectResult("Booking Not Found For The Id");
            }
            AddBookingDtos addBookingDtos = new AddBookingDtos()
            {
                CheckInDate = bookingdetails.CheckOutDate,
                CheckOutDate = bookingdetails.CheckOutDate,
                RoomId = bookingdetails.RoomId,
                CustomerId = bookingdetails.CustomerId,
                Status = bookingdetails.Status,
                TotalAmount = bookingdetails.TotalAmount,
            };
            return new OkObjectResult(addBookingDtos);
            
        }
    }
}
