﻿namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoom> Create(HotelRoom hotelRoom, int hotelId);
        Task<List<HotelRoom>> Get(int hotelId);
        Task<HotelRoom> GetById(int hotelId, int roomNumber);
        Task<HotelRoom> Update(int hotelId, int roomNumber,HotelRoom hotelRoom);
        Task Delete(int hotelId, int roomNumber);
    }
}