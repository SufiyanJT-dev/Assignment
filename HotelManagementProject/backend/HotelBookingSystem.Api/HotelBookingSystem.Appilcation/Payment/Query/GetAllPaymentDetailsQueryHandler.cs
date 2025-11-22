using HotelBookingSystem.Appilcation.RoomType.Query;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Payment.Query
{
    public  class GetAllPaymentDetailsQueryHandler : IRequestHandler<GetAllPaymentDetailsQuery, List<Domain.Entities.Payment>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetAllPaymentDetailsQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<List<Domain.Entities.Payment>> Handle(GetAllPaymentDetailsQuery request, CancellationToken cancellationToken)
        {
           List<Domain.Entities.Payment> Payment = await  hotelDbContext.Payments.ToListAsync();
            

            return Payment;
        }
    }
}
