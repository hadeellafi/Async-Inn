using Async_Inn.Data;
using Async_Inn.Models.DTO;
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
        public async Task<AmenityDTO> Create(AmenityDTO amenity)
        {
            Amenity amenityEntity = new Amenity()
            {
                Name = amenity.Name
            };

            _context.Amenities.Add(amenityEntity);
            await _context.SaveChangesAsync();

            // Now the database has generated the actual ID for the newly created Amenity
            // retrieve it and set it in the AmenityDTO before returning
            amenity.Id = amenityEntity.Id;

            return amenity;
        }

        public async Task Delete(int id)
        {
            
            Amenity amenity = await _context.Amenities.FindAsync(id);
            if (amenity != null)
            {
                _context.Entry(amenity).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Hotel Room not found.");
            }

        }

        public async  Task<List<AmenityDTO>> GetAmenities()
        {

            //  Retrieve from the database
            var amenities = await _context.Amenities.ToListAsync();

            //  Map each Amenity entity to its  AmenityDTO 
            var amenityDTOs = amenities.Select(amenity => new AmenityDTO
            {
                Id = amenity.Id,
                Name = amenity.Name,
            }).ToList();

            
            return amenityDTOs;
        }

        public async Task<AmenityDTO> GetById(int amenityId)
        {
            /* Amenity amenity = await _context.Amenities
                     .FirstOrDefaultAsync(a => a.Id == amenityId);
             var amenityDTO = new AmenityDTO()
             {
                 Id = amenity.Id,
                 Name = amenity.Name,
             };
             */

            //now unsig LINQ
            //Selcet it used to transfer one type to another 
            
            var amenityDTO = await _context.Amenities
        .Where(a => a.Id == amenityId)
        .Select(a => new AmenityDTO
        {
            Id = a.Id,
            Name = a.Name,
        })
        .FirstOrDefaultAsync();

            return amenityDTO;
        }

        public async Task<AmenityDTO> Update(int id, AmenityDTO amenity)
        {
            Amenity existingAmenity = await _context.Amenities.FindAsync(id);
            if (existingAmenity != null)
            {
                existingAmenity.Id = amenity.Id;
                existingAmenity.Name = amenity.Name;
                
                await _context.SaveChangesAsync();
                return amenity;
            }

            else
            {
                throw new InvalidOperationException("Aminity does not exist.");
            }
        }
        
        

    }
}
