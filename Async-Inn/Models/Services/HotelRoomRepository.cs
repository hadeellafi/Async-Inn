using Async_Inn.Data;
using Async_Inn.Models.DTO;
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
        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int hotelId)
        {
            var existingHotelRoom = await _context.HotelRooms
                .FirstOrDefaultAsync(hr => hr.HotelId == hotelId && hr.RoomNumber == hotelRoom.RoomNumber);

            if (existingHotelRoom != null)
            {
                throw new InvalidOperationException("Hotel room already exists.");
            }

            // Find the Room and Hotel entities by their respective Ids
            var room = await _context.Rooms.FindAsync(hotelRoom.RoomId);
            var hotel = await _context.Hotels.FindAsync(hotelId);

            if (room == null)
            {
                throw new InvalidOperationException("Room not found.");
            }

            if (hotel == null)
            {
                throw new InvalidOperationException("Hotel not found.");
            }

            var newHotelRoom = new HotelRoom
            {
                HotelId = hotelId,
                RoomId = hotelRoom.RoomId,
                RoomNumber = hotelRoom.RoomNumber,
                IsPetFriendly = hotelRoom.IsPetFriendly,
                Rate = hotelRoom.Rate
            };

            await _context.HotelRooms.AddAsync(newHotelRoom);

            await _context.SaveChangesAsync();
            hotelRoom.HotelId = newHotelRoom.HotelId;

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

        public async Task<List<HotelRoomDTO>> Get(int hotelId)
        {
            var hotelRooms = await _context.HotelRooms
                .Where(hr => hr.HotelId == hotelId)
                .Include(hr => hr.Room)
                    .ThenInclude(r => r.RoomAmenities)
                        .ThenInclude(ra => ra.Amenity)
                .Select(hr => new HotelRoomDTO
                {
                    HotelId = hr.HotelId,
                    RoomNumber = hr.RoomNumber,
                    Rate = hr.Rate,
                    IsPetFriendly = hr.IsPetFriendly,
                    RoomId = hr.RoomId,
                    Room = new RoomDTO
                    {
                        Id = hr.Room.Id,
                        Name = hr.Room.Name,
                        RoomLayout = hr.Room.RoomLayout,
                        Amenities = hr.Room.RoomAmenities.Select(ra => new AmenityDTO
                        {
                            Id = ra.Amenity.Id,
                            Name = ra.Amenity.Name
                        }).ToList()
                    }
                })
                .ToListAsync();

            return hotelRooms;
        }


        public async Task<HotelRoomDTO> GetById(int hotelId, int roomNumber)
        {
            // Use Include and ThenInclude to eagerly load related data
            var hotelRoom = await _context.HotelRooms
                .Include(room => room.Room) // Include the related Room for the HotelRoom
                    .ThenInclude(roomAmenities => roomAmenities.RoomAmenities) // Include the related RoomAmenities for the Room
                        .ThenInclude(amenity => amenity.Amenity) // Include the related Amenity for each RoomAmenity
                .FirstOrDefaultAsync(hr => hr.HotelId == hotelId && hr.RoomNumber == roomNumber);

            if (hotelRoom != null)
            {
                var hotelRoomDTO = new HotelRoomDTO
                {
                    HotelId = hotelRoom.HotelId,
                    RoomNumber = hotelRoom.RoomNumber,
                    Rate = hotelRoom.Rate,
                    IsPetFriendly = hotelRoom.IsPetFriendly,
                    RoomId = hotelRoom.RoomId,
                    Room = new RoomDTO
                    {
                        Id = hotelRoom.Room.Id,
                        Name = hotelRoom.Room.Name,
                        RoomLayout = hotelRoom.Room.RoomLayout,
                        Amenities = hotelRoom.Room.RoomAmenities.Select(ra => new AmenityDTO
                        {
                            Id = ra.Amenity.Id,
                            Name = ra.Amenity.Name
                        }).ToList()
                    }
                };
                return hotelRoomDTO;
            }
            else
            {
                throw new InvalidOperationException("Hotel Room not found.");
            }
        }


        public async Task<HotelRoomDTO> Update(int hotelId, int roomNumber, HotelRoomDTO hotelRoomDTO)
        {
            var existingHotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            if (existingHotelRoom != null)
            {
                existingHotelRoom.HotelId = hotelRoomDTO.HotelId;
                existingHotelRoom.RoomId = hotelRoomDTO.RoomId;
                existingHotelRoom.RoomNumber = hotelRoomDTO.RoomNumber;
                existingHotelRoom.Rate = hotelRoomDTO.Rate;
                existingHotelRoom.IsPetFriendly = hotelRoomDTO.IsPetFriendly;

                await _context.SaveChangesAsync();
                return hotelRoomDTO;
            }

            else
            {
                throw new InvalidOperationException("Hotel Room does not exist.");
            }
        }
           
        }

    }

