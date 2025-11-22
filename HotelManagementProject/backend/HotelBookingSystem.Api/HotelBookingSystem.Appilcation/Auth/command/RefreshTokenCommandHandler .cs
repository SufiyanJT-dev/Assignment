using HotelBookingSystem.Appilcation.Auth.command;
using HotelBookingSystem.Appilcation.Auth.Query;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Appilcation.Auth.Command
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, (string accessToken, string refreshToken)>
    {
        private readonly HotelDbContext _hotelDbContext;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public RefreshTokenCommandHandler(HotelDbContext hotelDbContext, JwtTokenGenerator jwtTokenGenerator)
        {
            _hotelDbContext = hotelDbContext;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<(string accessToken, string refreshToken)> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var hashedToken = _jwtTokenGenerator.HashRefreshToken(request.RefreshToken);

            var tokenEntity = await _hotelDbContext.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == hashedToken && !t.Revoked, cancellationToken);

            if (tokenEntity == null || tokenEntity.ExpiresAt < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            
            tokenEntity.Revoked = true;

            string newAccessToken;
            string newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();
            string hashedNewRefreshToken = _jwtTokenGenerator.HashRefreshToken(newRefreshToken);

            if (tokenEntity.UserType == "Employee")
            {
                var employee = await _hotelDbContext.Employees.FindAsync(tokenEntity.UserId);
                if (employee == null)
                    throw new UnauthorizedAccessException("User not found");

                newAccessToken = await _jwtTokenGenerator.GenerateToken(employee.Email, "Employee");

                _hotelDbContext.RefreshTokens.Add(new RefreshToken
                {
                    UserId = employee.Id,
                    Token = hashedNewRefreshToken,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    CreatedAt = DateTime.UtcNow,
                    Revoked = false,
                    UserType = "Employee"
                });
            }
            else
            {
                var customer = await _hotelDbContext.Customers.FindAsync(tokenEntity.UserId);
                if (customer == null)
                    throw new UnauthorizedAccessException("User not found");

                newAccessToken = await _jwtTokenGenerator.GenerateToken(customer.Email, "Customer");

                _hotelDbContext.RefreshTokens.Add(new RefreshToken
                {
                    UserId = customer.Id,
                    Token = hashedNewRefreshToken,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    CreatedAt = DateTime.UtcNow,
                    Revoked = false,
                    UserType = "Customer"
                });
            }

            await _hotelDbContext.SaveChangesAsync(cancellationToken);

            return (newAccessToken, newRefreshToken); // return raw refresh token to client
        }
    }
}
