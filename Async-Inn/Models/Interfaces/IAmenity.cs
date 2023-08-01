using Async_Inn.Models.DTO;

namespace Async_Inn.Models.Interfaces
{
    public interface IAmenity
    {
        Task<AmenityDTO> Create(AmenityDTO amenity);

        Task<List<AmenityDTO>> GetAmenities();

        Task<AmenityDTO> GetById(int amenityId);

        Task<AmenityDTO> Update(int id, AmenityDTO amenity);

        Task Delete(int id);


    }
}
