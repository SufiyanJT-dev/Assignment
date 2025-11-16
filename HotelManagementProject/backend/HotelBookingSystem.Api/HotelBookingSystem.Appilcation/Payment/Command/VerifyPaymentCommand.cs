using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Appilcation.Payment.Command
{
    public class VerifyPaymentCommand : IRequest<ActionResult<string>>
    {
        public string RazorpayOrderId { get; set; }
        public string RazorpayPaymentId { get; set; }
        public string RazorpaySignature { get; set; }
        public int BookingId { get; set; }
    }
}