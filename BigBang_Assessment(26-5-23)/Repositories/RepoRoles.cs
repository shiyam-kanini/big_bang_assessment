using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public class RepoRoles : IRepoRoles
    {
        private readonly Random random = new();
        private readonly XYZHotelDbContext _context;
        public RepoRoles(XYZHotelDbContext context)
        {
            _context = context;
        }
        List<Role> roles = new();
        Role? newRole = new();
        RolesResponse roleResponse = new();
        public async Task<RolesResponse> DeleteRole(string id)
        {
            try
            {
                newRole = await _context.Role.FindAsync(id != null ? id : "");
                if (newRole == null)
                {
                    AddResponse(false, "No Room Found", roles);
                    return roleResponse;
                }
                await _context.Role.Where(x => (x.RoleId != null ? x.RoleId : "").Equals(id)).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
                roles.Add(newRole);
                AddResponse(true, "1 Role Deleted", roles);
                return roleResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, roles);
                return roleResponse;
            }
        }

        public async Task<RolesResponse> GetRoleById(string id)
        {
            try
            {
                roles = await _context.Role.Where(x => (x.RoleId ?? "").Equals(id)).Include(x => x.Employee_XYZHotels).ToListAsync();
                if (roles.Count <= 0)
                {
                    AddResponse(false, "No Data Found", roles);
                    return roleResponse;
                }
                AddResponse(true, $"{roles.Count} records Found", roles);
                return roleResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, roles);
                return roleResponse;
            }
        }

        public async Task<RolesResponse> GetRoles()
        {
            try
            {
                roles = await _context.Role.Include(x => x.Employee_XYZHotels).ToListAsync();
                if (roles.Count <= 0)
                {
                    AddResponse(false, "No Data Found", roles);
                    return roleResponse;
                }
                AddResponse(true, $"{roles.Count} Records Found", roles);
                return roleResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.StackTrace, roles);
                return roleResponse;
            }
        }

        public async Task<RolesResponse> PostRole(RolesRequest role)
        {
            try
            {
               newRole = await _context.Role.Where(x => (x.RoleName ?? "").Equals(role.RoleName)).FirstOrDefaultAsync();
                if (newRole != null)
                {
                    roles.Add(newRole);
                    AddResponse(false, "Role Already exists", roles);
                    return roleResponse;
                }
                AddRole($"RID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}", role);
                roles.Add(newRole);
                await _context.Role.AddAsync(newRole);
                await _context.SaveChangesAsync();
                AddResponse(true, $"1 Record Added ({role.RoleName})", roles);
                return roleResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, roles);
                return roleResponse;
            }
        }

        public async Task<RolesResponse> PutRole(string id, RolesRequest role)
        {
            try
            {
                newRole = await _context.Role.FirstOrDefaultAsync(x => x.RoleId == id);
                if (newRole == null)
                {
                    AddResponse(true, "Role doesn't exist", roles);
                    return roleResponse;
                }
                _context.Entry(newRole).State = EntityState.Detached;
                AddRole(id ?? "", role);
                _context.Entry(newRole).State = EntityState.Modified;
                _context.Role.Update(newRole);
                await _context.SaveChangesAsync();roles.Add(newRole);
                AddResponse(true, "1 Record Updated", roles);
                return roleResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.StackTrace, roles);
                return roleResponse;
            }
        }
        public void AddResponse(bool success, string message, List<Role> role)
        {
            roleResponse = new()
            {
                Success = success,
                Message = message,
                Roles = role,
            };
        }
        public void AddRole(string roleId, RolesRequest role)
        {
            newRole = new()
            {
                RoleId = roleId,
                RoleName = role.RoleName,
                RoleDescription= role.RoleDescription,
            };
        }
    }
}
