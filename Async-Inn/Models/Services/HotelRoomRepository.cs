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
        public async Task<HotelRoom> Create(HotelRoom hotelRoom, int hotelId)
        {
            var existingHotelRoom = await _context.HotelRooms
               .FirstOrDefaultAsync(hr => hr.HotelId == hotelId && hr.RoomNumber == hotelRoom.RoomNumber);

            if (existingHotelRoom != null)
            {
                throw new InvalidOperationException("Hotel room already exists.");
            }
            var room = await _context.Rooms.FindAsync(hotelRoom.RoomId);
            var hotel = await _context.Hotels.FindAsync(hotelRoom.HotelId);

           // hotelRoom.Room= room;
           // hotelRoom.Hotel= hotel;
           hotelRoom.HotelId = hotelId;

             await _context.HotelRooms.AddAsync(hotelRoom);
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
       

        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            if (hotelRoom != null)
            {
                 _context.HotelRooms.Remove(hotelRoom);
                await _context.SaveChangesAsync();
            }
            else
                throw new InvalidOperationException("Hotel Room is not exist");

        }

        public async Task<List<HotelRoom>> Get(int hotelId)
        {
            var hotelRooms=await _context.HotelRooms.Where(hr => hr.HotelId == hotelId).ToListAsync();
            return hotelRooms;
                }

        public async Task<HotelRoom> GetById(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms
                .Include(room => room.Room)
                .ThenInclude(roomAmenities => roomAmenities.RoomAmenities)
                .ThenInclude(amenity => amenity.Amenity)
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
            HotelRoom existingHotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
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
