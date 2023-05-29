using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public class RepoXYZ : IRepoXYZ
    {
        private readonly Random random = new();
        private readonly XYZHotelDbContext _context;
        private readonly IConfiguration Configuration;
        public RepoXYZ(XYZHotelDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public async Task<Commonresponse> Register( RegisterModel loginCredentials)
        {
            Commonresponse registerResponse = new();
            try
            {
                byte[] passwordHash;
                byte[] passwordSalt;
                CreatePasswordHash(loginCredentials.Password ?? "", out passwordHash, out passwordSalt);
                User? isUserExists = await _context.Users.Where(name => (name.UserName ?? "") == (loginCredentials.UserName ?? "")).FirstOrDefaultAsync();
                if (isUserExists != null)
                {
                    registerResponse.status = false;
                    registerResponse.message = "User Already Exists";
                    return registerResponse;
                }                
                User newUser = new()
                {
                    UserId = $"UID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}",
                    UserName = loginCredentials.UserName,
                    UserPasswordHash = passwordHash,
                    UserPasswordSalt= passwordSalt                    
                };
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                registerResponse.status = true;
                registerResponse.message = $"{newUser.UserId} Register Proceed to Login";
                registerResponse.token = $"Login to generate token Mr.{loginCredentials.UserName}";
                return registerResponse;
            }
            catch (Exception ex)
            {
                registerResponse.status = false;
                registerResponse.message = ex.Message;
                return registerResponse;
            }            
        }
        public async Task<Commonresponse> LoginUser([System.Diagnostics.CodeAnalysis.NotNull] LoginRequest loginCredentials)
        {
            Commonresponse loginResponse = new();
            try
            {
                User? isUserExists = await _context.Users.FindAsync(loginCredentials.UserId ?? "");
                if (isUserExists == null)
                {
                    loginResponse.status = false;
                    loginResponse.message = "User Not Found";
                    return loginResponse;
                }
                Login_Logs newUser = new()
                {
                    SessionId = $"SID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}",
                    LoginId = loginCredentials.UserId,
                };
                loginResponse.token = CreateToken(loginCredentials, "user");
                await _context.Login_Logs.AddAsync(newUser);
                await _context.SaveChangesAsync();
                loginResponse.status = true;
                loginResponse.message = $"{newUser.LoginId} Logged in a session {newUser.SessionId}";
                return loginResponse;
            }
            catch (Exception ex)
            {
                loginResponse.status = false;
                loginResponse.message = ex.StackTrace;
                return loginResponse;
            }
        }

        public async Task<Commonresponse> LoginEmployee([System.Diagnostics.CodeAnalysis.NotNull] LoginRequest loginCredentials)
        {
            Commonresponse loginResponse = new();
            try
            {
                Employees? isEmployeeExists = await _context.Employees.FindAsync(loginCredentials.UserId ?? "");
                if (isEmployeeExists == null)
                {
                    loginResponse.status = false;
                    loginResponse.message = "User Not Found";
                    return loginResponse;
                }
                string employeeRole = await _context.Employee_XYZHotels.Where(x => x.Employees.EmployeeId.Equals(loginCredentials.UserId)).Select(x => x.Role.RoleId).FirstOrDefaultAsync();
                loginResponse.token = CreateToken(loginCredentials, employeeRole ?? "");
                Login_Logs newUser = new()
                {
                    SessionId = $"SID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}",
                    LoginId = loginCredentials.UserId,
                };
                await _context.Login_Logs.AddAsync(newUser);
                await _context.SaveChangesAsync();
                loginResponse.status = true;
                loginResponse.message = $"{newUser.LoginId}({employeeRole}) Logged in a session {newUser.SessionId}";
                return loginResponse;
            }
            catch (Exception ex)
            {
                loginResponse.status = false;
                loginResponse.message = ex.StackTrace;
                return loginResponse;
            }
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public string CreateToken(LoginRequest loginCredentials, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginCredentials.UserId ?? ""),
                new Claim(ClaimTypes.Role, role)
            };

            string secretKey = Configuration["JWT:Key"] ?? ""; // Retrieve the secret key from configuration

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: Configuration["JWT:ValidIssuer"],
                audience: Configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        


        
        
    }
}
