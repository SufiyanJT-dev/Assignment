using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Appilcation.Auth.Query
{
    public class ValidatePasswordQueryHandler : IRequestHandler<validatePasswordQuery, (string accessToken, string refreshToken)>
    {
        private readonly HotelDbContext _hotelDbContext;
        private readonly IPasswordHasher<Domain.Entities.Employee> _passwordHasher;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public ValidatePasswordQueryHandler(
            HotelDbContext hotelDbContext,
            IPasswordHasher<Domain.Entities.Employee> passwordHasher,
            JwtTokenGenerator jwtTokenGenerator)
        {
            _hotelDbContext = hotelDbContext;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<(string accessToken, string refreshToken)> Handle(validatePasswordQuery request, CancellationToken cancellationToken)
        {
            var employee = await _hotelDbContext.Employees
                .FirstOrDefaultAsync(e => e.Email == request.Email, cancellationToken);

            if (employee == null)
                return (null, null);

            var result = _passwordHasher.VerifyHashedPassword(employee, employee.password, request.Password);

            if (result != PasswordVerificationResult.Success)
                return (null, null);

            var rawRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();
            var hashedRefreshToken = _jwtTokenGenerator.HashRefreshToken(rawRefreshToken);
            var accessToken = await _jwtTokenGenerator.GenerateToken(employee.Email, "Employee");

            _hotelDbContext.RefreshTokens.Add(new RefreshToken
            {
                UserId = employee.Id,
                UserType = "Employee",
                Token = hashedRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                Revoked = false
            });

            await _hotelDbContext.SaveChangesAsync(cancellationToken);

            return (accessToken, rawRefreshToken);
        }

    }
}
