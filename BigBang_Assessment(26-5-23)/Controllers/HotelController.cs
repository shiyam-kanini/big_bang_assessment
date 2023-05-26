using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using BigBang_Assessment_26_5_23_.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBang_Assessment_26_5_23_.Controllers
{
    [ApiController]
    [Route("[controller]/actions")]
    public class HotelController : Controller
    {
        private readonly IRepoXYZ repoContext;
        public HotelController(IRepoXYZ repoContext)
        {
            this.repoContext = repoContext;
        }
        [HttpGet]
        public async Task<HotelResponse> GetHotels() 
        {

            return await repoContext.GetHotels();
        }
        [HttpGet("id")]
        public async Task<HotelResponse> GetHotelsById(int id)
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
        [HttpDelete]
        public async Task<ActionResult<Commonresponse>> GetDelete()
        {

            return new Commonresponse();
        }
    }
}
