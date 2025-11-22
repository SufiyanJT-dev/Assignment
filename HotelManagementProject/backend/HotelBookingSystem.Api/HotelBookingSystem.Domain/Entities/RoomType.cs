namespace HotelBookingSystem.Domain.Entities
{
    using System.Collections.Generic;

    public class RoomType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }

       
        public List<Rooms> Room { get; set; }
    }

   
}
