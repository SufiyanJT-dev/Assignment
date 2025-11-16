using HotelBookingSystem.Appilcation.Payment.Dtos;
using HotelBookingSystem.Domain.Entities; // For Booking, BookingStatus
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // For getting API keys
using Razorpay.Api; // Add "dotnet add package Razorpay"
using System.Collections.Generic;

namespace HotelBookingSystem.Appilcation.Payment.Command
{
    public class InitiatePaymentCommandHandler : IRequestHandler<InitiatePaymentCommand, ActionResult<PaymentOrderResponseDto>>
    {
        private readonly HotelDbContext _context;
        private readonly IConfiguration _configuration;

        public InitiatePaymentCommandHandler(HotelDbContext hotelDbContext, IConfiguration configuration)
        {
            _context = hotelDbContext;
            _configuration = configuration;
        }

        public async Task<ActionResult<PaymentOrderResponseDto>> Handle(InitiatePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. TODO: Calculate TotalAmount securely
                decimal calculatedAmount = 1000.00m; // Example

                // 2. Create the Booking as 'Pending'
                var booking = new Domain.Entities.Booking
                {
                    CustomerId = request.CustomerId,
                    RoomId = request.RoomId,
                    CheckInDate = request.CheckInDate,
                    CheckOutDate = request.CheckOutDate,
                    Status = BookingStatus.Pending,
                    TotalAmount = calculatedAmount
                };
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync(cancellationToken);

                // 3. Create Razorpay Order
                var key = _configuration["Razorpay:Key"];
                var secret = _configuration["Razorpay:Secret"];
                RazorpayClient client = new RazorpayClient(key, secret);

                var options = new Dictionary<string, object>
                {
                    { "amount", (int)(booking.TotalAmount * 100) }, // Amount in paise
                    { "currency", "INR" },
                    { "receipt", $"booking_{booking.Id}" }
                };

                Order order = client.Order.Create(options);
                string razorpayOrderId = order.Attributes["id"];

                // 4. Send Order ID back to Angular
                var responseDto = new PaymentOrderResponseDto
                {
                    RazorpayOrderId = razorpayOrderId,
                    Amount = booking.TotalAmount,
                    BookingId = booking.Id
                };

                return new OkObjectResult(responseDto);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Unexpected error: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}