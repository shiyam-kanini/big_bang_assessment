using System.ComponentModel.DataAnnotations;

namespace BigBang_Assessment_26_5_23_.Models
{
    public class Employee_XYZHotels
    {
        [Key]
        public string? EHID { get; set; }
        public XYZHotels? XYZHotel { get; set; }
        
    }
}
