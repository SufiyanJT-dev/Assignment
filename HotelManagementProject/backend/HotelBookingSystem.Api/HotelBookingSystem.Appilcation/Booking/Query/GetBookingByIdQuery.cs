using HotelBookingSystem.Appilcation.Booking.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Booking.Query
{
    public class GetBookingByIdQuery : IRequest<ActionResult<AddBookingDtos>>
    {
        public int Id { get; set; }

        
    }
}