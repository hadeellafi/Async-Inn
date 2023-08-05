using Async_Inn.Data;
using Async_Inn.Models.DTO;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Async_Inn.Models.Services
{
    public class HotelService : IHotel
    {
        private readonly AsyncInnDbContext _context;

        public HotelService(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<HotelDTO> Create(HotelDTO hotel)
        {
            Hotel hotelEntity = new Hotel()
            {
                Name = hotel.Name,
                City = hotel.City,
                State = hotel.State,
                Address = hotel.Address,
                PhoneNumber = hotel.PhoneNumber,
            };
            _context.Hotels.Add(hotelEntity);
            await _context.SaveChangesAsync();
            hotel.Id = hotelEntity.Id;
            return hotel;

        }

        public async Task Delete(int id)
        {
            Hotel hotel =await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {

                _context.Entry(hotel).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Hotel was not found.");
            }
        }

        public async Task<HotelDTO> GetById(int hotelId)
        {
            HotelDTO hotel = await _context.Hotels
                .Include(h => h.HotelRooms) // Include the related HotelRooms for the Hotel
                .ThenInclude(hr => hr.Room) // Include the Room for each HotelRoom
                .ThenInclude(r => r.RoomAmenities) // Include the related RoomAmenities for each Room
                .Where(h => h.Id == hotelId)
                .Select(h => new HotelDTO
                {
                    Id = h.Id,
                    Name = h.Name,
                    City = h.City,
                    State = h.State,
                    Address = h.Address,
                    PhoneNumber = h.PhoneNumber,
                    HotelRooms = h.HotelRooms.Select(hr => new HotelRoomDTO
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
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return hotel;
        }



        public async Task<List<HotelDTO>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();

            var hotelDTOs = hotels.Select(hotel => new HotelDTO
            {
                Id=hotel.Id,
                Name = hotel.Name,
                City = hotel.City,
                State = hotel.State,
                Address = hotel.Address,
                PhoneNumber = hotel.PhoneNumber,
            }).ToList();


            return hotelDTOs;
        }

        public async Task<HotelDTO> UpdateHotel(int id, HotelDTO hotelDTO)
        {
            Hotel existingHotel = await _context.Hotels.FindAsync(id);
            if (existingHotel != null)
            {
                existingHotel.Id = hotelDTO.Id;
                existingHotel.Name = hotelDTO.Name;
                existingHotel.City = hotelDTO.City;
                existingHotel.State = hotelDTO.State;
                existingHotel.Address = hotelDTO.Address;
                existingHotel.PhoneNumber = hotelDTO.PhoneNumber;

                await _context.SaveChangesAsync();
                return hotelDTO;
            }

            else
            {
                throw new InvalidOperationException("Hotel does not exist.");
            }
        }
    }
}
