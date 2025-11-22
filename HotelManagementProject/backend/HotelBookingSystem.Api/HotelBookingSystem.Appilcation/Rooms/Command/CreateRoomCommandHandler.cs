using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Rooms.Command
{
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomCommand, ActionResult<Domain.Entities.Rooms>>
    {
        private readonly HotelDbContext hotelDbContext;

        public CreateRoomTypeCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<Domain.Entities.Rooms>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate required fields
                if (
                    request.PricePerNight <= 0 ||
                    request.HotelId <= 0 ||
                    request.RoomTypeId <= 0)
                {
                    return new BadRequestObjectResult("All fields are required and must be valid.");
                }

                var room = new Domain.Entities.Rooms
                {
                    RoomNumber = request.RoomNumber,
                    Status = request.Status,
                    PricePerNight = request.PricePerNight,
                    HotelId = request.HotelId,
                    RoomTypeId = request.RoomTypeId
                };

                await hotelDbContext.Room.AddAsync(room, cancellationToken);
                int result = await hotelDbContext.SaveChangesAsync(cancellationToken);

                if (result <= 0)
                {
                    return new BadRequestObjectResult("Room was not added. Please try again.");
                }

                return new OkObjectResult(room);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Unexpected error: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}
