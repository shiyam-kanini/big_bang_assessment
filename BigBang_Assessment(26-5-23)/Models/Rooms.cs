using System.ComponentModel.DataAnnotations;

namespace BigBang_Assessment_26_5_23_.Models
{
    public class Rooms
    {
        [Key]
        public string? RoomId { get; set; }
        public XYZHotels? XYZHotelId { get; set; }
    }
}
