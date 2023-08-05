using Async_Inn.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    /// <summary>
    /// Represents the interface for managing hotels.
    /// </summary>
    public interface IHotel
    {
        /// <summary>
        /// Creates a new hotel.
        /// </summary>
        /// <param name="hotel">The hotel DTO to create.</param>
        /// <returns>The created hotel DTO.</returns>
        Task<HotelDTO> Create(HotelDTO hotel);

        /// <summary>
        /// Gets all hotels.
        /// </summary>
        /// <returns>A list of hotel DTOs.</returns>
        Task<List<HotelDTO>> GetHotels();

        /// <summary>
        /// Gets a hotel by its ID.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to retrieve.</param>
        /// <returns>The hotel DTO with the specified ID.</returns>
        Task<HotelDTO> GetById(int hotelId);

        /// <summary>
        /// Updates an existing hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to update.</param>
        /// <param name="hotel">The updated hotel DTO.</param>
        /// <returns>The updated hotel DTO.</returns>
        Task<HotelDTO> UpdateHotel(int id, HotelDTO hotel);

        /// <summary>
        /// Deletes a hotel by its ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to delete.</param>
        Task Delete(int id);
    }
}
