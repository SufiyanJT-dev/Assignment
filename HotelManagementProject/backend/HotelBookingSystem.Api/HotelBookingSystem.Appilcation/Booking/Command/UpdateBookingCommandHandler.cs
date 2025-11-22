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

namespace HotelBookingSystem.Appilcation.Booking.Command
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, ActionResult<bool>>
    {
        private readonly HotelDbContext hotelDbContext;

        public UpdateBookingCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<bool>> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await hotelDbContext.Bookings
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (booking == null)
            {
                return new NotFoundObjectResult("Booking ID not found.");
            }

            var customerExists = await hotelDbContext.Customers
                .AnyAsync(c => c.Id == request.CustomerId, cancellationToken);

            if (!customerExists)
            {
                return new NotFoundObjectResult("Customer ID not found.");
            }

            var roomExists = await hotelDbContext.Room
                .AnyAsync(r => r.Id == request.RoomId, cancellationToken);

            if (!roomExists)
            {
                return new NotFoundObjectResult("Room ID not found.");
            }

            if (request.CheckInDate >= request.CheckOutDate)
            {
                return new BadRequestObjectResult("Check-in date must be before check-out date.");
            }

            if (!Enum.IsDefined(typeof(BookingStatus), request.Status))
            {
                return new BadRequestObjectResult("Invalid booking status.");
            }

            booking.CustomerId = request.CustomerId;
            booking.RoomId = request.RoomId;
            booking.CheckInDate = request.CheckInDate;
            booking.CheckOutDate = request.CheckOutDate;
            booking.Status = request.Status;
            booking.TotalAmount = request.TotalAmount;

            await hotelDbContext.SaveChangesAsync(cancellationToken);
            return new OkObjectResult(true);
        }
    }

}
