using HotelBookingSystem.Appilcation.RoomType.Query;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Hotels.Query
{
    public  class GetAllHotelDetailsQueryHandler : IRequestHandler<GetAllHotelDetailsQuery, List<Domain.Entities.Hotel>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetAllHotelDetailsQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<List<Hotel>> Handle(GetAllHotelDetailsQuery request, CancellationToken cancellationToken)
        {
           List<Hotel>  hotel =await  hotelDbContext.Hotels.ToListAsync();
            

            return  hotel;
        }
    }
}
