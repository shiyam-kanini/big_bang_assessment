using BigBang_Assessment_26_5_23_.Model_Request_Response_;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public interface IRepoAddress
    {
        Task<AddressResponse> GetAddress();
        Task<AddressResponse> GetAddressById(string id);
        Task<AddressResponse> PostAddress(AddressRequest address);
        Task<AddressResponse> PutAddress(string id, AddressRequest address);
        Task<AddressResponse> DeleteAddress(string id);
    }
}
