using HotelBookingSystem.Appilcation.Payment.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HotelBookingSystem.Appilcation.Payment.Command
{
    public class InitiatePaymentCommand : IRequest<ActionResult<PaymentOrderResponseDto>>
    {
        // These fields are needed to create the booking
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        // Amount will be calculated on the server
    }
}