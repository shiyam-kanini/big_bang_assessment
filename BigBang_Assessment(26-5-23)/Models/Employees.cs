using System.ComponentModel.DataAnnotations;

namespace BigBang_Assessment_26_5_23_.Models
{
    public class Employees
    {
        [Key]
        public string? EmployeeId { get; set; }
        public string? EmployeeFirstName { get; set; }
        public string? EmployeLastName { get; set; }
        public string? EmployeeDOJ { get; set; }
        public string? EmployeeDOB { get; set; }
        public string? EmployeeQualifications { get; set; }
        public byte[]? EmployeePasswordHash { get; set; }
        public byte[]? EmployeePasswordSalt { get;set; }   
        public ICollection<Employee_XYZHotels> Employee_XYZHotels { get;set; }
    }
}
