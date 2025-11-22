using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.RoomType.Command
{
    public class DeleteRoomTypeCommand:IRequest<string>
    {
        public int Id { get; set; }
    }
}
