using System.Text.Json.Serialization;

namespace Async_Inn.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //add navigation properties
        public List<RoomAmenity> RoomAmenities { get; set; }

    }
}
