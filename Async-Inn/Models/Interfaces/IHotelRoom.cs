using Async_Inn.Models.DTO;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int hotelId);
        Task<List<HotelRoomDTO>> Get(int hotelId);
        Task<HotelRoomDTO> GetById(int hotelId, int roomNumber);
        Task<HotelRoomDTO> Update(int hotelId, int roomNumber,HotelRoomDTO hotelRoom);
        Task Delete(int hotelId, int roomNumber);
    }
}
