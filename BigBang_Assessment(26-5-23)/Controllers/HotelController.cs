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
    public class HotelController : Controller
    {
        private readonly IRepoHotel repoContext;
        public HotelController(IRepoHotel repoContext)
        {
            this.repoContext = repoContext;
        }
        [HttpGet]
        public async Task<HotelResponse> GetHotels() 
        {
            return await repoContext.GetHotels();
        }
        [HttpGet("id")]
/*        [Authorize(Roles = "user")]
*/        public async Task<HotelResponse> GetHotelsById(string id)
        {
            return await repoContext.GetHotelById(id);
        }
        [HttpPost]
        public async Task<HotelResponse> PostHotels(string hotelName)
        {            
            return await repoContext.PostHotel(hotelName);
        }
        [HttpPut("ID")]
        public async Task<ActionResult<HotelResponse>> EditHotels(string id, string name)
        {
            return await repoContext.PutHotel(id, name) ;
        }
        [HttpDelete("ID")]
        public async Task<ActionResult<HotelResponse>> GetDelete(string id)
        {
            return await repoContext.DeleteHotel(id);
        }
    }
}
