namespace HotelBookingSystem.Domain.Entities
{
    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed
    }

    public enum PaymentMethod
    {
        Cash,
        Card,
        UPI,
        Online
    }
    public class Payment
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public string RazorpayOrderId { get; set; }
        public string RazorpayPaymentId { get; set; }
        public string RazorpaySignature { get; set; }
        public Booking Booking { get; set; }
    }

}
