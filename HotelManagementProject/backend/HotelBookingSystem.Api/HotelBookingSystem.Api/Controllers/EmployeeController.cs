using HotelBookingSystem.Appilcation.Auth.command;
using HotelBookingSystem.Appilcation.Auth.Query;
using HotelBookingSystem.Appilcation.Customer.Query;
using HotelBookingSystem.Appilcation.Employee.Command;
using HotelBookingSystem.Appilcation.Employee.Dtos;
using HotelBookingSystem.Appilcation.Employee.Query;
using HotelBookingSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IMediator mediator;

        public EmployeeController(IMediator mediator)
        {
            this.mediator = mediator;
        }
       
        [HttpPost]
        public async Task<string> CreateEmployee(CreateEmployeeCommand command)
        {
            return await mediator.Send(command);
        }
       
        [HttpGet]
        public async Task<List<Domain.Entities.Employee>> GetEmployeeAsync()
        {
            GetAllEmployeeQuery query = new GetAllEmployeeQuery();
            return await mediator.Send(query);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeGetByIdDtos>> getByIdEmployee(int id)
        {
            GetEmployeeByIdQuery query = new GetEmployeeByIdQuery();
            query.Id = id;
            return await mediator.Send(query);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteRoomType(int id)
        {
            DeleteEmployeeCommand command = new DeleteEmployeeCommand();

            command.Id = id;
            return await mediator.Send(command);

        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> UpdateRoomType(UpdateEmployeeCommand command, int id)
        {

            command.Id = id;
            return await mediator.Send(command);
        }
       
        //public async Task<IActionResult> Logout()
        //{
        //    Response.Cookies.Append("refreshToken", "", new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = false,
        //        SameSite = SameSiteMode.Lax,
        //        Path = "/",
        //        Expires = DateTimeOffset.UtcNow.AddDays(-1)
        //    });
        //    return Ok("Logged out successfully.");
        //}
        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetByHotelId(int hotelId)
        {
           GetAllEmployeeByHotelIdQuery query= new GetAllEmployeeByHotelIdQuery();
            query.HotelId = hotelId;
            var employees = await mediator.Send(query);
            return Ok(employees);
        }
     
    }
}