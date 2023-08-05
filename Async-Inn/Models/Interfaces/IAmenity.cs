using Async_Inn.Models.DTO;

namespace Async_Inn.Models.Interfaces
{
    /// <summary>
    /// Represents the interface for managing amenities.
    /// </summary>
    public interface IAmenity
    {
        /// <summary>
        /// Creates a new amenity.
        /// </summary>
        /// <param name="amenity">The amenity DTO to create.</param>
        /// <returns>The created amenity DTO.</returns>
        Task<AmenityDTO> Create(AmenityDTO amenity);

        /// <summary>
        /// Gets all amenities.
        /// </summary>
        /// <returns>A list of amenity DTOs.</returns>
        Task<List<AmenityDTO>> GetAmenities();

        /// <summary>
        /// Gets an amenity by its ID.
        /// </summary>
        /// <param name="amenityId">The ID of the amenity to retrieve.</param>
        /// <returns>The amenity DTO with the specified ID.</returns>
        Task<AmenityDTO> GetById(int amenityId);

        /// <summary>
        /// Updates an existing amenity.
        /// </summary>
        /// <param name="id">The ID of the amenity to update.</param>
        /// <param name="amenity">The updated amenity DTO.</param>
        /// <returns>The updated amenity DTO.</returns>
        Task<AmenityDTO> Update(int id, AmenityDTO amenity);

        /// <summary>
        /// Deletes an amenity by its ID.
        /// </summary>
        /// <param name="id">The ID of the amenity to delete.</param>
        Task Delete(int id);
    }
}
