using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBang_Assessment_26_5_23_.Controllers
{
    [ApiController]
    [Route("[controller]/actions")]
    [Authorize(Roles = "RID170")]

    public class EmployeeXYZRoleController : Controller
    {
        private readonly IRepoEmployeeXYZRole repoContext;
        public EmployeeXYZRoleController(IRepoEmployeeXYZRole repoContext)
        {
            this.repoContext = repoContext;
        }
        [HttpGet]
        public async Task<EmployeeXYZRoleResponse> GetEmployeeXYZRoles()
        {
            return await repoContext.GetEmployeeXYZRoles();
        }
        [HttpGet("id")]
        public async Task<EmployeeXYZRoleResponse> GetEmployeeXYZRoleById(string id)
        {
            return await repoContext.GetEmployeeXYZRoleById(id);
        }
        [HttpPost]
        public async Task<EmployeeXYZRoleResponse> PostEmployeeXYZRole(EmployeeXYXRoleRequest role)
        {
            return await repoContext.PostEmployeeXYZRole(role);
        }
    }
}
