using Async_Inn.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    /// <summary>
    /// Represents the interface for managing rooms.
    /// </summary>
    public interface IRoom
    {
        /// <summary>
        /// Creates a new room.
        /// </summary>
        /// <param name="room">The room DTO to create.</param>
        /// <returns>The created room DTO.</returns>
        Task<RoomDTO> Create(RoomDTO room);

        /// <summary>
        /// Gets all rooms.
        /// </summary>
        /// <returns>A list of room DTOs.</returns>
        Task<List<RoomDTO>> GetRooms();

        /// <summary>
        /// Gets a room by its ID.
        /// </summary>
        /// <param name="roomId">The ID of the room to retrieve.</param>
        /// <returns>The room DTO with the specified ID.</returns>
        Task<RoomDTO> GetById(int roomId);

        /// <summary>
        /// Updates an existing room.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="room">The updated room DTO.</param>
        /// <returns>The updated room DTO.</returns>
        Task<RoomDTO> Update(int id, RoomDTO room);

        /// <summary>
        /// Associates an amenity with a specific room.
        /// </summary>
        /// <param name="roomId">The ID of the room to associate the amenity with.</param>
        /// <param name="amenityId">The ID of the amenity to be associated with the room.</param>
        Task AddAmenityToRoom(int roomId, int amenityId);

        /// <summary>
        /// Removes the association of an amenity from a specific room.
        /// </summary>
        /// <param name="roomId">The ID of the room to remove the amenity association from.</param>
        /// <param name="amenityId">The ID of the amenity to be removed from the room.</param>
        Task RemoveAmenityFromRoom(int roomId, int amenityId);

        /// <summary>
        /// Deletes a room by its ID.
        /// </summary>
        /// <param name="id">The ID of the room to delete.</param>
        Task Delete(int id);
    }
}
