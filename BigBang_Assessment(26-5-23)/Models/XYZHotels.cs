using System.ComponentModel.DataAnnotations;

namespace BigBang_Assessment_26_5_23_.Models
{
    public class XYZHotels
    {
        [Key]
        public string? HotelId { get; set; }
        public string? HotelName { get; set; }
        /*public string?  CEOId { get; set; }*/
        public ICollection<HotelAddress>? HotelAddresses { get; set; }
        public ICollection<Employee_XYZHotels>? Employee_XYZHotels { get; set; }
        public ICollection<Rooms>? Rooms { get; set; }

        public static implicit operator List<object>(XYZHotels? v)
        {
            throw new NotImplementedException();
        }
    }
}
