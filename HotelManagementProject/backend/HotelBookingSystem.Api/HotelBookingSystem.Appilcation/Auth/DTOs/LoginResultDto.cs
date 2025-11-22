using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Auth.DTOs
{
    public class LoginResultDto
    {
        public string AccessToken { get; set; }
        public string Error { get; set; }
    }

}
