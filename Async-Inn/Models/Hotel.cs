using Microsoft.AspNetCore.Identity;

namespace Async_Inn.Models
{
    public class Hotel
    {
        public int Id { get; set; }//locationID
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}
