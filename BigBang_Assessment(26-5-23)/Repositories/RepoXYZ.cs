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
                result.Success = true;
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
                if (hotelExists.Count > 0)
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
                await _context.Hotels.AddAsync(newHotel);
                await _context.SaveChangesAsync();
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
            HotelResponse putResponse;
            try
            {
                XYZHotels hotelExists = await _context.Hotels.FindAsync(id);
                if (hotelExists == null)
                {
                    hotels.Add(hotelExists);
                    putResponse = new HotelResponse()
                    {
                        Success = false,
                        Message = "Hotel doesnt Exists",
                        Hotels = hotels,
                    };
                    return putResponse;
                }
                
                XYZHotels newHotel = new XYZHotels()
                {
                    HotelId = id,
                    HotelName = name,
                };
                hotels.Add(newHotel);
                
                putResponse = new HotelResponse()
                {
                    Success = true,
                    Message = $"{await _context.Hotels.Where(x => x.HotelId.Equals(id)).Select(y => y.HotelName).FirstOrDefaultAsync()} has been changed to {name}",
                    Hotels = hotels,
                };
                _context.Hotels.Update(newHotel);
                await _context.SaveChangesAsync();
                return putResponse;
            }
            catch (Exception ex)
            {
                putResponse = new HotelResponse()
                {
                    Success = false,
                    Message = ex.StackTrace,
                    Hotels = null,
                };
                return putResponse;
            }
        }

        public async Task<HotelResponse> DeleteHotel(string id)
        {
            List<XYZHotels> hotels = new List<XYZHotels>();
            HotelResponse deleteResponse;
            try
            {
                XYZHotels hotelExists = await _context.Hotels.FindAsync(id);
                if (hotelExists == null)
                {
                    hotels.Add(hotelExists);
                    deleteResponse = new HotelResponse()
                    {
                        Success = false,
                        Message = "Hotel doesnt Exists",
                        Hotels = hotels,
                    };
                    return deleteResponse;
                }                
                hotels.Add(hotelExists);
                _context.Hotels.Where(x => x.HotelId.Equals(id)).ExecuteDelete();
                await _context.SaveChangesAsync();
                deleteResponse = new HotelResponse()
                {
                    Success = true,
                    Message = $"{await _context.Hotels.Where(x => x.HotelId.Equals(id)).Select(y => y.HotelName).FirstOrDefaultAsync()} has been Deleted",
                    Hotels = hotels,
                };
               return deleteResponse;
            }
            catch (Exception ex)
            {
                deleteResponse = new HotelResponse()
                {
                    Success = false,
                    Message = ex.StackTrace,
                    Hotels = null,
                };
                return deleteResponse;
            }
        }


        
        public Task<AddressResponse> PutAddress(int id)
        {
            throw new NotImplementedException();
        }
        public Task<AddressResponse> DeleteAddress(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AddressResponse> GetAddress()
        {
            AddressResponse result = new AddressResponse();
            try
            {
                var hotels = _context.HotelAddresses.ToList();
                if (hotels.Count <= 0)
                {
                    result.Success = true;
                    result.Message = "No Records Found";
                    result.HotelAddress = hotels;
                    return result;
                }
                result.Success = true;
                result.Message = $"{hotels.Count} records found";
                result.HotelAddress = hotels;
                return result;
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.HotelAddress = null;
                return result;
            }
        }

        public async Task<AddressResponse> GetAddressById(int id)
        {
            AddressResponse result = new AddressResponse();
            try
            {
                var address = await _context.HotelAddresses.Where(id => id.HotelAddressId.Equals(id)).ToListAsync();
                if (address.Count <= 0)
                {
                    result.Success = false;
                    result.Message = "No Records Found";
                    result.HotelAddress = address;
                    return result;
                }
                result.Success = true;
                result.Message = $"{address.Count} records found";
                result.HotelAddress = address;
                return result;
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.HotelAddress = null; return result;
            }
        }

        public async Task<AddressResponse> PostAddress(AddressRequest address)
        {
            AddressResponse postResponse;
            try
            {
                List<HotelAddress> addressExists = await _context.HotelAddresses.Where(x => x.City == address.City).ToListAsync();
                if (addressExists.Count > 0)
                {
                    postResponse = new AddressResponse()
                    {
                        Success = false,
                        Message = "Hotel Exists in the same city ",
                        HotelAddress = addressExists,
                    };
                    return postResponse;
                }
                HotelAddress newAddress = new HotelAddress()
                {
                    HotelAddressId = $"AID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}",
                    City = address.City,
                    StreetName= address.StreetName,
                    Pincode= address.Pincode,
                    Hotel = await _context.Hotels.FindAsync(address.HotelId),
                };

                addressExists.Add(newAddress);
                postResponse = new AddressResponse()
                {
                    Success = true,
                    Message = "Address Added",
                    HotelAddress = addressExists,
                };
                await _context.HotelAddresses.AddAsync(newAddress);
                await _context.SaveChangesAsync();
                return postResponse;
            }
            catch (Exception ex)
            {
                postResponse = new AddressResponse()
                {
                    Success = true,
                    Message = ex.Message,
                    HotelAddress = null,
                };
                return postResponse;
            }
        }
    }
}
