using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Customer.Command
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ActionResult<bool>>
    {
        private readonly HotelDbContext hotelDbContext;

        public DeleteCustomerCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerdeleted = await hotelDbContext.Customers.FirstOrDefaultAsync(h => h.Id == request.Id);
            if (customerdeleted == null)
            {
                return new NotFoundObjectResult(customerdeleted);
            }
            hotelDbContext.Customers.Remove(customerdeleted);
           
            int a = await hotelDbContext.SaveChangesAsync();
            return new OkObjectResult(true);
        }
     
    }
}
