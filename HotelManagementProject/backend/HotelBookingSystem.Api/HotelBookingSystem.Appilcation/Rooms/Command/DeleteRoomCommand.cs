using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Rooms.Command
{
    public class DeleteRoomCommand:IRequest<ActionResult<string>>
    {
        public int Id { get; set; }

    }
}
