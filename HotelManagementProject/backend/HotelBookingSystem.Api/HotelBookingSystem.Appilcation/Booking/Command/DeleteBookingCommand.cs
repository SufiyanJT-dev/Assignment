using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Booking.Command
{
    public class DeleteBookingCommand : IRequest<ActionResult<bool>>
    {
        public int Id { get; set; }

       
    }
}
