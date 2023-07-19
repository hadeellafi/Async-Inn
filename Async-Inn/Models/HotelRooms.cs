using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Async_Inn.Models
{
    public class HotelRooms
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public int RoomId { get; set; }

        public bool IsPetFriendly { get; set; }

        public double Price { get; set; }
    }
}
