using BigBang_Assessment_26_5_23_.Models;

namespace BigBang_Assessment_26_5_23_.Model_Request_Response_
{
    public class RoomResponse
    {
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public List<Rooms>? Rooms { get; set; }
    }
}
