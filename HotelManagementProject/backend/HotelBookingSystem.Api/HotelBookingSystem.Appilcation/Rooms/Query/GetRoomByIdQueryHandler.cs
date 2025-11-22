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

namespace HotelBookingSystem.Appilcation.Rooms.Query
{
    public class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomByIdQuery,ActionResult<Dtos.RoomGetByIdDtos>>
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

        async Task<ActionResult<Dtos.RoomGetByIdDtos>> IRequestHandler<GetRoomByIdQuery, ActionResult<Dtos.RoomGetByIdDtos>>.Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Rooms room = await hotelDbContext.Room.FirstOrDefaultAsync(R => R.Id == request.Id);
            if (room == null)
            {
                return new NotFoundObjectResult("Room not found");
            }
            Dtos.RoomGetByIdDtos roomGetByIdDtos = new Dtos.RoomGetByIdDtos()
            {
                RoomNumber = room.RoomNumber,
                RoomTypeId = room.RoomTypeId,
                HotelId = room.HotelId,
                PricePerNight = room.PricePerNight,
                Status = room.Status,
                

            };

            return roomGetByIdDtos;
            
        }

        //public async Task<string> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        //{


        //}

    }
}
