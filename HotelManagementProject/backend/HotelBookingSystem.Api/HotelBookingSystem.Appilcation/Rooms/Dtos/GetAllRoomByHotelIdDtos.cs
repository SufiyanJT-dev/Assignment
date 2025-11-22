using HotelBookingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotelBookingSystem.Domain.Entities.Rooms;

namespace HotelBookingSystem.Appilcation.Rooms.Dtos
{
    public class GetAllRoomByHotelIdDtos
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public RoomStatus Status { get; set; }
        public decimal PricePerNight { get; set; }
     
        public HotelBookingSystem.Domain.Entities.RoomType RoomType { get; set; }

    }
}
