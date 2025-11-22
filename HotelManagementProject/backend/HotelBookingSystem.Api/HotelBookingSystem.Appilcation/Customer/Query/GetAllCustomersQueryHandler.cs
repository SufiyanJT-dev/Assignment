using HotelBookingSystem.Appilcation.Customer.Dtos;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Customer.Query
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<Dtos.AddCustomerDtos>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetAllCustomersQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
        public async  Task<List<AddCustomerDtos>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var hotels = await hotelDbContext.Customers.ToListAsync();
            List<Dtos.AddCustomerDtos> addCustomerDtos = new List<AddCustomerDtos>();
            foreach (var item in hotels) {  
                Dtos.AddCustomerDtos customerDtos = new Dtos.AddCustomerDtos();
                customerDtos.FullName = item.FullName;
                customerDtos.PhoneNumber = item.PhoneNumber;
                customerDtos.IdProofNumber = item.IdProofNumber;
                customerDtos.Email = item.Email;
                addCustomerDtos.Add(customerDtos);
            }
            return addCustomerDtos;
        }
    }
}
