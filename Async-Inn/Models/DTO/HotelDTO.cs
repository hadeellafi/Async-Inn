namespace Async_Inn.Models.DTO
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public List<HotelRoomDTO>? HotelRooms { get; set; }
    }
}
