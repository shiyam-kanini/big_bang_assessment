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

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public class RepoXYZ : IRepoXYZ
    {
        private readonly Random? random = new Random();
        private readonly XYZHotelDbContext _context;
        private readonly IConfiguration configuration;
        public RepoXYZ(XYZHotelDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }
        public async Task<Commonresponse> Register([System.Diagnostics.CodeAnalysis.NotNull] RegisterModel loginCredentials)
        {
            Commonresponse registerResponse = new Commonresponse();
            try
            {
                byte[] passwordHash;
                byte[] passwordSalt;
                CreatePasswordHash(loginCredentials.Password, out passwordHash, out passwordSalt);
                User isUserExists = await _context.Users.Where(name => name.UserName == loginCredentials.UserName).FirstOrDefaultAsync();
                if (isUserExists != null)
                {
                    registerResponse.status = false;
                    registerResponse.message = "User Already Exists";
                    return registerResponse;
                }                
                User newUser = new User()
                {
                    UserId = $"UID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}",
                    UserName = loginCredentials.UserName,
                    UserPasswordHash = passwordHash,
                    UserPasswordSalt= passwordSalt                    
                };
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                registerResponse.status = true;
                registerResponse.message = $"{newUser.UserName} Register Proceed to Login";
                return registerResponse;
            }
            catch (Exception ex)
            {
                registerResponse.status = false;
                registerResponse.message = ex.Message;
                return registerResponse;
            }            
        }
        public async Task<Commonresponse> Login([System.Diagnostics.CodeAnalysis.NotNull] LoginRequest? loginCredentials)
        {
            Commonresponse loginResponse = new Commonresponse();
            try
            {
                User isUserExists = await _context.Users.FindAsync(loginCredentials.UserId);
                if (isUserExists == null)
                {
                    loginResponse.status = false;
                    loginResponse.message = "User Not Found";
                    return loginResponse;
                }
                Login_Logs newUser = new Login_Logs()
                {
                    SessionId = $"SID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}",
                    LoginId = loginCredentials.UserId,
                };
                await _context.Login_Logs.AddAsync(newUser);
                await _context.SaveChangesAsync();
                loginResponse.status = true;
                loginResponse.message = $"{newUser.LoginId} Logged in a session {newUser.SessionId}";
                loginResponse.token = CreatToken(loginCredentials);
                return loginResponse;
            }
            catch (Exception ex)
            {
                loginResponse.status = false;
                loginResponse.message = ex.StackTrace;
                return loginResponse;
            }
        }
        
        Login_Logs currLogger = new Login_Logs();

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public string CreatToken(LoginRequest loginCredentials)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginCredentials.UserId),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSetting:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public async Task<HotelResponse> GetHotels()
        {
            var hotels = _context.Hotels.ToList();
            HotelResponse result = new HotelResponse();
            if(hotels.Count <= 0)
            {
                result.Success = false;
                result.Message = "No Records Found";
                result.Hotels= hotels;
                return result;
            }
            result.Success = true;
            result.Message = $"{hotels.Count} records found";
            result.Hotels = hotels;
            return result;
        }
        public async Task<HotelResponse> GetHotelById(int id)
        {
            var hotels = await _context.Hotels.Where(id => id.HotelId.Equals(id)).ToListAsync();
            HotelResponse result = new HotelResponse();
            if (hotels.Count <= 0)
            {
                result.Success = false;
                result.Message = "No Records Found";
                result.Hotels = hotels;
                return result;
            }
            result.Success = true;
            result.Message = $"{hotels.Count} records found";
            result.Hotels = hotels;
            return result;
        }

        public async Task<HotelResponse> PostHotel(string hotelName)
        {
            HotelResponse postResponse;
            try
            {
                List<XYZHotels> hotelExists = await _context.Hotels.Where(x => x.HotelName == hotelName).ToListAsync();
                if (hotelExists.Count <= 0)
                {
                    postResponse = new HotelResponse()
                    {
                        Success = false,
                        Message = "Hotel with same name Exists",
                        Hotels = hotelExists,
                    };
                    return postResponse;
                }
                XYZHotels newHotel = new XYZHotels()
                {
                    HotelId = $"XYZ{random.Next(0, 9)}",
                    HotelName = hotelName,
                };
                hotelExists.Add(newHotel);
                postResponse = new HotelResponse()
                {
                    Success = true,
                    Message = "Hotel Added",
                    Hotels = hotelExists,
                };
                return postResponse;
            }
            catch(Exception ex)
            {
                postResponse = new HotelResponse()
                {
                    Success = true,
                    Message = ex.Message,
                    Hotels = null,
                };
                return postResponse;
            }
        }

        public async Task<HotelResponse> PutHotel(string id, string name)
        {
            List<XYZHotels> hotels = new List<XYZHotels>();
            HotelResponse postResponse;
            try
            {
                XYZHotels hotelExists = await _context.Hotels.FindAsync(id);
                if (hotelExists == null)
                {
                    hotels.Add(hotelExists);
                    postResponse = new HotelResponse()
                    {
                        Success = false,
                        Message = "Hotel doesnt Exists",
                        Hotels = hotels,
                    };
                    return postResponse;
                }
                
                XYZHotels newHotel = new XYZHotels()
                {
                    HotelId = $"XYZ{random.Next(0, 9)}",
                    HotelName = name,
                };
                hotels.Add(newHotel);
                
                postResponse = new HotelResponse()
                {
                    Success = true,
                    Message = "Hotel Added",
                    Hotels = hotels,
                };
                return postResponse;
            }
            catch (Exception ex)
            {
                postResponse = new HotelResponse()
                {
                    Success = true,
                    Message = ex.Message,
                    Hotels = null,
                };
                return postResponse;
            }
        }

        public Task<HotelResponse> DeleteHotel()
        {
            throw new NotImplementedException();
        }

        

        public Task<HotelResponse> PutAddress()
        {
            throw new NotImplementedException();
        }

        public Task<HotelResponse> DeleteAddress()
        {
            throw new NotImplementedException();
        }

        public Task<HotelResponse> GetAddress()
        {
            throw new NotImplementedException();
        }

        public Task<HotelResponse> GetAddressById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HotelResponse> PostAddress()
        {
            throw new NotImplementedException();
        }
    }
}
