using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Employee.Command
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ActionResult<string>>
    {
        private readonly HotelDbContext hotelDbContext;

        public UpdateEmployeeCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<string>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await hotelDbContext.Employees.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
                if (employee == null)
                {
                    return new NotFoundObjectResult("Employee ID not found");
                }

                
                if (string.IsNullOrWhiteSpace(request.FullName) ||
                    string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.Role) ||
                    request.HotelId <= 0)
                {
                    return new BadRequestObjectResult("All fields are required and must be valid");
                }

                employee.FullName = request.FullName;
                employee.Role = request.Role;
                employee.Email = request.Email;
                employee.HotelId = request.HotelId;

                int result = await hotelDbContext.SaveChangesAsync(cancellationToken);
                if (result <= 0)
                {
                    return new BadRequestObjectResult("Update not successful");
                }

                return new OkObjectResult("Update successful");
            }
            catch (DbUpdateException dbEx)
            {
                return new BadRequestObjectResult($"Database error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Unexpected error: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}
