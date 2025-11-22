using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Employee.Command
{
    public class DeleteEmployeeCommand : IRequest<ActionResult<string>>
    {
        public int Id { get; set; }
    }
}
