using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Payment.Command
{
    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, ActionResult<string>>
    {
        private readonly HotelDbContext hotelDbContext;

        public UpdatePaymentCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<string>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var payment = await hotelDbContext.Payments
                    .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

                if (payment == null)
                {
                    return new NotFoundObjectResult("Payment ID not found");
                }

                // Update only if the new value is valid, otherwise keep old value
                payment.PaymentDate = request.PaymentDate != default(DateTime)
                    ? request.PaymentDate
                    : payment.PaymentDate;

                payment.BookingId = request.BookingId > 0
                    ? request.BookingId
                    : payment.BookingId;

                payment.Amount = request.Amount > 0
                    ? request.Amount
                    : payment.Amount;

             

                int result = await hotelDbContext.SaveChangesAsync(cancellationToken);
                if (result <= 0)
                {
                    return new BadRequestObjectResult("Update not successful");
                }

                return new OkObjectResult("Update successful");
            }
            catch (DbUpdateException dbEx)
            {
                return new BadRequestObjectResult($"Database error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Unexpected error: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}
