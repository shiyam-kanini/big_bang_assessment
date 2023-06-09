﻿using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBang_Assessment_26_5_23_.Controllers
{
    [ApiController]
    [Route("[controller]/actions")]
    [AllowAnonymous]
    public class AuthenticateController : ControllerBase
    {
        private readonly IRepoXYZ repoContext;
        public AuthenticateController(IRepoXYZ repoContext)
        {
            this.repoContext = repoContext;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<Commonresponse>> Register(RegisterModel userCredentials)
        {
            return await repoContext.Register(userCredentials);
        }
        [HttpPost]
        [Route("User-Login")]
        public async Task<ActionResult<Commonresponse>> LoginU(LoginRequest userCredentials)
        {
            return await repoContext.LoginUser(userCredentials);
        }
        [HttpPost]
        [Route("Employee-Login")]
        public async Task<ActionResult<Commonresponse>> LoginE(LoginRequest userCredentials)
        {
            return await repoContext.LoginEmployee(userCredentials);
        }
    }
}
