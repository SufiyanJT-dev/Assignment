using HotelBookingSystem.Appilcation.Employee.Dtos;
using HotelBookingSystem.Appilcation.Employee.Query;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Employee.Query
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery,ActionResult<Dtos.EmployeeGetByIdDtos>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetEmployeeByIdQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<Dtos.EmployeeGetByIdDtos>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Employee employee = await hotelDbContext.Employees.FirstOrDefaultAsync(R => R.Id == request.Id);
            if (employee==null)
            {
                return new NotFoundObjectResult("employee not found");
            }
            Dtos.EmployeeGetByIdDtos employeeGetByIdDtos = new Dtos.EmployeeGetByIdDtos()
            {
                Email = employee.FullName,
                FullName = employee.FullName,
                Role=employee.Role,
                HotelId = employee.HotelId,
            };

            return new OkObjectResult( employeeGetByIdDtos);
        }

        //public async Task<RoomTypeGetByIdDtos> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        //{
        //    Domain.Entities.Hotel hotelUpdatedetails = await hotelDbContext.Hotels.FirstOrDefaultAsync(h => h.Id == request.Id);
        //    if (hotelUpdatedetails == null) {
        //        return null;
        //    }
        //    Dtos.RoomTypeGetByIdDtos hotelGetByIDDtos=new RoomTypeGetByIdDtos();
        //    hotelGetByIDDtos.PhoneNumber = hotelUpdatedetails.PhoneNumber;
        //    hotelGetByIDDtos.Address = hotelUpdatedetails.Address;
        //    hotelGetByIDDtos.City = hotelUpdatedetails.City;
        //    hotelGetByIDDtos.Name= hotelUpdatedetails.Name;
        //    hotelGetByIDDtos.Country = hotelUpdatedetails.Country;
        //    return hotelGetByIDDtos;

        //}


        //public async Task<string> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        //{


        //}

    }
}
