using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Employee.Command
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
    {
        private readonly HotelDbContext hotelDbContext;
        private readonly IPasswordHasher<Domain.Entities.Employee> _passwordHasher;
        public CreateEmployeeCommandHandler(HotelDbContext hotelDbContext, IPasswordHasher<Domain.Entities.Employee> passwordHasher) {
            this.hotelDbContext = hotelDbContext;
            this._passwordHasher = passwordHasher;
        }
        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(request.FullName) ||
                    string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.Role) ||
                    string.IsNullOrWhiteSpace(request.password) ||
                    request.HotelId <= 0)
                {
                    return "All fields are required. Please provide valid data.";
                }
                if (await hotelDbContext.Employees.AnyAsync(e=>e.Email==request.Email))
                {
                    return "An employee with the same email already exists.";
                }
                var employee = new Domain.Entities.Employee
                {
                    Email = request.Email,
                    HotelId = request.HotelId,
                    FullName = request.FullName,
                    Role = request.Role,
                    
                };
                employee.password = _passwordHasher.HashPassword(employee, request.password);
                await hotelDbContext.Employees.AddAsync(employee, cancellationToken);
                int result = await hotelDbContext.SaveChangesAsync(cancellationToken);

                if (result <= 0)
                {
                    return "Employee was not added. Please try again.";
                }

                return "Employee added successfully.";
            }
            catch (DbUpdateException dbEx)
            {
                return $"Database error occurred: {dbEx.Message}";
            }
            catch (Exception ex)
            {
                return $"An unexpected error occurred: {ex.Message}";
            }
        }

    }
}
