using HotelBookingSystem.Appilcation.Payment.Command;
using HotelBookingSystem.Domain.Entities; // For Booking, Payment, Statuses
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystem.Application.Payment.Command
{
    public class VerifyPaymentCommandHandler : IRequestHandler<VerifyPaymentCommand, ActionResult<string>>
    {
        private readonly HotelDbContext _context;
        private readonly IConfiguration _configuration;

        public VerifyPaymentCommandHandler(HotelDbContext hotelDbContext, IConfiguration configuration)
        {
            _context = hotelDbContext;
            _configuration = configuration;
        }

        public async Task<ActionResult<string>> Handle(VerifyPaymentCommand request, CancellationToken cancellationToken)
        {
            // 1. Fetch the 'Pending' booking
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == request.BookingId && b.Status == BookingStatus.Pending, cancellationToken);

            if (booking == null)
            {
                return new NotFoundObjectResult("Booking not found or is not pending.");
            }

            // 2. Prevent duplicate payment records
            var existingPayment = await _context.Payments
                .FirstOrDefaultAsync(p => p.RazorpayPaymentId == request.RazorpayPaymentId, cancellationToken);

            if (existingPayment != null)
            {
                return new BadRequestObjectResult("Payment already processed.");
            }

            // 3. Verify Razorpay Signature
            var isSignatureVerified = VerifySignature(request);

            // 4. Create the Payment record
            var payment = new Domain.Entities.Payment
            {
                BookingId = request.BookingId,
                PaymentDate = DateTime.UtcNow,
                Amount = booking.TotalAmount,
                Method = PaymentMethod.Online,
                RazorpayOrderId = request.RazorpayOrderId,
                RazorpayPaymentId = request.RazorpayPaymentId,
                RazorpaySignature = request.RazorpaySignature,
                Status = isSignatureVerified ? PaymentStatus.Paid : PaymentStatus.Failed
            };

            // 5. Update booking status
            booking.Status = isSignatureVerified ? BookingStatus.Confirmed : BookingStatus.Cancelled;

            // 6. Save changes atomically
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            // 7. Return result
            return isSignatureVerified
                ? new OkObjectResult(new {message = "Booking Confirmed!" })
                : new BadRequestObjectResult("Payment verification failed.");
        }


        private bool VerifySignature(VerifyPaymentCommand request)
        {
            try
            {
                var secret = _configuration["Razorpay:Secret"];
                var payload = request.RazorpayOrderId + "|" + request.RazorpayPaymentId;

                var expectedSignature = CalculateSHA256Hash(payload, secret);

                return expectedSignature == request.RazorpaySignature;
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine("Signature verification failed: " + ex.Message);
                return false;
            }
        }

        private string CalculateSHA256Hash(string text, string secret)
        {
            var encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(text);

            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                // Convert to hex string properly
                return string.Concat(hashmessage.Select(b => b.ToString("x2")));
            }
        }

    }
}
