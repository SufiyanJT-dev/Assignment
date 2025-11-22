using HotelBookingSystem.Appilcation.Customer.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Customer.Query
{
    public class GetCustomerByIdQuery:IRequest<ActionResult<AddCustomerDtos>>
    {
        public int Id { get; set; }

        
    }
}