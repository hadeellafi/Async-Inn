using Async_Inn.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Async_Inn.Data
{
    //IdentityDbContext<ApplicationUser> 
   //  This will provide the necessary tables and infrastructure for managing user authentication
    public class AsyncInnDbContext: IdentityDbContext<ApplicationUser>
    {
        // constructer will take paramenter of type Dbcontextoptions and will base it to base
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, and Identity needs it
            //By calling base.OnModelCreating(modelBuilder), you ensure that the default configuration for identity-related entities
            //is applied first, and then you can customize and extend it as needed for your application's requirements.

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Hotel A",
                    City = "City A",
                    State = "State A",
                    Address = "Address A",
                    PhoneNumber = "07998883014"
                },
        new Hotel
        {
            Id = 2,
            Name = "Hotel B",
            City = "City B",
            State = "State B",
            Address = "Address B",
            PhoneNumber = "0799783014"
        },
         new Hotel
         {
             Id = 3,
             Name = "Hotel C",
             City = "City C",
             State = "State C",
             Address = "Address C",
             PhoneNumber = "07978883014"
         });
            // Seed data for Amenities
            modelBuilder.Entity<Amenity>().HasData(
                new Amenity { Id = 1, Name = "Amenity 1" },
                new Amenity { Id = 2, Name = "Amenity 2" },
                new Amenity { Id = 3, Name = "Amenity 3" }
            );

            // Seed data for Rooms
            modelBuilder.Entity<Room>().HasData(
             new Room { Id = 1, Name = "Room 1", RoomLayout = Layout.Studio },
             new Room { Id = 2, Name = "Room 2", RoomLayout = Layout.OneBedroom },
             new Room { Id = 3, Name = "Room 3", RoomLayout = Layout.TwoBedroom }
            );

            modelBuilder.Entity<RoomAmenity>().HasKey(
               roomAmenity => new
               {
                   roomAmenity.RoomId,
                   roomAmenity.AmenityId
               }
               );

            modelBuilder.Entity<HotelRoom>().HasKey(
               hotelRoom => new
               {
                   hotelRoom.HotelId,
                   hotelRoom.RoomNumber
               }
               );
        }
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Amenity> Amenities { get; set; }

        public DbSet<RoomAmenity> RoomAmenities { get; set; }

        public DbSet<HotelRoom> HotelRooms { get; set; }


    }
}
