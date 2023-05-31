using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBang_Assessment_26_5_23_.Controllers
{
    [ApiController]
    [Route("[Controller]/actions")]
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        private readonly IRepoUser repoContext;
        public UserController(IRepoUser repoContext)
        {
            this.repoContext = repoContext;
        }
        [HttpPost]
        public async Task<BookingResponse> BookRooms(BookingRequest request)
        {
            return await repoContext.CreateBookingTask(request);
        }
        [HttpGet]
        [Route("Get-Rooms_count")]
        [AllowAnonymous]
        public  async Task<int> GetRoomsCount(string hotelId)
        {
            return await repoContext.GetRoomsCount(hotelId);
        }
        [HttpGet]
        [Route("Get-By-BookingIId")]
        public async Task<BookingResponse> GetBookingById(string bookingId)
        {
            return await repoContext.GetBookingStatus(bookingId);
        }
    }
}
