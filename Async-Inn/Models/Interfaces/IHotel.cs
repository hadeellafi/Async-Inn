using Async_Inn.Models.DTO;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {
        Task<HotelDTO> Create(HotelDTO hotel);

        Task<List<HotelDTO>> GetHotels();

        Task<HotelDTO> GetById(int hotelId);

        Task<HotelDTO> UpdateHotel(int id, HotelDTO hotel);

        Task Delete(int id);


    }
}
