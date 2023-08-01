using Async_Inn.Models.DTO;

namespace Async_Inn.Models.Interfaces
{
    public interface IRoom
    {
        Task<RoomDTO> Create(RoomDTO room);

        Task<List<RoomDTO>> GetRooms();

        Task<RoomDTO> GetById(int roomId);

        Task<RoomDTO> Update(int id, RoomDTO room);

        Task AddAmenityToRoom(int roomId, int amenityId);
        Task RemoveAmenityFromRoom(int roomId, int amenityId);

        Task Delete(int id);
    }
}
