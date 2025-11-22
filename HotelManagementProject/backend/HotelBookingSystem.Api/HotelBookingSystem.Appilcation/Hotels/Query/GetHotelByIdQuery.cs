using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Hotels.Query
{
    public class GetHotelByIdQuery : IRequest<ActionResult<Dtos.HotelTypeGetByIdDtos>>
    {
      public int Id { get; set; }
      
    }
}
