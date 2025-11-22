using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Auth.Query
{
    public class ValidateCustomerCredentialAuthQuery:IRequest<(string accessToken, string refreshToken,int id,string role)>
    {
        public string email { get; set; }
        public string Password { get; set; }
    }
}
