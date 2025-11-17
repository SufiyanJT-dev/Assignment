using HotelBookingSystem.Appilcation.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Auth.Query
{
    public class JwtTokenGenerator
    {
        private readonly JwtSettings _settings;

        public JwtTokenGenerator(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }

        public Task<string> GenerateToken( string email,string role)
        {
            if (string.IsNullOrEmpty(_settings.Key))
                throw new InvalidOperationException("JWT Key is missing from configuration.");
          
          
            var claims = new[] {
          
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role,role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
                signingCredentials: creds
            );


            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);

        }

    }

}
