using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.RoomType.Command
{
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, ActionResult<string>>
    {
        private readonly HotelDbContext hotelDbContext;

        public UpdateRoomTypeCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<string>> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var roomType = await hotelDbContext.RoomTypes
                    .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

                if (roomType == null)
                {
                    return new NotFoundObjectResult("Room type ID not found");
                }

                // Update only if valid, else keep old value
                roomType.TypeName = !string.IsNullOrWhiteSpace(request.TypeName) && request.TypeName != "string"
                    ? request.TypeName
                    : roomType.TypeName;

                roomType.Description = !string.IsNullOrWhiteSpace(request.Description) && request.Description != "string"
                    ? request.Description
                    : roomType.Description;

                roomType.Capacity = request.Capacity > 0
                    ? request.Capacity
                    : roomType.Capacity;

                int result = await hotelDbContext.SaveChangesAsync(cancellationToken);
                if (result <= 0)
                {
                    return new BadRequestObjectResult("Update not successful");
                }

                return new OkObjectResult("Update successful");
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
