using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Hotels.Query
{
    public  class GetAllHotelDetailsQuery : IRequest<List< Domain.Entities.Hotel>>
    {
       
    }
}
