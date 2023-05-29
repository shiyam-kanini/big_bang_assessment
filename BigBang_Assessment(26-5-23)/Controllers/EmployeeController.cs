using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBang_Assessment_26_5_23_.Controllers
{
    [ApiController]
    [Route("[controller]/actions")]
    [Authorize(Roles ="RID170")]
    public class EmployeeController : Controller
    {
        private readonly IRepoEmployee repoContext;
        public EmployeeController(IRepoEmployee repoContext)
        {
            this.repoContext = repoContext;
        }
        [HttpGet]
        public async Task<EmployeeResponse> GetEmployees()
        {
            return await repoContext.GetEmployees();
        }
        [HttpGet("{id}")]
        public async Task<EmployeeResponse> GetEmployeeById(string id)
        {
            return await repoContext.GetEmployeeById(id);
        }
        [HttpPost]
        public async Task<EmployeeResponse> PostEmployee(EmployeeRequest employee)
        {
            return await repoContext.PostEmployee(employee);
        }
        
        [HttpDelete]
        public async Task<ActionResult<EmployeeResponse>> GetEmployee(string id)
        {
            return await repoContext.DeleteEmployee(id);
        }

    }
}
