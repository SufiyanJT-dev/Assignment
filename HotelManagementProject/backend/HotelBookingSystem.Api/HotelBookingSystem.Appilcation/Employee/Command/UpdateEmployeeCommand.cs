using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Employee.Command
{
    public class UpdateEmployeeCommand : IRequest<ActionResult<string>>
    {
       [JsonIgnore]  public int Id { get; set; }
        public int HotelId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }



    }
}
