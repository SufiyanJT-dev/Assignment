using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Review.Command
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, string>
    {
        private readonly HotelDbContext hotelDbContext;

        public DeleteReviewCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
        public async  Task<string> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Review review= await hotelDbContext.Review.FirstOrDefaultAsync(h=>h.Id==request.Id);
            if (review == null)
            {
                return "Review Not Found";
            }
            hotelDbContext.Review.Remove(review);
           int a= hotelDbContext.SaveChanges();
            if (a < 0) {
                return "Failed To delete";
            }
            return $"{request.Id} deleted";
        }
    }
}
