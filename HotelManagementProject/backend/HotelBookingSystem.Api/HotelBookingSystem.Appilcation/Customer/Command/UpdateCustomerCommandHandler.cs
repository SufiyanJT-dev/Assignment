using Azure.Core;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Customer.Command
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand,ActionResult<bool>>
    {
        private readonly HotelDbContext hotelDbContext;

        public UpdateCustomerCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }



        public async Task<ActionResult<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = hotelDbContext.Customers.FirstOrDefault(c => c.Id == request.Id);
            if (customer == null)
            {
                return new NotFoundObjectResult("Customer Id not Found");
            }

            
            if (!string.IsNullOrWhiteSpace(request.FullName) && request.FullName != "string")
            {
                customer.FullName = request.FullName;
            }

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber) && request.PhoneNumber != "string")
            {
                customer.PhoneNumber = request.PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(request.IdProofNumber) && request.IdProofNumber != "string")
            {
                customer.IdProofNumber = request.IdProofNumber;
            }

            if (!string.IsNullOrWhiteSpace(request.Email) && request.Email != "string")
            {
                customer.Email = request.Email;
            }

            var result = await hotelDbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

    }
}
