using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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
        Employees? newEmployee = new();
        EmployeeResponse employeeResponse = new();
        public async Task<EmployeeResponse> DeleteEmployee(string id)
        {
            try
            {
                newEmployee = await _context.Employees.Where(x => (x.EmployeeId ?? "").Equals(id)).FirstOrDefaultAsync();
                if (newEmployee == null)
                {
                    AddResponse(false, "No Employee Found", employees);
                    return employeeResponse;
                }
                AddResponse(true, $"User : {newEmployee.EmployeeFirstName} {newEmployee.EmployeLastName} Deleted", employees);
                await _context.Employees.Where(x => (x.EmployeeId != null ? x.EmployeeId : "").Equals(id)).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
                employees.Add(newEmployee);
                return employeeResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, employees);
                return employeeResponse;
            }
        }

        public async Task<EmployeeResponse> GetEmployeeById(string id)
        {
            try
            {
                newEmployee = await _context.Employees.FindAsync(id ?? "");
                if (newEmployee == null)
                {
                    AddResponse(false, "No Data Found", employees);
                    return employeeResponse;
                }
                AddResponse(true, $"{employees.Count} records Found", employees);
                return employeeResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, employees);
                return employeeResponse;
            }
        }

        public async Task<EmployeeResponse> GetEmployees()
        {
            try
            {
                employees = await _context.Employees.ToListAsync();
                if (employees.Count <= 0)
                {
                    AddResponse(false, "No Data Found", employees);
                    return employeeResponse;
                }
                AddResponse(true, $"{employees.Count} Records Found", employees);
                return employeeResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, employees);
                return employeeResponse;
            }
        }
        public async Task<EmployeeResponse> PostEmployee(EmployeeRequest employee)
        {
            try
            {
                byte[] passwordHash;
                byte[] passwordSalt;
                CreatePasswordHash(employee.Password ?? "", out passwordHash, out passwordSalt);
                AddEmployee($"EID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}", employee, passwordHash, passwordSalt);
                employees.Add(newEmployee);                
                await _context.Employees.AddAsync(newEmployee);
                await _context.SaveChangesAsync();
                AddResponse(true, "1 Record Added", employees);
                return employeeResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, employees);
                return employeeResponse;
            }
        }

        public Task<EmployeeResponse> PutEmployee(string id, EmployeeRequest employee)
        {
            return Task.FromResult(employeeResponse);
        }        

        public void AddResponse(bool success, string message, List<Employees> employees)
        {
            employeeResponse = new()
            {
                Success = success,
                Message = message,
                Employee = employees,
            };
        }
        public void AddEmployee(string employeeId, EmployeeRequest employee, byte[] passwordHash, byte[] passworrdSalt)
        {
            newEmployee = new()
            {
                EmployeeFirstName = employee.EmployeeFirstName,
                EmployeLastName = employee.EmployeLastName,
                EmployeeDOB = employee.EmployeeDOB,
                EmployeeDOJ = employee.EmployeeDOJ,
                EmployeeQualifications = employee.EmployeeQualifications,
                EmployeeId = employeeId,
                EmployeePasswordHash= passwordHash,
                EmployeePasswordSalt= passworrdSalt
            };
        }
        
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
