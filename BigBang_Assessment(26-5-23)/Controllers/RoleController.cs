using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBang_Assessment_26_5_23_.Controllers
{
    [ApiController]
    [Route("[Controller]/actions")]
    [Authorize(Roles = "RID170")]


    public class RoleController : Controller
    {

        private readonly IRepoRoles repoContext;
        public RoleController(IRepoRoles repoContext)
        {
            this.repoContext = repoContext;
        }
        [HttpGet]
        public async Task<RolesResponse> GetHotelRooms()
        {
            return await repoContext.GetRoles();
        }
        [HttpGet("id")]
        public async Task<RolesResponse> GetHotelAddressById(string id)
        {
            return await repoContext.GetRoleById(id);
        }
        [HttpPost]
        public async Task<RolesResponse> PostHotelRoom(RolesRequest role)
        {
            return await repoContext.PostRole(role);
        }
        [HttpPut("ID")]
        public async Task<RolesResponse> PutRoom(string id, RolesRequest role)
        {
            return await repoContext.PutRole(id, role);
        }
        [HttpDelete("ID")]
        public async Task<RolesResponse> DeleteRoom(string id)
        {
            return await repoContext.DeleteRole(id);
        }
    }
}
