using HotelBookingSystem.Appilcation.Hotels.Command;
using HotelBookingSystem.Appilcation.Hotels.Query;
using HotelBookingSystem.Appilcation.RoomType.Command;
using HotelBookingSystem.Appilcation.RoomType.Dtos;
using HotelBookingSystem.Appilcation.RoomType.Query;
using HotelBookingSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IMediator mediator;

        public RoomTypeController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> CreateRoomType(CreateRoomTypeCommand command)
        {
            return await mediator.Send(command);
        }
        [HttpGet]
        public  async Task<List<Domain.Entities.RoomType>> GetRoomTypesAsync()
        {
            GetAllRoomTypeQuery query = new GetAllRoomTypeQuery();
            return  await mediator.Send(query);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomTypeGetByIdDtos>> getByIdRoomType(int id)
        {
            GetRoomTypeByIdQuery query = new GetRoomTypeByIdQuery();
            query.Id = id;
            return await mediator.Send(query);
        }
        [HttpDelete("{id}")]
        public async Task<string> DeleteRoomType(int id)
        {
            DeleteRoomTypeCommand command = new DeleteRoomTypeCommand();
            
            command.Id = id;
            return await mediator.Send(command);

        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> UpdateRoomType(UpdateRoomTypeCommand command,int id)
        {
          
            command.Id = id;
            return await mediator.Send(command);
        }
    }

}
