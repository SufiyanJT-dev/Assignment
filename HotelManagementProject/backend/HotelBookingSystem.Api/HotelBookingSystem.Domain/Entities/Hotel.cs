namespace HotelBookingSystem.Domain.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string path { get; set; }
        public List<Rooms> Room { get; set; }
        public List<Employee> Employees { get; set; }


    }
}
