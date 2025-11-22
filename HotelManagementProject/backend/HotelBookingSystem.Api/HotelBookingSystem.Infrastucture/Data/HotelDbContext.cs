using Microsoft.Extensions.Options;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using HotelBookingSystem.Domain.Entities;

namespace HotelBookingSystem.Infrastructure.Data
{
    public class HotelDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=HotelSystem;User Id=sa;Password=Sufiyan@123;TrustServerCertificate=True;");

        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Rooms> Room { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().Property(r => r.TotalAmount).HasPrecision(10, 2);
            modelBuilder.Entity<Payment>().Property(r => r.Amount).HasPrecision(10, 2);
            modelBuilder.Entity<Rooms>().Property(r => r.PricePerNight).HasPrecision(10, 2);
        }

    }
}