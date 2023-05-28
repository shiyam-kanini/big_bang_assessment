using BigBang_Assessment_26_5_23_.Model_Request_Response_;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public interface IRepoRoles
    {
        Task<RolesResponse> GetRoles();
        Task<RolesResponse> GetRoleById(string id);
        Task<RolesResponse> PostRole(RolesRequest role);
        Task<RolesResponse> PutRole(string id, RolesRequest role);
        Task<RolesResponse> DeleteRole(string id);
    }
}
