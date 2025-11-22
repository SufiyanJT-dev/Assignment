using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Review.Command
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ActionResult<string>>
    {
        private readonly HotelDbContext hotelDbContext;

        public CreateReviewCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<string>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
               
                if (request.CustomerId <= 0 ||
                    request.HotelId <= 0 ||
                    request.Rating <= 0 ||
                    string.IsNullOrWhiteSpace(request.Comment) ||
                    request.Comment == "string")
                {
                    return new BadRequestObjectResult("All fields are required and must be valid.");
                }

                var review = new Domain.Entities.Review
                {
                    CustomerId = request.CustomerId,
                    HotelId = request.HotelId,
                    Rating = request.Rating,
                    Comment = request.Comment,
                    ReviewDate = DateTime.Now
                };

                hotelDbContext.Review.Add(review);
                int result = await hotelDbContext.SaveChangesAsync(cancellationToken);

                if (result <= 0)
                {
                    return new BadRequestObjectResult("Review was not added. Please try again.");
                }

                return new OkObjectResult("Review added successfully.");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Unexpected error: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}
