using BigBang_Assessment_26_5_23_.Model_Request_Response_;
using BigBang_Assessment_26_5_23_.Models;
using Microsoft.EntityFrameworkCore;

namespace BigBang_Assessment_26_5_23_.Repositories
{
    public class RepoUser : IRepoUser
    {
        private readonly Random random = new();

        private readonly XYZHotelDbContext _context;
        public RepoUser(XYZHotelDbContext context)
        {
            _context = context;
        }
        List<Booking> bookings = new();
        Booking? newBooking = new();
        BookingResponse bookingResponse = new();


        public async Task<BookingResponse> CreateBookingTask(BookingRequest booking)
        {
            try
            {
                User? isUser = await _context.Users.FindAsync(booking.UserId);
                Rooms[] rooms = await _context.Rooms.Where(x => x.IsAvailable == true).Where(y => (y.XYZHotelId.HotelId ?? "").Equals(booking.HotelId)).ToArrayAsync();
                if(isUser == null)
                {
                    AddResponse(false, "User Doesnt exists", bookings);
                    return bookingResponse;
                }
                else if(booking.NoOfRooms >= rooms.Length)
                { 
                    AddResponse(false, "No More Rooms Available", bookings);
                    return bookingResponse; 
                }
                BookRoom(rooms, booking.NoOfRooms);
                AddBooking(booking);
                bookings.Add(newBooking);
                AddResponse(true, $"Booked Successfully BookingId : {newBooking.BookingId} ", bookings);
                _context.Rooms.UpdateRange(rooms);
                await _context.Bookings.AddAsync(newBooking);
                await _context.SaveChangesAsync();
                return bookingResponse; 
            }
            catch(Exception ex)
            {
                AddResponse(false, ex.InnerException.Message, bookings);
                return bookingResponse;
            }
            
        }
        public async Task<int> GetRoomsCount(string hotelId)
        {
            try
            {
                List<Rooms> rooms = await _context.Rooms.Where(x => x.IsAvailable == true).Where(y => (y.XYZHotelId.HotelId ?? "").Equals(hotelId)).ToListAsync();
                return rooms.Count;
            }
            catch
            {
                return -1;
            }
        }
        //public async Task<string> 
        public void BookRoom(Rooms[] rooms, int roomCount)
        {
            for (int i = 0; i<roomCount; i++)
            {
                rooms[i].IsAvailable = false;
            }
            
        }
        public void AddResponse(bool success, string message, List<Booking> bookings)
        {
            bookingResponse = new()
            {
                Success = success,
                Message = message,
                Bookings = bookings
            };
        }
        public async void AddBooking(BookingRequest booking)
        {
            newBooking = new()
            {
                BookingId = $"BId{random.Next(0, 9)}{random.Next(0, 9)}{random.Next(0, 9)}",
                UserId = await _context.Users.FindAsync(booking.UserId),
                BookingDate = booking.BookingDate,
                BookedOn = DateTime.Now,
                NoOfRooms = booking.NoOfRooms,
                IsBooked = true,
            };
        }

        public async Task<BookingResponse> GetBookingStatus(string booking)
        {
            bookings = await _context.Bookings.Where(x => (x.BookingId ?? "").Equals(booking)).ToListAsync();
            if(newBooking == null)
            {
                AddResponse(true, "No Booking History Found With the Booking Id", bookings);
                return bookingResponse;
            }
            AddResponse(true, $"{bookings.Count} records foundn for booking id", bookings);
            return bookingResponse;
        }

        
    }
}
