using Async_Inn.Data;
using Async_Inn.Models.DTO;
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

        public async Task<RoomDTO> Create(RoomDTO room)
        {
            var roomEntity = new Room()
            {
                Name = room.Name,
                RoomLayout = room.RoomLayout
            };
            _context.Rooms.Add(roomEntity);
            await _context.SaveChangesAsync();
            room.Id = roomEntity.Id;
            return room;
        }

        public async Task Delete(int id)
        {
            Room existingRoom = await _context.Rooms.FindAsync(id);
            if (existingRoom != null)
            {
                Room room = await _context.Rooms.FindAsync(id);
                _context.Entry(room).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }

            else
            {
                throw new InvalidOperationException("room does not exist.");
            }
            
         }

        public async Task<RoomDTO> GetById(int roomId)
        {
            var roomDTO = await _context.Rooms
                .Where(a => a.Id == roomId)
                .Select(a => new RoomDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    RoomLayout = a.RoomLayout, 
                    Amenities = a.RoomAmenities
                        .Select(ra => new AmenityDTO
                        {
                            Id = ra.Amenity.Id,
                            Name = ra.Amenity.Name
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return roomDTO;
        }


        public async Task<List<RoomDTO>> GetRooms()
        {
            var rooms = await _context.Rooms
                .Include(x => x.RoomAmenities)
                .ThenInclude(y => y.Amenity)
                .ToListAsync();

            var roomDTOs = rooms.Select(r => new RoomDTO
            {
                Id = r.Id,
                Name = r.Name,
                RoomLayout = r.RoomLayout,
                Amenities = r.RoomAmenities
                    .Select(ra => new AmenityDTO
                    {
                        Id = ra.Amenity.Id,
                        Name = ra.Amenity.Name
                    })
                    .ToList()
            }).ToList();

            return roomDTOs;
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

        public async Task<RoomDTO> Update(int id, RoomDTO room)
        {
            Room existingRoom = await _context.Rooms.FindAsync(id);
            if (existingRoom != null)
            {
                existingRoom.Id = room.Id;
                existingRoom.Name = room.Name;
                existingRoom.RoomLayout = room.RoomLayout;

                await _context.SaveChangesAsync();
                return room;
            }

            else
            {
                throw new InvalidOperationException("room does not exist.");
            }
        }
    }
}
