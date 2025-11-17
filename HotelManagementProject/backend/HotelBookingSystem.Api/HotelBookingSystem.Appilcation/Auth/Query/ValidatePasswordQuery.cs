using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Auth.Query
{
   
        public class validatePasswordQuery : IRequest<(string accessToken, string refreshToken)>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    

}

