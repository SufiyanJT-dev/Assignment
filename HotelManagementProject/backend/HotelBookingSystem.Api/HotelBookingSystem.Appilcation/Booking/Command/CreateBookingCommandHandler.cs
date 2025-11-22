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
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, ActionResult<int>>
    {
        private readonly HotelDbContext hotelDbContext;

        public CreateBookingCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<int>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var room = await hotelDbContext.Room.FindAsync(new object[] { request.RoomId }, cancellationToken);
            var customer = await hotelDbContext.Customers.FindAsync(new object[] { request.CustomerId }, cancellationToken);

            if (!Enum.IsDefined(typeof(BookingStatus), request.Status))
            {
                return new BadRequestObjectResult("Status must be a valid value.");
            }

            if (customer == null)
            {
                return new NotFoundObjectResult($"Customer with ID {request.CustomerId} not found.");
            }

            if (room == null)
            {
                return new NotFoundObjectResult("Room not found.");
            }

            if (request.TotalAmount < 0)
            {
                return new BadRequestObjectResult("Total amount must be a positive value.");
            }

            if (request.CheckInDate >= request.CheckOutDate)
            {
                return new BadRequestObjectResult("Check-in date must be before check-out date.");
            }

            bool isRoomBooked = await hotelDbContext.Bookings
                .AnyAsync(b =>
                    b.RoomId == request.RoomId &&
                    b.CheckInDate < request.CheckOutDate &&
                    b.CheckOutDate > request.CheckInDate,
                    cancellationToken);

            if (isRoomBooked)
            {
                return new BadRequestObjectResult("Room is already booked for the selected dates.");
            }

            var booking = new Domain.Entities.Booking
            {
                CustomerId = request.CustomerId,
                RoomId = request.RoomId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                Status = request.Status,
                TotalAmount = request.TotalAmount
            };

            hotelDbContext.Bookings.Add(booking);
            await hotelDbContext.SaveChangesAsync(cancellationToken);

            return new OkObjectResult(booking.Id);
        }

    }
}
