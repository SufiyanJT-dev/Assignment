using HotelBookingSystem.Appilcation.Auth.Query;
using HotelBookingSystem.Appilcation.Customer.Command;
using HotelBookingSystem.Appilcation.Customer.Query;
using HotelBookingSystem.Appilcation.Employee.Query;
using HotelBookingSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HotelBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;
        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<int>> AddCustomer(CreateCustomerCommand command)
        {
            return await mediator.Send(command);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Appilcation.Customer.Dtos.AddCustomerDtos>> GetByIDCustomer(int id)
        {
            GetCustomerByIdQuery query = new GetCustomerByIdQuery();
            query.Id = id;
            return await mediator.Send(query);
        }

        [HttpGet]
        public async Task<ActionResult<List<Appilcation.Customer.Dtos.AddCustomerDtos>>> GetAllCustomer()
        {
           


            var query = new GetAllCustomersQuery();    
             
             var Customer= await mediator.Send(query);
            if (Customer == null )
                return NotFound("No customers found.");
            return Ok(Customer);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCustomer(int id)
        {
            var command= new DeleteCustomerCommand(id);
            await mediator.Send(command);
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> UpdateCustomer( UpdateCustomerCommand commad,int id)
        {
            
         
            commad.Id=id;
           
            return await mediator.Send(commad);
         
        }
       

    }

}
