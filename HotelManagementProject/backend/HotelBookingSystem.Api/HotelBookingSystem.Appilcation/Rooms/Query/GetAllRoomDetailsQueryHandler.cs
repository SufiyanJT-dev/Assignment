using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Rooms.Query
{
    public  class GetAllRoomDetailsQueryHandler : IRequestHandler<GetAllRoomQuery, List<Domain.Entities.Rooms>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetAllRoomDetailsQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<List<Domain.Entities.Rooms>> Handle(GetAllRoomQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Rooms> room = await hotelDbContext.Room.ToListAsync(cancellationToken);
            

            return room;
        }
    }
}
