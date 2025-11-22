using HotelBookingSystem.Appilcation.Review.Command;
using HotelBookingSystem.Appilcation.Review.Query;
using HotelBookingSystem.Appilcation.Review.Dtos;

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReviewController(IMediator mediator)
        {

            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<string>> CreateReview(CreateReviewCommand command)
        {
            return await mediator.Send(command);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewGetByIdDtos>> DeleteReview(int id)
        {
            GetReviewByIdQuery query = new GetReviewByIdQuery();
            query.Id = id;
           return  await mediator.Send(query);
        }
        [HttpGet]
        public async Task<List<Domain.Entities.Review>> GetAllReview()
        {
            GetAllReviewDetailsQuery query = new GetAllReviewDetailsQuery();
            return await mediator.Send(query);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> UpdateReview(int id, UpdateReviewCommand command)
        {
            command.Id = id;
            return await mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<string> deleteReview(int id)
        {
            DeleteReviewCommand command= new DeleteReviewCommand();
            command.Id = id;
            return await mediator.Send(command);
        }
    }
}
