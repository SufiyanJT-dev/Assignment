using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingSystem;
namespace HotelBookingSystem.Appilcation.Employee.Query
{
    public  class GetAllEmployeeQuery : IRequest<List<Domain.Entities.Employee>>
    {
       
    }
}
