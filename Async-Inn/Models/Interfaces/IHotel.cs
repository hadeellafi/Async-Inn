namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {
        Task<Hotel> Create(Hotel hotel);

        Task<List<Hotel>> GetHotels();

        Task<Hotel> GetById(int hotelId);

        Task<Hotel> UpdateHotel(int id, Hotel hotel);

        Task Delete(int id);


    }
}
