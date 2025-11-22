using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Hotels.Command
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, string>
    {
        private readonly HotelDbContext hotelDbContext;

        public UpdateHotelCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<string> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await hotelDbContext.Hotels.FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);
            if (hotel == null)
            {
                return "Hotel ID not found";
            }

            
            hotel.Name = !string.IsNullOrWhiteSpace(request.Name) && request.Name != "string"
                ? request.Name
                : hotel.Name;

            hotel.Address = !string.IsNullOrWhiteSpace(request.Address) && request.Address != "string"
                ? request.Address
                : hotel.Address;

            hotel.City = !string.IsNullOrWhiteSpace(request.City) && request.City != "string"
                ? request.City
                : hotel.City;

            hotel.Country = !string.IsNullOrWhiteSpace(request.Country) && request.Country != "string"
                ? request.Country
                : hotel.Country;

            hotel.PhoneNumber = !string.IsNullOrWhiteSpace(request.PhoneNumber) && request.PhoneNumber != "string"
                ? request.PhoneNumber
                : hotel.PhoneNumber;
            hotel.Description = !string.IsNullOrWhiteSpace(request.Description) && request.Description != "string"
                ? request.Description
                : hotel.Description;
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            
            if (request.Image != null)
            {
                
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
                var filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await request.Image.CopyToAsync(stream, cancellationToken);

                hotel.path = $"/images/{fileName}";
            }

            int result = await hotelDbContext.SaveChangesAsync(cancellationToken);
            return result > 0 ? "Update Successful" : "Update Not Successful";
        }
    }
}
