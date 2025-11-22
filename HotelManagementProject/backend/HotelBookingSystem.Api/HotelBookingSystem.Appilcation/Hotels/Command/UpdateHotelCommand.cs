using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Hotels.Command
{
    public class UpdateHotelCommand :IRequest<string>
    {
       [JsonIgnore]  public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        [FromForm] public IFormFile Image { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string PhoneNumber { get; set; }


    }
}
