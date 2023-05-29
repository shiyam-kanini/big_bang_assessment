using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using Microsoft.EntityFrameworkCore;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public class RepoEmployeeXYZRole : IRepoEmployeeXYZRole
    {
        private readonly Random random = new();
        private readonly XYZHotelDbContext _context;
        public RepoEmployeeXYZRole(XYZHotelDbContext context)
        {
            _context = context;
        }
        List<Employee_XYZHotels> employeeXYZRoles = new();
        Employee_XYZHotels newEmployeeXYZRole = new();
        EmployeeXYZRoleResponse employeeXYZRolesResponse = new();

        public async Task<EmployeeXYZRoleResponse> GetEmployeeXYZRoles()
        {
            try
            {
                employeeXYZRoles = await _context.Employee_XYZHotels.Include(x => x.XYZHotel).Include(z => z.Employees).ToListAsync();
                if (employeeXYZRoles.Count <= 0)
                {
                    AddResponse(false, "No Data Found", employeeXYZRoles);
                    return employeeXYZRolesResponse;
                }
                AddResponse(true, $"{employeeXYZRoles.Count} Records Found", employeeXYZRoles);
                return employeeXYZRolesResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, employeeXYZRoles);
                return employeeXYZRolesResponse;
            }
        }

        public async Task<EmployeeXYZRoleResponse> GetEmployeeXYZRoleById(string id)
        {
            try
            {
                newEmployeeXYZRole = await _context.Employee_XYZHotels.FindAsync(id ?? "");
                if (newEmployeeXYZRole == null)
                {
                    AddResponse(false, "No Data Found", employeeXYZRoles);
                    return employeeXYZRolesResponse;
                }
                AddResponse(true, $"{employeeXYZRoles.Count} records Found", employeeXYZRoles);
                return employeeXYZRolesResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, employeeXYZRoles);
                return employeeXYZRolesResponse;
            }
        }

        public async Task<EmployeeXYZRoleResponse> PostEmployeeXYZRole(EmployeeXYXRoleRequest employeeXYZRole)
        {
            try
            {
                //AddEmployeeXYZRole($"EXYZRID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}",employeeXYZRole);
                newEmployeeXYZRole = new()
                {
                    EHID = $"EXYZRID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}",
                    Employees = await _context.Employees.FindAsync(employeeXYZRole.Employees),
                    Role = await _context.Role.FindAsync(employeeXYZRole.Role),
                    XYZHotel = await _context.Hotels.FindAsync(employeeXYZRole.XYZHotel),
                };
                employeeXYZRoles.Add(newEmployeeXYZRole);
                await _context.Employee_XYZHotels.AddAsync(newEmployeeXYZRole);
                await _context.SaveChangesAsync();
                AddResponse(true, "1 Record Added", employeeXYZRoles);
                return employeeXYZRolesResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, employeeXYZRoles);
                return employeeXYZRolesResponse;
            }
        }

        public void AddResponse(bool success, string message, List<Employee_XYZHotels> employeeXYZRoles)
        {
            employeeXYZRolesResponse = new()
            {
                Success = success,
                Message = message,
                Employee_XYZHotels = employeeXYZRoles,
            };
        }
        public async void AddEmployeeXYZRole(string employeeId, EmployeeXYXRoleRequest employeeXYZRole)
        {
            newEmployeeXYZRole = new()
            {
                EHID = employeeId,
                Employees = await _context.Employees.FindAsync(employeeXYZRole.Employees),
                Role = await _context.Role.FindAsync(employeeXYZRole.Role),
                XYZHotel = await _context.Hotels.FindAsync(employeeXYZRole.XYZHotel),
            };
        }
    }
}
