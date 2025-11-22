using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Employee.Query
{
    public class GetAllEmployeeByHotelIdQueryHandler : IRequestHandler<GetAllEmployeeByHotelIdQuery, List<HotelBookingSystem.Domain.Entities.Employee>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetAllEmployeeByHotelIdQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
        public Task<List<Domain.Entities.Employee>> Handle(GetAllEmployeeByHotelIdQuery request, CancellationToken cancellationToken)
        {
          var employees= hotelDbContext.Employees.Where(e => e.HotelId == request.HotelId).ToList();
            return Task.FromResult(employees);
        }


    }
}
