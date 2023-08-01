namespace Async_Inn.Models.DTO
{
    public class HotelRoomDTO
    {
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }

        public int RoomId { get; set; }

        public bool IsPetFriendly { get; set; }

        public decimal Rate { get; set; }
        public RoomDTO ? Room { get; set; }
    }
}
