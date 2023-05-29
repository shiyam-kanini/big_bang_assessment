using BigBang_Assessment_26_5_23_.Models;

namespace BigBang_Assessment_26_5_23_.Model_Request_Response_
{
    public class BookingRequest
    {
        public string? UserId { get; set; }
        public DateTime? BookingDate { get; set; }
        public string? HotelId { get; set; }
        public int NoOfRooms { get; set; }
    }
}
