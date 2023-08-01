using Async_Inn.Models.DTO;

namespace Async_Inn.Models.Interfaces
{
    public interface IAmenity
    {
        Task<AmenityDTO> Create(AmenityDTO amenity);

        Task<List<AmenityDTO>> GetAmenities();

        Task<AmenityDTO> GetById(int amenityId);

        Task<Amenity> Update(int id, Amenity amenity);

        Task Delete(int id);

        Task<bool> AmenityExists(int id);

    }
}
