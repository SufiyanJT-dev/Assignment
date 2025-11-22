namespace HotelBookingSystem.Appilcation.Payment.Dtos
{
    public class PaymentOrderResponseDto
    {
        public string RazorpayOrderId { get; set; }
        public decimal Amount { get; set; }
        public int BookingId { get; set; }
    }
}