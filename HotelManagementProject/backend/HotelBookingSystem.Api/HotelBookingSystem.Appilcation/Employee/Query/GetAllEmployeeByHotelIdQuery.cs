using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Employee.Query
{
    public class GetAllEmployeeByHotelIdQuery:IRequest<List<HotelBookingSystem.Domain.Entities.Employee>>
    {
        public int HotelId { get; set; }
    }
}
