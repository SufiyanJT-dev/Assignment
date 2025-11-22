using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingSystem;
namespace HotelBookingSystem.Appilcation.RoomType.Query
{
    public  class GetAllRoomTypeQuery  : IRequest<List<Domain.Entities.RoomType>>
    {
       
    }
}
