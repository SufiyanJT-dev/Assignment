using MediatR;

namespace HotelBookingSystem.Appilcation.Auth.command
{
    public class RefreshTokenCommand : IRequest<(string accessToken, string refreshToken)>
    {
        public string RefreshToken { get; set; }
        public string UserType { get; set; }
    }
}
