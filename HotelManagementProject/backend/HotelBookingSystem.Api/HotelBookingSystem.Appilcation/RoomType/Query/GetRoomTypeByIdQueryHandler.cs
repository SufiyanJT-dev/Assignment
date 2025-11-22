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

namespace HotelBookingSystem.Appilcation.RoomType.Query
{
    public class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomTypeByIdQuery, ActionResult<Dtos.RoomTypeGetByIdDtos>>
    {
        private readonly HotelDbContext hotelDbContext;

        public GetRoomTypeByIdQueryHandler(HotelDbContext hotelDbContext)
        {
            this.hotelDbContext = hotelDbContext;
        }

        //public async Task<RoomTypeGetByIdDtos> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        //{
        //    Domain.Entities.Hotel hotelUpdatedetails = await hotelDbContext.Hotels.FirstOrDefaultAsync(h => h.Id == request.Id);
        //    if (hotelUpdatedetails == null) {
        //        return null;
        //    }
        //    Dtos.RoomTypeGetByIdDtos hotelGetByIDDtos=new RoomTypeGetByIdDtos();
        //    hotelGetByIDDtos.PhoneNumber = hotelUpdatedetails.PhoneNumber;
        //    hotelGetByIDDtos.Address = hotelUpdatedetails.Address;
        //    hotelGetByIDDtos.City = hotelUpdatedetails.City;
        //    hotelGetByIDDtos.Name= hotelUpdatedetails.Name;
        //    hotelGetByIDDtos.Country = hotelUpdatedetails.Country;
        //    return hotelGetByIDDtos;

        //}

        async Task<ActionResult<Dtos.RoomTypeGetByIdDtos>> IRequestHandler<GetRoomTypeByIdQuery, ActionResult<Dtos.RoomTypeGetByIdDtos>>.Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.RoomType roomType = await hotelDbContext.RoomTypes.FirstOrDefaultAsync(R => R.Id == request.Id);
            if(roomType == null)
            {
                return new NotFoundObjectResult($"Room Type with ID {request.Id} not found.");
            }
            Dtos.RoomTypeGetByIdDtos roomTypeGetByIdDtos = new Dtos.RoomTypeGetByIdDtos()
            {
                TypeName = roomType.TypeName,
                Description = roomType.Description,
                Capacity = roomType.Capacity,
            };
            return roomTypeGetByIdDtos;
            
        }

        //public async Task<string> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        //{


        //}

    }
}
