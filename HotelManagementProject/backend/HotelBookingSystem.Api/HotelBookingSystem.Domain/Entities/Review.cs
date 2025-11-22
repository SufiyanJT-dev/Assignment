namespace HotelBookingSystem.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        
        public Hotel Hotel { get; set; }
        public Customer Customer { get; set; }
    }

}
