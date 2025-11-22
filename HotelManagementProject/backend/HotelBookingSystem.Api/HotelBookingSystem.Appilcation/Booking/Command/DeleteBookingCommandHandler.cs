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
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, ActionResult<bool>>
    {
        private readonly HotelDbContext hotelDbContext;

        public DeleteBookingCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<bool>> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var bookingToDelete = await hotelDbContext.Bookings
                .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

            if (bookingToDelete == null)
            {
                return new NotFoundObjectResult($"Booking with ID {request.Id} not found.");
            }

            hotelDbContext.Bookings.Remove(bookingToDelete);
            await hotelDbContext.SaveChangesAsync(cancellationToken);

            return new OkObjectResult("Successfull Found");
        }
    }
}
