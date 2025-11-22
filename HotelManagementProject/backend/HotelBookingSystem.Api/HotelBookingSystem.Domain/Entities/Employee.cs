namespace HotelBookingSystem.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string password { get; set; }

        public Hotel Hotel { get; set; }
    }
    

}
