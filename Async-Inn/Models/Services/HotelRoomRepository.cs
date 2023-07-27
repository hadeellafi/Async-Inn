using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private readonly AsyncInnDbContext _context;

        public HotelRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<HotelRoom> Create(HotelRoom hotelRoom)
        {
            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
        /* public async Task<HotelRoom> Create(HotelRoom hotelRoom)
        {
            // Check if the hotel room already exists in the database
            var existingHotelRoom = await _context.HotelRooms
                .FirstOrDefaultAsync(hr => hr.HotelId == hotelRoom.HotelId && hr.RoomNumber == hotelRoom.RoomNumber);

            if (existingHotelRoom != null)
            {
                throw new InvalidOperationException("Hotel room already exists.");
            }

            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
            return hotelRoom;
        }*/

        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await GetById(hotelId, roomNumber);
            if (hotelRoom != null)
            {
                 _context.HotelRooms.Remove(hotelRoom);
                await _context.SaveChangesAsync();
            }
            else
                throw new InvalidOperationException("Hotel Room is not exist");

        }

        public async Task<List<HotelRoom>> Get()
        {
            var hotelRooms=await _context.HotelRooms.ToListAsync();
            return hotelRooms;
                }

        public async Task<HotelRoom> GetById(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms
                .FirstOrDefaultAsync(hr => hr.HotelId == hotelId && hr.RoomNumber == roomNumber);

            if (hotelRoom != null)
            {
                return hotelRoom;
            }
            else
            {
                throw new InvalidOperationException("Hotel Room not found.");
            }
        }


        public async Task<HotelRoom> Update(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            HotelRoom existingHotelRoom = await GetById(hotelId, roomNumber);
            if (existingHotelRoom != null)
            {
                // Update all properties of existingHotelRoom from hotelRoom
                _context.Entry(existingHotelRoom).CurrentValues.SetValues(hotelRoom);

                _context.Entry(existingHotelRoom).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return existingHotelRoom;
            }
            else
            {
                throw new InvalidOperationException("Hotel Room does not exist.");
            }
        }

    }
}
