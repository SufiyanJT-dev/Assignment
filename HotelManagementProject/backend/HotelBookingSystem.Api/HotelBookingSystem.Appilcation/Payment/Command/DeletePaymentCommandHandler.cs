using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Payment.Command
{
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, string>
    {
        private readonly HotelDbContext hotelDbContext;

        public DeletePaymentCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
        public async  Task<string> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.Payment payment= await hotelDbContext.Payments.FirstOrDefaultAsync(h=>h.Id==request.Id);
            if (payment == null) {
                return "Payment Not Found";
            }
            hotelDbContext.Payments.Remove(payment);
           int a= hotelDbContext.SaveChanges();
            if (a < 0) {
                return "Failed To delete";
            }
            return $"{request.Id} deleted";
        }
    }
}
