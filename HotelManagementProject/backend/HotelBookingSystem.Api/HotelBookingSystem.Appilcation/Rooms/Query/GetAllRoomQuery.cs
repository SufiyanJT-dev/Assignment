using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingSystem;
namespace HotelBookingSystem.Appilcation.Rooms.Query
{
    public  class GetAllRoomQuery  : IRequest<List<Domain.Entities.Rooms>>
    {
       
    }
}
