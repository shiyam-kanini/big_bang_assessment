namespace BigBang_Assessment_26_5_23_.Models
{
    public class Booking
    {
        public string? BookingId { get; set; }
        public User? UserId { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime BookedOn { get; set; }
        public int NoOfRooms { get; set; }
        public bool? IsBooked { get; set; }
    }
}
