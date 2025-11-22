using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.RoomType.Query
{
    public class GetRoomTypeByIdQuery:IRequest<ActionResult<Dtos.RoomTypeGetByIdDtos>>
    {
      public int Id { get; set; }
      
    }
}
