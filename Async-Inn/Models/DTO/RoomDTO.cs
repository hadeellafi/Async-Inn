namespace Async_Inn.Models.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Layout RoomLayout { get; set; }
        public List<AmenityDTO>? Amenities { get; set; }
    }
}
