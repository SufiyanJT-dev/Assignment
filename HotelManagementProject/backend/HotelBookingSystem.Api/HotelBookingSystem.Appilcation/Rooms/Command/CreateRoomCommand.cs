using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotelBookingSystem.Domain.Entities.Rooms;

namespace HotelBookingSystem.Appilcation.Rooms.Command
{
    public class CreateRoomCommand:IRequest<ActionResult<Domain.Entities.Rooms>>
    {
     
        public string RoomNumber { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public RoomStatus Status { get; set; }
        public decimal PricePerNight { get; set; }



    }
}
