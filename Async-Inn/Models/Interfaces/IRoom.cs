namespace Async_Inn.Models.Interfaces
{
    public interface IRoom
    {
        Task<Room> Create(Room room);

        Task<List<Room>> GetRooms();

        Task<Room> GetById(int roomId);

        Task<Room> Update(int id, Room room);

        Task AddAmenityToRoom(int roomId, int amenityId);
        Task RemoveAmenityFromRoom(int roomId, int amenityId);

        Task Delete(int id);
    }
}
