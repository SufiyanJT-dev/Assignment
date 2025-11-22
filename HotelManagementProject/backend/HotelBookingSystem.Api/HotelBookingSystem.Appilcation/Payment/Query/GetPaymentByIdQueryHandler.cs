using HotelBookingSystem.Appilcation.Hotels.Query;
using HotelBookingSystem.Appilcation.Payment.Dtos;
using HotelBookingSystem.Appilcation.Review.Dtos;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Payment.Query
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, ActionResult<Dtos.PaymentGetByIdDtos>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetPaymentByIdQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<Dtos.PaymentGetByIdDtos>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Payment payment = await hotelDbContext.Payments.FirstOrDefaultAsync(h => h.Id == request.Id);
            if (payment == null) {
                return null;
            }
            Dtos.PaymentGetByIdDtos PaymentGetByIDDtos = new PaymentGetByIdDtos()
            {
                PaymentDate = payment.PaymentDate,
                BookingId = payment.BookingId,
                Amount = payment.Amount,
                Method = payment.Method,
                Status = payment.Status,
            };
            
            return PaymentGetByIDDtos;

        }

        //public async Task<string> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        //{
    

        //}

    }
}
