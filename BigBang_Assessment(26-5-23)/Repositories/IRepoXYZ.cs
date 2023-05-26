using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public interface IRepoXYZ
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordKey);
        Task<Commonresponse> Register(RegisterModel loginCredentials);
        Task<Commonresponse> Login(LoginRequest loginCredentials);
        string CreatToken(LoginRequest loginCredentials);

        Task<HotelResponse> GetHotels();
        Task<HotelResponse> GetHotelById(int id);
        Task<HotelResponse> PostHotel(string hotelName);
        Task<HotelResponse> PutHotel(string id, string name);
        Task<HotelResponse> DeleteHotel(string id);




        Task<AddressResponse> GetAddress();
        Task<AddressResponse> GetAddressById(int id);
        Task<AddressResponse> PostAddress(AddressRequest address);
        Task<AddressResponse> PutAddress(int id);
        Task<AddressResponse> DeleteAddress(int id);
    }
}
