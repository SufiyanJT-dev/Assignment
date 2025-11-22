using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Employee.Query
{
    public  class GetAllEmployeeDetailsQueryHandler : IRequestHandler<GetAllEmployeeQuery, List<Domain.Entities.Employee>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetAllEmployeeDetailsQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<List<Domain.Entities.Employee>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Employee> employees = await hotelDbContext.Employees.ToListAsync(cancellationToken);
            

            return employees;
        }
    }
}
