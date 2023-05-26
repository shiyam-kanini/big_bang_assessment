using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public class RepoXYZ : IRepoXYZ
    {
        public readonly Random? random = new Random();
        private readonly XYZHotelDbContext? _context;
        public RepoXYZ(XYZHotelDbContext? context)
        {
            _context = context;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<string> Register(RegisterModel? loginCredentials)
        {
            try
            {
                byte[] passwordHash;
                byte[] passwordSalt;
                CreatePasswordHash(loginCredentials.Password, out passwordHash, out passwordSalt);
                User isUserExists = await _context.Users.Where(name => name.UserName == loginCredentials.UserName).FirstOrDefaultAsync();
                if (isUserExists != null)
                {
                    return "UserName Exists";
                }
                else
                {
                    return "";
                }
                User newUser = new User()
                {
                    UserId = $"UID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}",
                    UserName = loginCredentials.UserName,
                    //UserPasswordHash = passwordHash,

                    
                };
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            
        }   

    }
}
