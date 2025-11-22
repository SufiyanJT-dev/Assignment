namespace HotelBookingSystem.Domain.Entities
{
    public enum BookingStatus
    {
        Pending=1,
        Confirmed=2,
        Cancelled=3,
        Completed=4
    }

   
    public class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalAmount { get; set; }

      
        public Customer Customer { get; set; }
        public Rooms Room { get; set; }
        public Payment Payment { get; set; }
    }
    

}
