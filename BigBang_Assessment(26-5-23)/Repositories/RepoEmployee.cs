using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public class RepoEmployee : IRepoEmployee
    {
        private readonly Random random = new();
        private readonly XYZHotelDbContext _context;
        public RepoEmployee(XYZHotelDbContext context)
        {
            _context = context;
        }
        List<Employees> employees = new();
        List<Employee_XYZHotels> employeeRelation = new();
        Employees newEmployee = new();
        Employee_XYZHotels newEmployeeRelation = new();
        EmployeeResponse employeeResponse = new();
        public Task<EmployeeResponse> DeleteEmployee(string id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeResponse> GetEmployeeById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeResponse> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeResponse> PostEmployee(EmployeeRequest employee)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeResponse> PutEmployee(string id, EmployeeRequest employee)
        {
            throw new NotImplementedException();
        }

        public void AddResponse(bool success, string message, List<Employees> employees, List<Employee_XYZHotels> employeeRelation)
        {
            employeeResponse = new()
            {
                Success = success,
                Message = message,
                Employee = employees,
                Employee_XYZHotel = employeeRelation
            };
        }
        public void AddEmployee(string employeeId, EmployeeRequest employee)
        {
            newEmployee = new()
            {
                EmployeeFirstName = employee.EmployeeFirstName,
                EmployeLastName = employee.EmployeLastName,
                Employee_XYZHotels= employeeRelation,
                EmployeeDOB = employee.EmployeeDOB,
                EmployeeDOJ = employee.EmployeeDOJ,
                EmployeeQualifications = employee.EmployeeQualifications,
                EmployeeId = employeeId
            };
        }
    }
}
