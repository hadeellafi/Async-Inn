
namespace Async_Inn.Models
{
    public enum Layout
    {
        Studio = 0,
        OneBedroom = 1,
        TwoBedroom = 2
    }
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomLayout  { get; set; }

        //add navigation properties


        public List<RoomAmenity>? RoomAmenities { get; set; }

        public List<HotelRoom>? HotelRooms { get; set; }


    }
}
