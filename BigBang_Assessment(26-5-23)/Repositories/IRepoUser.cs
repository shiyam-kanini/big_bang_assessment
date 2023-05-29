using BigBang_Assessment_26_5_23_.Model_Request_Response_;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public interface IRepoUser
    {
        Task<BookingResponse> CreateBookingTask(BookingRequest booking);

        Task<int> GetRoomsCount(string booking);
        Task<BookingResponse> GetBookingStatus(string booking);


    }
}
