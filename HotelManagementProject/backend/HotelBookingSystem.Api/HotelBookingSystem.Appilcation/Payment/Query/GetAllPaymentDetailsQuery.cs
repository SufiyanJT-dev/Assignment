using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Payment.Query
{
    public  class GetAllPaymentDetailsQuery : IRequest<List< Domain.Entities.Payment>>
    {
       
    }
}
