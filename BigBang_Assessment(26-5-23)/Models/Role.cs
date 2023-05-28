namespace BigBang_Assessment_26_5_23_.Models
{
    public class Role
    {
        public string? RoleId { get; set; }
        public string? RoleName { get; set;}
        public string? RoleDescription { get; set;}
        public ICollection<Employee_XYZHotels>? Employee_XYZHotels{ get; set;}
    }
}
