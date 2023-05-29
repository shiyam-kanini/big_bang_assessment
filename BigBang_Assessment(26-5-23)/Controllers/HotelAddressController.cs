using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using BigBang_Assessment_26_5_23_.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BigBang_Assessment_26_5_23_.Controllers
{
    [ApiController]
    [Route("[controller]/actions")]
    [Authorize(Roles = "RID380")]

    public class HotelAddressController : Controller
    {
        private readonly IRepoAddress repoContext;
        public HotelAddressController(IRepoAddress repoContext)
        {
            this.repoContext = repoContext;
        }
        [HttpGet]
        public async Task<AddressResponse> GetHotelsAddress()
        {

            return await repoContext.GetAddress();
        }
        [HttpGet("id")]
        public async Task<AddressResponse> GetHotelAddressById(string id)
        {
            return await repoContext.GetAddressById(id);
        }
        [HttpPost]
        public async Task<AddressResponse> PostHotelAddress(AddressRequest Address)
        {
            return await repoContext.PostAddress(Address);
        }
        [HttpPut("ID")]
        public async Task<AddressResponse> PutAddress(string id,AddressRequest Address)
        {
            return await repoContext.PutAddress(id,Address);
        }
        [HttpDelete("ID")]
        public async Task<AddressResponse> DeleteAddress(string id)
        {
            return await repoContext.DeleteAddress(id);
        }

    }
}
