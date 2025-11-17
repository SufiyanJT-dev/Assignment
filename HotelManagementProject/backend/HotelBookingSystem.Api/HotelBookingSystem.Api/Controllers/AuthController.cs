using Azure.Core;
using HotelBookingSystem.Appilcation.Auth.command;
using HotelBookingSystem.Appilcation.Auth.Query;
using HotelBookingSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("validate-login-Customer")]
        public async Task<IActionResult> validateLogin([FromBody]  ValidateCustomerCredentialAuthQuery query)
        {
        
            var (accessToken, refreshToken,id,role) = await mediator.Send(query);
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("invalid Credential");
            }

            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });
            return Ok(new { accessToken, refreshToken, id, role });
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var refreshToken = command.RefreshToken;
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("Refresh token missing");

            try
            {
                var (accessToken, newRefreshToken) = await mediator.Send(new RefreshTokenCommand { RefreshToken = refreshToken });

                Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Path = "/",
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });

                return Ok(new { accessToken });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        [HttpPost("validate-login-Employee")]


        public async Task<IActionResult> ValidatingLogin([FromBody] validatePasswordQuery query)
        {
            var (accessToken, refreshToken) = await mediator.Send(query);

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Invalid credentials.");
            }

            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(new { accessToken });
        }


    }
}
