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
        public string? EmployeePasswordHash { get; set; }
        public string? EmployeePasswordSalt { get;set; }        
    }
}
