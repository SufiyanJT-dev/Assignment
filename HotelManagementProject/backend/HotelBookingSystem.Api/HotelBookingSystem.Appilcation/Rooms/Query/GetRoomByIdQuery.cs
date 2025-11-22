using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Rooms.Query
{
    public class GetRoomByIdQuery:IRequest<ActionResult<Dtos.RoomGetByIdDtos>>
    {
      public int Id { get; set; }
      
    }
}
