using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Customer.Query
{
    public class GetAllCustomersQuery:IRequest<List<Dtos.AddCustomerDtos>>
    {
    }
}
