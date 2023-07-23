namespace Async_Inn.Models.Interfaces
{
    public interface IAmenity
    {
        Task<Amenity> Create(Amenity amenity);

        Task<List<Amenity>> GetAmenities();

        Task<Amenity> GetById(int amenityId);

        Task<Amenity> Update(int id, Amenity amenity);

        Task Delete(int id);

        Task<bool> AmenityExists(int id);

    }
}
