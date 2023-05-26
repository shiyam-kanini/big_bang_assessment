using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using BigBang_Assessment_26_5_23_.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BigBang_Assessment_26_5_23_.Controllers
{
    [ApiController]
    [Route("[controller]/actions")]
    public class HotelAddressController : Controller
    {
        private readonly IRepoXYZ repoContext;
        public HotelAddressController(IRepoXYZ repoContext)
        {
            this.repoContext = repoContext;
        }
        [HttpGet]
        public async Task<AddressResponse> GetHotelsAddress()
        {

            return await repoContext.GetAddress();
        }
        [HttpGet("id")]
        public async Task<AddressResponse> GetHotelAddressById(int id)
        {
            return await repoContext.GetAddressById(id);
        }
        [HttpPost]
        public async Task<AddressResponse> PostHotelAddress(AddressRequest Address)
        {
            return await repoContext.PostAddress(Address);
        }
        [HttpPut("ID")]
        public async Task<ActionResult<HotelResponse>> EditHotels(string id, string name)
        {

            return await repoContext.PutHotel(id, name);
        }
        [HttpDelete("ID")]
        public async Task<ActionResult<HotelResponse>> DeleteAddress(string id)
        {
            return await repoContext.DeleteHotel(id);
        }

    }
}
