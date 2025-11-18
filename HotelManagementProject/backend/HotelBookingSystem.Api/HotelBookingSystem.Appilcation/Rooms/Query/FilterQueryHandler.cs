using HotelBookingSystem.Appilcation.Rooms.Query;
using HotelBookingSystem.Domain.Entities;
using HotelBookingSystem.Infrastructure.Data;
using MediatR;

using Microsoft.EntityFrameworkCore;
namespace HotelBookingSystem.Appilcation.Rooms.Query
    { 
public class FilterQueryHandler : IRequestHandler<FilterQuery, List<Domain.Entities.Rooms>>
{
    private readonly HotelDbContext hotelDbContext;

    public FilterQueryHandler(HotelDbContext hotelDbContext)
    {
        this.hotelDbContext = hotelDbContext;
    }

    public Task<List<Domain.Entities.Rooms>> Handle(FilterQuery request, CancellationToken cancellationToken)
    {
        // Get all room IDs that are booked during the requested period
        var bookedRoomIds = hotelDbContext.Bookings
            .Where(b => request.checkInDate < b.CheckOutDate && request.checkOutDate > b.CheckInDate && b.Status != BookingStatus.Confirmed)
            .Select(b => b.RoomId)
            .ToList();

        // Filter rooms based on location and availability
        var rooms = hotelDbContext.Room
            .Include(r => r.hotel)
            .Where(r =>
                (EF.Functions.Like(r.hotel.City, $"%{request.location}%") ||
                 EF.Functions.Like(r.hotel.Country, $"%{request.location}%")) &&
                !bookedRoomIds.Contains(r.Id))
            .ToList();

        return Task.FromResult(rooms);
    }
}
}