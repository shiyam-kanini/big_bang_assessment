using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BigBang_Assessment_26_5_23_.Controllers
{
    [ApiController]
    [Route("[controller]/actions")]
    [Authorize(Roles = "RID574")]
    public class RoomsController : Controller
    {
        private readonly IRepoRooms repoContext;
        public RoomsController(IRepoRooms repoContext)
        {
            this.repoContext = repoContext;
        }
        [HttpGet]
        public async Task<RoomResponse> GetHotelRooms()
        {
            return await repoContext.GetRooms();
        }
        [HttpGet("id")]
        public async Task<RoomResponse> GetHotelAddressById(string id)
        {
            return await repoContext.GetRoomById(id);
        }
        [HttpPost]
        public async Task<RoomResponse> PostHotelRoom(RoomsRequest rooms)
        {
            return await repoContext.PostRoom(rooms);
        }
        [HttpPut("ID")]
        public async Task<RoomResponse> PutRoom(string id, RoomsRequest rooms)
        {
            return await repoContext.PutRoom(id, rooms);
        }
        [HttpDelete("ID")]
        public async Task<RoomResponse> DeleteRoom(string id)
        {
            return await repoContext.DeleteRoom(id);
        }
    }
}
