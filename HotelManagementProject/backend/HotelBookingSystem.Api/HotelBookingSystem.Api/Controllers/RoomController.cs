using HotelBookingSystem.Appilcation.Rooms.Command;
using HotelBookingSystem.Appilcation.Rooms.Dtos;
using HotelBookingSystem.Appilcation.Rooms.Query;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace HotelBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
       
        private readonly IMediator mediator;

        public RoomController(IMediator mediator) { 
        
            this.mediator = mediator;
        }
        [HttpPost]
        public Task<ActionResult<Domain.Entities.Rooms>> CreateRoom(CreateRoomCommand command) { 
            return mediator.Send(command);
        }
        [HttpGet]
        public Task<List<Domain.Entities.Rooms>> GetAllRoom()
        {
            GetAllRoomQuery query = new GetAllRoomQuery();
            return mediator.Send(query);
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public Task<ActionResult<Appilcation.Rooms.Dtos.RoomGetByIdDtos>> GetByidRoomQuery(int id) {
            GetRoomByIdQuery query = new GetRoomByIdQuery();
        query.Id = id;
            return mediator.Send(query);
        }
        [HttpDelete("{id}")]
        public Task<ActionResult<string>> deleteRoom(int id) {
            DeleteRoomCommand command= new DeleteRoomCommand();
            command.Id = id;
            return mediator.Send(command);
        }
        [HttpPatch("{id}")]
        public Task<ActionResult<string>> updateRoom(int id, UpdateRoomCommand command) { 
        command.Id = id;
            return mediator.Send(command);
        }
        [HttpGet("hotel/{hotelId}")]
        public async Task<List<Domain.Entities.Rooms>> GetRoomByHotelId(int hotelId)
        {
         GetAllRoomsByHotelIdQuery query= new GetAllRoomsByHotelIdQuery();
            query.HotelId = hotelId;
            return await mediator.Send(query);
        }
        [HttpPost("room")]
   
        public async Task<IActionResult> GetAvailableRooms(FilterDataByHotelIdQuery query)
        {


           
        

            var rooms = await mediator.Send(query);
            return Ok(rooms);
        }

    }
}
