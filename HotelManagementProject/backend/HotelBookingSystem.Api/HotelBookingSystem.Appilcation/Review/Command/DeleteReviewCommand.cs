using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Review.Command
{
    public class DeleteReviewCommand:IRequest<string>
    {
        public int Id { get; set; }
    }
}
