using BigBang_Assessment_26_5_23_.Model_Request_Response_;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public interface IRepoEmployeeXYZRole
    {
        Task<EmployeeXYZRoleResponse> GetEmployeeXYZRoles();
        Task<EmployeeXYZRoleResponse> GetEmployeeXYZRoleById(string id);
        Task<EmployeeXYZRoleResponse> PostEmployeeXYZRole(EmployeeXYXRoleRequest employee);
    }
}
