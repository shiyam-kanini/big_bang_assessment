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
        public RepoAddress(XYZHotelDbContext context)
        {
            _context = context;
        }

        /*List<HotelAddress> address = new();
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
        }*/
        List<HotelAddress> addresses = new();
        HotelAddress? newAddress = new();
        AddressResponse addressResponse = new();
        public async Task<AddressResponse> DeleteAddress(string id)
        {
            try
            {
                newAddress = await _context.HotelAddresses.FindAsync(id != null ? id : "");
                if (newAddress == null)
                {
                    AddResponse(false, "No Room Found", addresses);
                    return addressResponse;
                }
                await _context.Rooms.Where(x => (x.RoomId != null ? x.RoomId : "").Equals(id)).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
                addresses.Add(newAddress);
                AddResponse(true, "1 Room Deleted", addresses);
                return addressResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, addresses);
                return addressResponse;
            }
        }

        public async Task<AddressResponse> PutAddress(string id, AddressRequest address)
        {
            try
            {
                XYZHotels? isAddressExists = await _context.Hotels.FindAsync(address.HotelId != null ? address.HotelId : "");
                if (isAddressExists == null)
                {
                    AddResponse(false, $"Hotel with id - {address.HotelId} Doesn't exists", addresses);
                    return addressResponse;
                }
                AddAddress(id, address);
                addresses.Add(newAddress);
                _context.HotelAddresses.Update(newAddress);
                await _context.SaveChangesAsync();
                AddResponse(true, "1 Record Updated", addresses);
                return addressResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, addresses);
                return addressResponse;
            }
        }

        /*GET ALL Addresses*/
        public async Task<AddressResponse> GetAddress()
        {
            try
            {
                addresses = await _context.HotelAddresses.Include(x => x.Hotel).ToListAsync();
                if (addresses.Count <= 0)
                {
                    AddResponse(false, "No Data Found", addresses);
                    return addressResponse;
                }
                AddResponse(true, $"{addresses.Count} Records Found", addresses);
                return addressResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, addresses);
                return addressResponse;
            }
        }
        /*GET Address BY ID*/
        public async Task<AddressResponse> GetAddressById(string id)
        {
            try
            {
                addresses = await _context.HotelAddresses.Where(x => (x.HotelAddressId ?? "").Equals(id)).Include(x => x.Hotel).ToListAsync();
                if (addresses.Count <= 0)
                {
                    AddResponse(false, "No Data Found", addresses);
                    return addressResponse;
                }
                AddResponse(true, $"{addresses.Count} records Found", addresses);
                return addressResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, addresses);
                return addressResponse;
            }
        }
        /*POST ROOM*/
        public async Task<AddressResponse> PostAddress(AddressRequest address)
        {
            try
            {
                XYZHotels? isHotelExists = await _context.Hotels.FindAsync(address.HotelId != null ? address.HotelId : "");
                if (isHotelExists == null)
                {
                    AddResponse(false, $"Hotel with id - {address.HotelId} Doesn't exists", addresses);
                    return addressResponse;
                }
                AddAddress($"RID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}", address);
                addresses.Add(newAddress);
                await _context.HotelAddresses.AddAsync(newAddress);
                await _context.SaveChangesAsync();
                AddResponse(true, "1 Record Added", addresses);
                return addressResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.StackTrace, addresses);
                return addressResponse;
            }
        }
        public void AddResponse(bool success, string message, List<HotelAddress> address)
        {
            addressResponse = new()
            {
                Success = success,
                Message = message,
                HotelAddress = address,
            };
        }
        public async void AddAddress(string addressId, AddressRequest address)
        {
            newAddress = new()
            {
                HotelAddressId = addressId,
                City= address.City,
                StreetName= address.StreetName,
                Pincode= address.Pincode,
                Hotel = await _context.Hotels.FindAsync(address.HotelId)
            };
        }

    }
}