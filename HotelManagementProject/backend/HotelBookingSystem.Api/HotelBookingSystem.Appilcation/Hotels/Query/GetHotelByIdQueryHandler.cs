using HotelBookingSystem.Appilcation.Hotels.Dtos;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Hotels.Query
{
    public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, ActionResult<Dtos.HotelTypeGetByIdDtos>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetHotelByIdQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        public async Task<ActionResult<HotelTypeGetByIdDtos>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Hotel hotelUpdatedetails = await hotelDbContext.Hotels.FirstOrDefaultAsync(h => h.Id == request.Id);
            if (hotelUpdatedetails == null) {
                return null;
            }
            Dtos.HotelTypeGetByIdDtos hotelGetByIDDtos =new HotelTypeGetByIdDtos();
            hotelGetByIDDtos.PhoneNumber = hotelUpdatedetails.PhoneNumber;
            hotelGetByIDDtos.Address = hotelUpdatedetails.Address;
            hotelGetByIDDtos.City = hotelUpdatedetails.City;
            hotelGetByIDDtos.Name= hotelUpdatedetails.Name;
            hotelGetByIDDtos.Country = hotelUpdatedetails.Country;
            return hotelGetByIDDtos;

        }

        //public async Task<string> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        //{
    

        //}

    }
}
