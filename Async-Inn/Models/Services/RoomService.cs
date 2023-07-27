using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class RoomService : IRoom
    {
        private readonly AsyncInnDbContext _context;

        public RoomService(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            // check if room & amenity exists

            Room room= await _context.Rooms.FindAsync(roomId);
            Amenity amenity=await _context.Amenities.FindAsync(amenityId);

            if (room == null || amenity == null)
            {
                throw new InvalidOperationException("Room or Amenity not found.");
            }
            // check if the record is already exist or not
            if(!_context.RoomAmenities.Any(row=>row.RoomId == roomId&&row.AmenityId==amenityId))
            {
                _context.RoomAmenities.Add(new RoomAmenity { RoomId=roomId,AmenityId=amenityId });
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new InvalidOperationException("the amenity of this room is already exists.");

            }

        }

        public async Task<Room> Create(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task Delete(int id)
        {
            Room room=await GetById(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
         }

        public async Task<Room> GetById(int roomId)
        {
            Room room = await _context.Rooms
                   .Include(r => r.RoomAmenities)
                       .ThenInclude(ra => ra.Amenity)
                   .FirstOrDefaultAsync(r => r.Id == roomId);
            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _context.Rooms.Include(x => x.RoomAmenities)
                                          .ThenInclude(y => y.Amenity)
                                          .ToListAsync();
            return rooms;
        }

        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            RoomAmenity roomAmenityToRemove= await _context.RoomAmenities.FindAsync(roomId,amenityId);
            if( roomAmenityToRemove!=null)
            {
                _context.RoomAmenities.Remove(roomAmenityToRemove);
                await _context.SaveChangesAsync();
            }
            else
                throw new InvalidOperationException("RoomAmenity is no exist");

        }

        public async Task<Room> Update(int id, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return room;
        }
    }
}
