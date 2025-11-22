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
    public class GetAllRoomsByHotelIdQueryHandler : IRequestHandler<GetAllRoomsByHotelIdQuery, List<Domain.Entities.Rooms>>
    {
        private readonly DbContext hotelDbContext;

        public GetAllRoomsByHotelIdQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }
        public Task<List<Domain.Entities.Rooms>> Handle(GetAllRoomsByHotelIdQuery request, CancellationToken cancellationToken)
        {
            return hotelDbContext.Set<Domain.Entities.Rooms>()
                .Where(r => r.HotelId == request.HotelId )
                .ToListAsync(cancellationToken);
        }
    }
}
