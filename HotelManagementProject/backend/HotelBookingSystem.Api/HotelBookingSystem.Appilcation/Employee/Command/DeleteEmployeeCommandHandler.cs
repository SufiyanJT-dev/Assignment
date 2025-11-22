using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Employee.Command
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ActionResult<string>>
    {
        private readonly HotelDbContext hotelDbContext;

        public DeleteEmployeeCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
        public async  Task<ActionResult<string>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Employee employee= await hotelDbContext.Employees.FirstOrDefaultAsync(r=>r.Id==request.Id);
            if (employee == null)
            {
                return new NotFoundObjectResult("EMployee Not Found");
            }
            hotelDbContext.Employees.Remove(employee);
           int a= hotelDbContext.SaveChanges();
            if (a < 0) {
                return "Failed To delete";
            }
            return $"{request.Id} deleted";
        }
    }
}
