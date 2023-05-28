using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using Microsoft.EntityFrameworkCore;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public class RepoRooms : IRepoRooms
    {
        private readonly Random random = new();
        private readonly XYZHotelDbContext _context;
        private readonly IConfiguration configuration;
        public RepoRooms(XYZHotelDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        List<Rooms> rooms = new();
        Rooms? newRoom = new();
        RoomResponse roomResponse = new();
        public async Task<RoomResponse> DeleteRoom(string id)
        {
            try
            {
                newRoom = await _context.Rooms.FindAsync(id != null? id:"");
                if(newRoom == null)
                {
                    AddResponse(false, "No Room Found", rooms);
                    return roomResponse;
                }
                await _context.Rooms.Where(x => (x.RoomId != null ? x.RoomId : "").Equals(id)).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
                rooms.Add(newRoom);
                AddResponse(true, "1 Room Deleted", rooms);
                return roomResponse;
            }
            catch(Exception ex)
            {
                AddResponse(false, ex.Message, rooms);
                return roomResponse;
            }
        }        

        public async Task<RoomResponse> PutRoom(string id, RoomsRequest room)
        {
            try
            {
                XYZHotels? isHotelExists = await _context.Hotels.FindAsync(room.XYZHotelId != null ? room.XYZHotelId : "");
                if (isHotelExists == null)
                {
                    AddResponse(false, "Hotel Doesn't exists", rooms);
                    return roomResponse;
                }
                AddRoom(id, isHotelExists);
                rooms.Add(newRoom);
                _context.Rooms.Update(newRoom);
                await _context.SaveChangesAsync();
                AddResponse(true, "1 Record Updated", rooms);
                return roomResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, rooms);
                return roomResponse;
            }
        }

        /*GET ALL  ROOMS*/
        public async Task<RoomResponse> GetRooms()
        {
            try
            {
                rooms = await _context.Rooms.ToListAsync();
                if(rooms.Count <= 0)
                {
                    AddResponse(false, "No Data Found", rooms);
                    return roomResponse;
                }
                AddResponse(true, $"{rooms.Count} Records Found", rooms);
                return roomResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, rooms);
                return roomResponse;
            }
        }
        /*GET ROOM BY ID*/
        public async Task<RoomResponse> GetRoomById(string id)
        {
            try
            {
                rooms = await _context.Rooms.Where(x => (x.RoomId ?? "").Equals(id)).Include(y => y.XYZHotelId).ToListAsync();
                if(rooms.Count <= 0)
                {
                    AddResponse(false, "No Data Found",rooms);
                    return roomResponse;
                }
                AddResponse(true, $"{rooms.Count} records Found", rooms);
                return roomResponse;
            }
            catch (Exception ex)
            {
                AddResponse(false, ex.Message, rooms);
                return roomResponse;
            }
        }
        /*POST ROOM*/
        public async Task<RoomResponse> PostRoom(RoomsRequest room)
        {
            try
            {
                XYZHotels? isHotelExists = await _context.Hotels.FindAsync(room.XYZHotelId != null ? room.XYZHotelId : "");
                if(isHotelExists == null)
                {
                    AddResponse(false, "Hotel Doesn't exists", rooms);
                    return roomResponse;
                }
                AddRoom($"RID{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}",isHotelExists);
                rooms.Add(newRoom);
                await _context.Rooms.AddAsync(newRoom);
                await _context.SaveChangesAsync();
                AddResponse(true, "1 Record Added", rooms);
                return roomResponse;
            }
            catch (Exception ex)
            {
                AddResponse (false, ex.Message, rooms);
                return roomResponse;
            }
        }
        public void AddResponse(bool success, string message, List<Rooms> rooms)
        {
            roomResponse = new RoomResponse()
            {
                Success = success,
                Message = message,
                Rooms = rooms,
            };
        }
        public void AddRoom(string roomId, XYZHotels hotel)
        {
            newRoom = new()
            {
                RoomId = roomId,
                XYZHotelId = hotel
            };
        }
        
    }
}
