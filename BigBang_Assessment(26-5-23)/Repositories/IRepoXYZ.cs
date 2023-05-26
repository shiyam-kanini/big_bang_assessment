using BigBang_Assessment_26_5_23_.Model_Request_Response_;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public interface IRepoXYZ
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordKey);
        Task<string> Register(RegisterModel loginCredentials);
        Task<string> Login(RegisterModel loginCredentials);
    }
}
