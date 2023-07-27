

namespace Async_Inn.Models
{
    public class HotelRoom
    {
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }

        public int RoomId { get; set; }

        public bool IsPetFriendly { get; set; }

        public decimal Rate { get; set; }

        // Navigation properties
        public Room Room { get; set; }
        public Hotel Hotel { get; set; }
    }

}
