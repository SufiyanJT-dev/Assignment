using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Review.Query
{
    public  class GetAllReviewDetailsQuery : IRequest<List< Domain.Entities.Review>>
    {
       
    }
}
