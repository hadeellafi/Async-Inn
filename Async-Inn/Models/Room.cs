
namespace Async_Inn.Models
{
    public enum Layout
    {
        Studio,
        OneBedroom ,
        TwoBedroom 
    }
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Layout RoomLayout  { get; set; }

        //add navigation properties

        public List<RoomAmenity>? RoomAmenities { get; set; }//it is like i promise "the program "that the value isnot null

        public List<HotelRoom>? HotelRooms { get; set; }

    }
}
