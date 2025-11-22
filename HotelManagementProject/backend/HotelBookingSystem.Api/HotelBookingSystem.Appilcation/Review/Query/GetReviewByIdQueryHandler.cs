using HotelBookingSystem.Appilcation.Review.Dtos;
using HotelBookingSystem.Appilcation.Hotels.Query;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Appilcation.Review.Query
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ActionResult<Dtos.ReviewGetByIdDtos>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetReviewByIdQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<Dtos.ReviewGetByIdDtos>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Review reviewGetById = await hotelDbContext.Review.FirstOrDefaultAsync(h => h.Id == request.Id);
            if (reviewGetById == null) {
                return new NotFoundObjectResult("Review Not Found");
            }
            Dtos.ReviewGetByIdDtos hotelGetByIDDtos =new ReviewGetByIdDtos();
            hotelGetByIDDtos.ReviewDate=reviewGetById.ReviewDate;
            hotelGetByIDDtos.HotelId = reviewGetById.HotelId;
            hotelGetByIDDtos.CustomerId = reviewGetById.CustomerId;
            hotelGetByIDDtos.Comment=reviewGetById.Comment;
            hotelGetByIDDtos.Rating=reviewGetById.Rating;
            return new  OkObjectResult(hotelGetByIDDtos);

        }

        //public async Task<string> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        //{
    

        //}

    }
}
