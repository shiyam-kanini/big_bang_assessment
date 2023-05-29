using System.ComponentModel.DataAnnotations;

namespace BigBang_Assessment_26_5_23_.Models
{
    public class User
    {
        [Key]
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public byte[]? UserPasswordHash { get; set; }
        public byte[]? UserPasswordSalt { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
