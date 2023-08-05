using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.DTO;
using Async_Inn.Models.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAsync
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
       protected readonly AsyncInnDbContext _db;

        public Mock()
        {
            _connection=new SqliteConnection("Filename=:memory:");
            _connection.Open();

           _db=new AsyncInnDbContext(
               new DbContextOptionsBuilder<AsyncInnDbContext>()
               .UseSqlite(_connection).Options);

            _db.Database.EnsureCreated();

        }
       
        protected async Task<Amenity> CreateAndSaveTestAmenity()
        {
            var amenity=new Amenity() { Name="Aminty1" };
            _db.Add(amenity);
            await _db.SaveChangesAsync();

            //Assert.NotEqual(0, amenity.Id);

            return amenity;

        }
        protected async Task<Room> CreateAndSaveTestRoom()
        {
            var room = new Room() 
            {
                Name = "Test Room",
                RoomLayout = Layout.OneBedroom
            };
            _db.Add(room);
            await _db.SaveChangesAsync();
            return room;
        }
        protected async Task<Hotel> CreateAndSaveTestHotel()
        {
            var hotel = new Hotel()
            {
                Name = "Test Room",
                City = "Test City",
                State = "Test State",
                Address = "Test Address",
                PhoneNumber = "Test Phone Number"
            };
            _db.Add(hotel);
            await _db.SaveChangesAsync();
            return hotel;
        }
        public void Dispose()
        {

            _db?.Dispose();
            //using ? it equivalent to : if(db!=null){db.Dispose();}

            _connection?.Dispose();
        }
    }
}
