using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Models.Services
{
    public class AmenityService : IAmenity
    {
        private readonly AsyncInnDbContext _context;
        public AmenityService(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<Amenity> Create(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task Delete(int id)
        {
            
            Amenity amenity = await GetById(id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

        }

        public async  Task<List<Amenity>> GetAmenities()
        {
            var amenities = await _context.Amenities.ToListAsync();
            return amenities;
        }

        public async Task<Amenity> GetById(int amenityId)
        {
            Amenity amenity = await _context.Amenities.FindAsync(amenityId);
            return amenity;
        }

        public async Task<Amenity> Update(int id, Amenity amenity)
        {

            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return amenity;
        }
        
        public async Task<bool> AmenityExists(int id)
        {
            return await _context.Amenities.AnyAsync(e => e.Id == id);
        }

    }
}
