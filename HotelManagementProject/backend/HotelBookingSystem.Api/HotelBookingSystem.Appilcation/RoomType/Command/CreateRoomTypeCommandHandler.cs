using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.RoomType.Command
{
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, ActionResult<string>>
    {
        private readonly HotelDbContext hotelDbContext;

        public CreateRoomTypeCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<string>> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(request.TypeName) ||
                    string.IsNullOrWhiteSpace(request.Description) ||
                    request.Capacity <= 0)
                {
                    return new BadRequestObjectResult("All fields are required and must be valid.");
                }

                var roomType = new Domain.Entities.RoomType
                {
                    TypeName = request.TypeName,
                    Description = request.Description,
                    Capacity = request.Capacity
                };

                await hotelDbContext.RoomTypes.AddAsync(roomType, cancellationToken);
                int result = await hotelDbContext.SaveChangesAsync(cancellationToken);

                if (result <= 0)
                {
                    return new BadRequestObjectResult("Room type was not added. Please try again.");
                }

                return new OkObjectResult("Room type added successfully.");
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Unexpected error: {ex.Message}") { StatusCode = 500 };
            }
        }
    }
}
