using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.RoomType.Query
{
    public  class GetAllRoomTypeDetailsQueryHandler : IRequestHandler<GetAllRoomTypeQuery, List<Domain.Entities.RoomType>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetAllRoomTypeDetailsQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<List<Domain.Entities.RoomType>> Handle(GetAllRoomTypeQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.RoomType> roomTypes = await hotelDbContext.RoomTypes.ToListAsync(cancellationToken);
            

            return roomTypes;
        }
    }
}
