using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Rooms.Command
{
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomCommand, ActionResult<string>>
    {
        private readonly HotelDbContext hotelDbContext;

        public DeleteRoomTypeCommandHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
        public async  Task<ActionResult<string>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Rooms room= await hotelDbContext.Room.FirstOrDefaultAsync(r=>r.Id==request.Id);
            if (room == null) {
                return new NotFoundObjectResult("not found");
            }
            hotelDbContext.Room.Remove(room);
           int a= hotelDbContext.SaveChanges();
            if (a < 0) {
                return "Failed To delete";
            }
            return new OkObjectResult(room);
        }
    }
}
