using HotelBookingSystem.Appilcation.Booking.Command;
using HotelBookingSystem.Appilcation.Booking.Dtos;
using HotelBookingSystem.Appilcation.Booking.Query;
using HotelBookingSystem.Appilcation.Customer.Command;
using HotelBookingSystem.Appilcation.Customer.Query;
using HotelBookingSystem.Appilcation.Review.Command;
using HotelBookingSystem.Appilcation.Review.Dtos;
using HotelBookingSystem.Appilcation.Review.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator mediator;

        public BookingController(IMediator mediator)
        {

            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateBooking(CreateBookingCommand command)
        {
            return await mediator.Send(command);
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Appilcation.Booking.Dtos.AddBookingDtos>> GetByIDBooking(int id)
        {
            GetBookingByIdQuery query = new GetBookingByIdQuery();
            query.Id = id;
            return await mediator.Send(query);
        }

        [HttpGet]
        public async Task<ActionResult<List<Appilcation.Booking.Dtos.AddBookingDtos>>> GetAllBooking()
        {



            var query = new GetAllBookingQuery();

            var Customer = await mediator.Send(query);
            if (Customer == null)
                return NotFound("No customers found.");
            return Ok(Customer);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteBooking(int id)
        {
            var command = new DeleteBookingCommand();
            command.Id = id;

            return await mediator.Send(command);
        }
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> UpdateBooking(UpdateBookingCommand commad, int id)
        {


            commad.Id = id;

            return await mediator.Send(commad);

        }
        [HttpGet("RoomId{roomId}")]
        [Authorize]
        public async Task<List<Domain.Entities.Booking>> GetBokinByRoomId(int roomId)
        {
            var query = new GetBookingByRoomIdQuery();
            query.roomId = roomId;
            return await mediator.Send(query);
        }
        [HttpGet("GetByCustomer{CustomerId}")]
        [Authorize]
        public async Task<List<BookinDesplayDataDtos>> GetBookingDeatils(int CustomerId)
        {
            GetBookingDetailsByCustomerIdQuery query =new  GetBookingDetailsByCustomerIdQuery();
            query.id = CustomerId;
            return await mediator.Send(query);
        }

    }
}
