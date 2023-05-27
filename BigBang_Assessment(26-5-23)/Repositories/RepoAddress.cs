using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public class RepoAddress : IRepoAddress
    {
        private readonly Random random = new();
        private readonly XYZHotelDbContext _context;
        private readonly IConfiguration configuration;
        public RepoAddress(XYZHotelDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        List<HotelAddress> address = new();
        AddressResponse addressResponse = new();
        public async Task<AddressResponse> PutAddress(string id, AddressRequest addressRequest)
        {
            try
            {
                HotelAddress? addressExists = await _context.HotelAddresses.FindAsync(id);
                if (addressExists == null)
                {
                    addressResponse = new AddressResponse()
                    {
                        Success = false, Message = "Address doesn't Exists", HotelAddress = address,
                    };
                    return addressResponse;
                }
                HotelAddress newAddress = new()
                {
                    HotelAddressId = id, StreetName = addressRequest.StreetName,
                    City = addressRequest.City, Pincode = addressRequest.Pincode,
                    Hotel = await _context.Hotels.FindAsync(addressRequest.HotelId),
                };
                address.Add(newAddress); addressResponse = new AddressResponse()
                {
                    Message = $"{await _context.HotelAddresses.Where(x => (x.HotelAddressId ?? "").Equals(addressRequest.HotelId)).Select(y => y).FirstOrDefaultAsync()} has been changed to {addressRequest.StreetName}",
                    Success = true, HotelAddress = address,
                };
                _context.HotelAddresses.Update(newAddress); await _context.SaveChangesAsync(); return addressResponse;
            }
            catch (Exception ex)
            {
                addressResponse = new AddressResponse()
                {
                    Success = false, Message = ex.Message, HotelAddress = null,
                };
                return addressResponse;
            }
        }
        public async Task<AddressResponse> DeleteAddress(string id)
        {
            try
            {
                HotelAddress? addressExists = await _context.HotelAddresses.FindAsync(id);
                if (addressExists == null)
                {
                    addressResponse = new AddressResponse()
                    {
                        Success = false, Message = "Address doesnt Exists", HotelAddress = address,
                    };
                    return addressResponse;
                }
                address.Add(addressExists);
                _context.Hotels.Where(x => (x.HotelId != null ? x.HotelId : "").Equals(id)).ExecuteDelete();
                await _context.SaveChangesAsync();
                addressResponse = new AddressResponse()
                {
                    Message = $"1 Record has been Deleted",
                    Success = true, HotelAddress = address,
                };
                await _context.HotelAddresses.Where(x => x.HotelAddressId == id).ExecuteDeleteAsync();
                return addressResponse;
            }
            catch (Exception ex)
            {
                addressResponse = new AddressResponse()
                {
                    Success = false, Message = ex.StackTrace, HotelAddress = null,
                };
                return addressResponse;
            }
        }

        public async Task<AddressResponse> GetAddress()
        {
            try
            {
                address =  await _context.HotelAddresses.Include(x => x.Hotel).ToListAsync();
                if (address.Count <= 0)
                {
                    addressResponse.Success = true;
                    addressResponse.Message = "No Records Found";
                    addressResponse.HotelAddress = address;
                    return addressResponse;
                }
                addressResponse.Success = true;
                addressResponse.Message = $"{address.Count} records found";
                addressResponse.HotelAddress = address;
                return addressResponse;
            }
            catch (Exception ex)
            {
                addressResponse.Success = false;
                addressResponse.Message = ex.Message;
                addressResponse.HotelAddress = null;
                return addressResponse;
            }
        }

        public async Task<AddressResponse> GetAddressById(string id)
        {
            try
            {
                address = await _context.HotelAddresses.Where(x => (x.HotelAddressId ?? "").Equals(id)).ToListAsync();
                if (address.Count <= 0)
                {
                    addressResponse.Success = false; addressResponse.Message = "No Records Found";
                    addressResponse.HotelAddress = address; return addressResponse;
                }
                addressResponse.Success = true; addressResponse.Message = $"{address.Count} records found";
                addressResponse.HotelAddress = address; return addressResponse;
            }
            catch (Exception ex)
            {
                addressResponse.Success = false; addressResponse.Message = ex.Message;
                addressResponse.HotelAddress = null; return addressResponse;
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
                        Success = false, Message = "Hotel Exists in the same city, Contact Admin ", HotelAddress = addressExists,
                    };
                    return postResponse;
                }
                HotelAddress newAddress = new HotelAddress()
                {
                    HotelAddressId = $"AID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}",
                    City = address.City, StreetName = address.StreetName, Pincode = address.Pincode,
                    Hotel = await _context.Hotels.FindAsync(address.HotelId),
                };
                addressExists.Add(newAddress);
                postResponse = new AddressResponse()
                {
                    Success = true,
                    Message = "Address Added",
                    HotelAddress = addressExists,
                };
                await _context.HotelAddresses.AddAsync(newAddress); await _context.SaveChangesAsync();
                return postResponse;
            }
            catch (Exception ex)
            {
                postResponse = new AddressResponse()
                {
                    Success = true, Message = ex.Message, HotelAddress = null,
                };
                return postResponse;
            }
        }

    }
}
