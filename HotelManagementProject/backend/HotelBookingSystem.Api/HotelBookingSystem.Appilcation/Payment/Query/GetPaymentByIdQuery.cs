using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Payment.Query
{
    public class GetPaymentByIdQuery : IRequest<ActionResult<Dtos.PaymentGetByIdDtos>>
    {
      public int Id { get; set; }
      
    }
}
