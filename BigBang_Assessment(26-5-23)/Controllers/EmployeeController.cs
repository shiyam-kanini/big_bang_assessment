using BigBang_Assessment_26_5_23_.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BigBang_Assessment_26_5_23_.Controllers
{
    [ApiController]
    [Route("[controller]/actions")]
    public class EmployeeController : Controller
    {
        private readonly IRepoEmployee repoContext;
        public EmployeeController(IRepoEmployee repoContext)
        {
            this.repoContext = repoContext;
        }

    }
}
