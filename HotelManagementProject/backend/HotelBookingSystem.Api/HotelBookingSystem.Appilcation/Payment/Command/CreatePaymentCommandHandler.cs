using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Payment.Command
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, ActionResult<string>>
    {
        private readonly HotelDbContext hotelDbContext;

        public CreatePaymentCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<string>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
               
                if (request.PaymentDate == default ||
                    request.Amount <= 0 ||
                 
                    request.BookingId <= 0)
                {
                    return new BadRequestObjectResult("All fields are required and must be valid.");
                }

                var payment = new Domain.Entities.Payment
                {
                    PaymentDate = request.PaymentDate,
                    Amount = request.Amount,
                    Status = request.Status,
                    Method = request.Method,
                    BookingId = request.BookingId
                };

                await hotelDbContext.Payments.AddAsync(payment, cancellationToken);
                int result = await hotelDbContext.SaveChangesAsync(cancellationToken);

                if (result <= 0)
                {
                    return new BadRequestObjectResult("Payment was not added. Please try again.");
                }

                return new OkObjectResult("Payment added successfully.");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Unexpected error: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}
