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

namespace HotelBookingSystem.Appilcation.Review.Query
{
    public  class GetAllReviewDetailsQueryHandler : IRequestHandler<GetAllReviewDetailsQuery, List<Domain.Entities.Review>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetAllReviewDetailsQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<List<Domain.Entities.Review>> Handle(GetAllReviewDetailsQuery request, CancellationToken cancellationToken)
        {
           List<Domain.Entities.Review>  reviews =await  hotelDbContext.Review.ToListAsync();
            

            return reviews;
        }
    }
}
