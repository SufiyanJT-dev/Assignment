using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.RoomType.Command
{
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, string>
    {
        private readonly HotelDbContext hotelDbContext;

        public DeleteRoomTypeCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
        public async  Task<string> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.RoomType roomType= await hotelDbContext.RoomTypes.FirstOrDefaultAsync(r=>r.Id==request.Id);
            if (roomType == null) {
                return "Room Type Not Found";
            }
            hotelDbContext.RoomTypes.Remove(roomType);
           int a= hotelDbContext.SaveChanges();
            if (a < 0) {
                return "Failed To delete";
            }
            return $"{request.Id} deleted";
        }
    }
}
