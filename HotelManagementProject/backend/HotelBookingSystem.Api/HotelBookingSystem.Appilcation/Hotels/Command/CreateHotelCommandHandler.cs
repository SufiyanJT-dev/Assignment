using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Hotels.Command
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, ActionResult<string>>
    {
        private readonly HotelDbContext hotelDbContext;

        public CreateHotelCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
        public async Task<ActionResult<string>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name) ||
                    string.IsNullOrWhiteSpace(request.Address) ||
                    string.IsNullOrWhiteSpace(request.City) ||
                    string.IsNullOrWhiteSpace(request.Description) ||
                    string.IsNullOrWhiteSpace(request.PhoneNumber) ||
                    string.IsNullOrWhiteSpace(request.Country) ||
                    request.Image == null || request.Image.Length == 0)
                {
                    return new BadRequestObjectResult("All fields including image are required.");
                }

               
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
                var filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await request.Image.CopyToAsync(stream);

                var hotel = new Domain.Entities.Hotel
                {
                    Description = request.Description,
                    path = $"/images/{fileName}", 
                    Name = request.Name,
                    Address = request.Address,
                    City = request.City,
                    PhoneNumber = request.PhoneNumber,
                    Country = request.Country
                };

                hotelDbContext.Hotels.Add(hotel);
                int result = await hotelDbContext.SaveChangesAsync(cancellationToken);

                if (result <= 0)
                    return new BadRequestObjectResult("Hotel was not added. Please try again.");

                return new OkObjectResult(hotel);
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Unexpected error: {ex.Message}") { StatusCode = 500 };
            }
        }



    }
}
