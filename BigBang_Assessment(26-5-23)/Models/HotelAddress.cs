using System.ComponentModel.DataAnnotations;

namespace BigBang_Assessment_26_5_23_.Models
{
    public class HotelAddress
    {
        [Key]
        public string? HotelAddressId { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public XYZHotels? Hotel { get; set; }
    }
}
