using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class HotelService : IHotel
    {
        private readonly AsyncInnDbContext _context;

        public HotelService(AsyncInnDbContext context)
        {
            _context = context;
        }
        public  async Task<Hotel> Create(Hotel hotel)
        {
           _context.Hotels.Add(hotel);
           await _context.SaveChangesAsync();

            return hotel;

        }

        public async Task Delete(int id)
        {
            Hotel hotel = await GetById(id);

            _context.Entry(hotel).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<Hotel> GetById(int hotelId)
        {
            Hotel hotel = await _context.Hotels.FindAsync(hotelId);

            return hotel;
        }

        public  async Task<List<Hotel>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();

            return hotels;
        }

        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return hotel; 
        }
    }
}
