using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.RoomType.Command
{
    public class CreateRoomTypeCommand:IRequest<ActionResult<string>>
    {
        public string TypeName { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }


    }
}
