using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.RoomType.Dtos
{
    public class RoomTypeGetByIdDtos
    {
        public string TypeName { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }

    }
}
