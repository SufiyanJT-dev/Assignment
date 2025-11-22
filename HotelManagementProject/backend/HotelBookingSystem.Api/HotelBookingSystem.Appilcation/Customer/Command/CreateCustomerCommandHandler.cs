using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Customer.Command
{
    public class CreateCustomerCommandHandler:IRequestHandler<CreateCustomerCommand, ActionResult<int>>
    { 
        private readonly HotelDbContext hotelDbContext;
        private readonly IPasswordHasher<Domain.Entities.Customer> passwordHasher;

        public CreateCustomerCommandHandler(HotelDbContext hotelDbContext,IPasswordHasher<Domain.Entities.Customer> passwordHasher)
        {
            this.hotelDbContext = hotelDbContext;
            this.passwordHasher = passwordHasher;
        }

        public async Task<ActionResult<int>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Customer customer = new Domain.Entities.Customer();
            if (request == null)
            {
                return new BadRequestObjectResult("Pls Add All Value");
            }
            bool existCustomer= await hotelDbContext.Customers.AnyAsync(c => c.IdProofNumber == request.IdProofNumber || c.PhoneNumber==request.PhoneNumber || c.Email==request.Email);
            if (existCustomer)
            {
                return new ConflictObjectResult("User Already Exist Try to use other Id's or phonenumber ");
            }
            customer.FullName = request.FullName;
            customer.PhoneNumber = request.PhoneNumber;
            customer.Email = request.Email;
            customer.password = passwordHasher.HashPassword(customer, request.password);
            customer.IdProofNumber = request.IdProofNumber;
             hotelDbContext.Add(customer);
            return new OkObjectResult( await hotelDbContext.SaveChangesAsync());

           
        }
    }
}
