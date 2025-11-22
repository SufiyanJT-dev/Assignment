using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Review.Command
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ActionResult<string>>
    {
        private readonly HotelDbContext hotelDbContext;

        public UpdateReviewCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<string>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var review = await hotelDbContext.Review
                    .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

                if (review == null)
                {
                    return new NotFoundObjectResult("Review ID not found");
                }

                // Update only if valid, else keep old value
                review.CustomerId = request.CustomerId > 0
                    ? request.CustomerId
                    : review.CustomerId;

                review.HotelId = request.HotelId > 0
                    ? request.HotelId
                    : review.HotelId;

                review.Rating = request.Rating > 0
                    ? request.Rating
                    : review.Rating;

                review.Comment = !string.IsNullOrWhiteSpace(request.Comment) && request.Comment != "string"
                    ? request.Comment
                    : review.Comment;

                review.ReviewDate = DateTime.UtcNow;

                int result = await hotelDbContext.SaveChangesAsync(cancellationToken);
                if (result <= 0)
                {
                    return new BadRequestObjectResult("Update not successful");
                }

                return new OkObjectResult("Update successful");
            }
            catch (DbUpdateException dbEx)
            {
                return new BadRequestObjectResult($"Database error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Unexpected error: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}
