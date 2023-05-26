using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BigBang_Assessment_26_5_23_.Controllers
{
    [ApiController]
    [Route("[controller]/actions")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IRepoXYZ? repoContext;
        public AuthenticateController(IRepoXYZ? repoContext)
        {
            this.repoContext = repoContext;
        }
        [HttpGet]
        public async Task<ActionResult> Register(RegisterModel userCredentials)
        {

            return Ok();
        }
    }
}
