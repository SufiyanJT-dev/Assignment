using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Employee.Query
{
    public class GetEmployeeByIdQuery : IRequest<ActionResult<Dtos.EmployeeGetByIdDtos>>
    {
      public int Id { get; set; }
      
    }
}
