using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Auth.Query
{
    public class ValidateCustomerCredentialAuthQueryHandler : IRequestHandler<ValidateCustomerCredentialAuthQuery, (string accessToken, string refreshToken, int id,string role)>
    {
        private readonly HotelDbContext hotelDbContext;
        private readonly IPasswordHasher<Domain.Entities.Customer> passwordHasher;
        private readonly JwtTokenGenerator jwtTokenGenerator;

        public ValidateCustomerCredentialAuthQueryHandler(HotelDbContext hotelDbContext,IPasswordHasher<Domain.Entities.Customer> passwordHasher,JwtTokenGenerator jwtTokenGenerator)
        {
            this.hotelDbContext = hotelDbContext;
            this.passwordHasher = passwordHasher;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<(string accessToken, string refreshToken, int id, string role)> Handle(ValidateCustomerCredentialAuthQuery request, CancellationToken cancellationToken)
        {
            var customer =await  hotelDbContext.Customers.FirstOrDefaultAsync(e=>e.Email== request.email);
           
            if (customer == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
            var result = passwordHasher.VerifyHashedPassword(customer, customer.password, request.Password);
            if (result == PasswordVerificationResult.Success)
            {
                var newAccessToken = await jwtTokenGenerator.GenerateToken(customer.Email, "Customer");
                var newRefreshToken = jwtTokenGenerator.GenerateRefreshToken();

                var tokenEntity = new RefreshToken
                {
                    UserId = customer.Id,
                    Token = newRefreshToken,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    CreatedAt = DateTime.UtcNow,
                    Revoked = false,
                    UserType= "Customer"

                };
                hotelDbContext.RefreshTokens.Add(tokenEntity);
                await hotelDbContext.SaveChangesAsync(cancellationToken);
                return (newAccessToken, newRefreshToken, customer.Id, "Customer");
            }
            throw new UnauthorizedAccessException("Invalid credentials");
        }
    }
}
