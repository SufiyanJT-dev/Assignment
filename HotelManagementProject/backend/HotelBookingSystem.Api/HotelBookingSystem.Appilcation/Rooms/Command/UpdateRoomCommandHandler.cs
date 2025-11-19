using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Rooms.Command
{
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomCommand, ActionResult<string>>
    {
        private readonly HotelDbContext hotelDbContext;

        public UpdateRoomTypeCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<string>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var room = await hotelDbContext.Room
                    .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

                if (room == null)
                {
                    return new NotFoundObjectResult("Room ID not found");
                }

                room.RoomNumber = !string.IsNullOrWhiteSpace(request.RoomNumber) && request.RoomNumber != "string"
                    ? request.RoomNumber
                    : room.RoomNumber;

                room.Status =
                        request.Status;
                       
                room.PricePerNight = request.PricePerNight > 0
                    ? request.PricePerNight
                    : room.PricePerNight;

                room.HotelId = request.HotelId > 0
                    ? request.HotelId
                    : room.HotelId;

                room.RoomTypeId = request.RoomTypeId > 0
                    ? request.RoomTypeId
                    : room.RoomTypeId;

                int result = await hotelDbContext.SaveChangesAsync(cancellationToken);
                if (result <= 0)
                {
                    return new BadRequestObjectResult("Update not successful");
                }

                return new OkObjectResult(room);
            }
            catch (DbUpdateException dbEx)
            {
                return new BadRequestObjectResult($"Database error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Unexpected error: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}
