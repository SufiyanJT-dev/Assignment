using HotelBookingSystem.Appilcation.Customer.Dtos;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Customer.Query
{
    public class GetcustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ActionResult<AddCustomerDtos>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetcustomerByIdQueryHandler(HotelDbContext hotelDbContext) {
            this.hotelDbContext = hotelDbContext;
        }
        public async Task<ActionResult<AddCustomerDtos>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {

           var hotels= hotelDbContext.Customers.FirstOrDefault(C => C.Id == request.Id);
            if (hotels == null)
            {
                return new NotFoundObjectResult("NOT FOUND");   
            }
            var addCustomerDtos = new AddCustomerDtos
            {
                FullName = hotels.FullName,
                IdProofNumber = hotels.IdProofNumber,
                PhoneNumber = hotels.PhoneNumber,
                Email = hotels.Email,
            };
            return addCustomerDtos;
        }
    }
}
