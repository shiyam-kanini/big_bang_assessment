using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using System.IdentityModel.Tokens.Jwt;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public interface IRepoXYZ
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordKey);
        Task<Commonresponse> Register(RegisterModel loginCredentials);
        Task<Commonresponse> Login(LoginRequest loginCredentials);
        string CreateToken(LoginRequest loginCredentials, string role);
        




        
    }
}
