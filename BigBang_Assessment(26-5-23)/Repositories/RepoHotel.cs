using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public class RepoHotel : IRepoHotel
    {
        private readonly Random random = new();
        private readonly XYZHotelDbContext _context;
        public RepoHotel(XYZHotelDbContext context)
        {
            _context = context;
        }

        List<XYZHotels> hotels = new();
        XYZHotels? newHotel = new();
        HotelResponse hotelResponse = new();
        public async Task<HotelResponse> DeleteHotel(string id)
        {
            try
            {
                newHotel = await _context.Hotels.FindAsync(id != null ? id : "");
                if (newHotel != null)
                {
                    hotels.Add(newHotel);
                    AddResponse(true, $"Hotel {id} not found", hotels);
                    return hotelResponse;
                }
                await _context.Rooms.Where(x => (x.RoomId != null ? x.RoomId : "").Equals(id)).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
                hotels.Add(newHotel);
                AddResponse(true, "1 Room Deleted", hotels);
                return hotelResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.StackTrace, hotels);
                return hotelResponse;
            }
        }

        public async Task<HotelResponse> PutHotel(string id, string hotelName)
        {
            try
            {
                newHotel = await _context.Hotels.FindAsync(id != null ? id : "");
                if (newHotel == null)
                {
                    AddResponse(true, "Hotel Doesn't exists", hotels);
                    return hotelResponse;
                }
                AddHotel(id ?? "", hotelName);
                hotels.Add(newHotel);
                _context.Hotels.Update(newHotel);
                await _context.SaveChangesAsync();
                AddResponse(true, "1 Record Updated", hotels);
                return hotelResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, hotels);
                return hotelResponse;
            }
        }

        /*GET ALL Addresses*/
        public async Task<HotelResponse> GetHotels()
        {
            try
            {
                hotels = await _context.Hotels.Include(y => y.HotelAddresses).Include(z => z.Employee_XYZHotels).Include(a => a.Rooms).ToListAsync();
                if (hotels.Count <= 0)
                {
                    AddResponse(true, "No Data Found", hotels);
                    return hotelResponse;
                }
                AddResponse(true, $"{hotels.Count} Records Found", hotels);
                return hotelResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, hotels);
                return hotelResponse;
            }
        }
        /*GET Address BY ID*/
        public async Task<HotelResponse> GetHotelById(string id)
        {
            try
            {
                hotels = await _context.Hotels.Where(x => (x.HotelId ?? "").Equals(id)).Include(y => y.HotelAddresses).Include(z => z.Employee_XYZHotels).Include(a => a.Rooms).ToListAsync();
                if (hotels.Count <= 0)
                {
                    AddResponse(true, $"No Hotel Found with id - {id}", hotels);
                    return hotelResponse;
                }
                AddResponse(true, $"{hotels.Count} records Found", hotels);
                return hotelResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, hotels);
                return hotelResponse;
            }
        }
        /*POST ROOM*/
        public async Task<HotelResponse> PostHotel(string hotelName)
        {
            try
            {
                newHotel = await _context.Hotels.Where(x => (x.HotelName ?? "").Equals(hotelName)).FirstOrDefaultAsync();
                if (newHotel != null)
                {
                    AddResponse(true, "Hotel Already exists", hotels);
                    return hotelResponse;
                }
                AddHotel($"XYZ{random.Next(0, 9)}", hotelName);
                hotels.Add(newHotel);
                await _context.Hotels.AddAsync(newHotel);
                await _context.SaveChangesAsync();
                AddResponse(true, "1 Record Added", hotels);
                return hotelResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, hotels);
                return hotelResponse;
            }
        }
        public void AddResponse(bool success, string message, List<XYZHotels> hotels)
        {
            hotelResponse = new()
            {
                Success = success,
                Message = message,
                Hotels = hotels,
            };
        }
        public void AddHotel(string hotelId, string hotelName)
        {
            newHotel = new()
            {
                HotelId= hotelId,
                HotelName= hotelName,
            };
        }
    }
}
