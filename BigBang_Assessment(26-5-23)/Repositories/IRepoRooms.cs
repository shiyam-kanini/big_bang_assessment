using BigBang_Assessment_26_5_23_.Model_Request_Response_;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public interface IRepoRooms
    {
        Task<RoomResponse> GetRooms();
        Task<RoomResponse> GetRoomById(string id);
        Task<RoomResponse> PostRoom(RoomsRequest rooms);
        Task<RoomResponse> PutRoom(string id, RoomsRequest rooms);
        Task<RoomResponse> DeleteRoom(string id);
    }
}
