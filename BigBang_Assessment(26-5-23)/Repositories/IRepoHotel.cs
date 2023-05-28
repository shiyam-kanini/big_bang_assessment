using BigBang_Assessment_26_5_23_.Model_Request_Response_;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public interface IRepoHotel
    {
        Task<HotelResponse> GetHotels();
        Task<HotelResponse> GetHotelById(string id);
        Task<HotelResponse> PostHotel(string hotelName);
        Task<HotelResponse> PutHotel(string id, string hotelName);
        Task<HotelResponse> DeleteHotel(string id);
    }
}
