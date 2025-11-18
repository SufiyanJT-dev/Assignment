using HotelBookingSystem.Appilcation.Customer.Dtos;
using HotelBookingSystem.Appilcation.Hotels.Command;
using HotelBookingSystem.Appilcation.Hotels.Dtos;
using HotelBookingSystem.Appilcation.Hotels.Query;
using HotelBookingSystem.Appilcation.Rooms.Query;
using HotelBookingSystem.Appilcation.RoomType.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator mediator;

        public HotelController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<string>> Create([FromForm] CreateHotelCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appilcation.Hotels.Dtos.HotelTypeGetByIdDtos>> GetByID(int id)
        {
            
            GetHotelByIdQuery query = new GetHotelByIdQuery();
            query.Id= id;
            return await mediator.Send(query);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> UpdateHotel(UpdateHotelCommand command,int id)
        {
            command.Id= id;
            return await mediator.Send(command);
        }
      
        [HttpGet]
        [Authorize]
        public async Task<List<Domain.Entities.Hotel>> GetAll()
        {
            GetAllHotelDetailsQuery query= new GetAllHotelDetailsQuery();
            return await mediator.Send(query);
        }
        [HttpDelete("{id}")]
        public async Task<string> DeleteHotel(int id)
        {
            DeleteHotelCommand command= new DeleteHotelCommand();
            command.Id= id;
            return await mediator.Send(command);
        }
        [HttpGet("filter")]
        public async Task<List<Domain.Entities.Rooms>> Filters([FromQuery] FilterQuery query)
        {
           
            return await mediator.Send(query);
        }




    }
}

