using Async_Inn.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    /// <summary>
    /// Represents the interface for managing hotel rooms.
    /// </summary>
    public interface IHotelRoom
    {
        /// <summary>
        /// Creates a new hotel room.
        /// </summary>
        /// <param name="hotelRoom">The hotel room DTO to create.</param>
        /// <param name="hotelId">The ID of the hotel to associate with the room.</param>
        /// <returns>The created hotel room DTO.</returns>
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int hotelId);

        /// <summary>
        /// Gets all hotel rooms associated with a specific hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to retrieve rooms for.</param>
        /// <returns>A list of hotel room DTOs.</returns>
        Task<List<HotelRoomDTO>> Get(int hotelId);

        /// <summary>
        /// Gets a hotel room by its ID and room number.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to search in.</param>
        /// <param name="roomNumber">The room number of the hotel room to retrieve.</param>
        /// <returns>The hotel room DTO with the specified ID and room number.</returns>
        Task<HotelRoomDTO> GetById(int hotelId, int roomNumber);

        /// <summary>
        /// Updates an existing hotel room.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel that the room belongs to.</param>
        /// <param name="roomNumber">The room number of the hotel room to update.</param>
        /// <param name="hotelRoom">The updated hotel room DTO.</param>
        /// <returns>The updated hotel room DTO.</returns>
        Task<HotelRoomDTO> Update(int hotelId, int roomNumber, HotelRoomDTO hotelRoom);

        /// <summary>
        /// Deletes a hotel room by its ID and room number.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to search in.</param>
        /// <param name="roomNumber">The room number of the hotel room to delete.</param>
        Task Delete(int hotelId, int roomNumber);
    }
}
