using Microsoft.EntityFrameworkCore;

namespace BigBang_Assessment_26_5_23_.Models
{
    public class XYZHotelDbContext : DbContext
    {
        public XYZHotelDbContext(DbContextOptions<XYZHotelDbContext> options):base(options) { }

        public DbSet<XYZHotels> Hotels { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<HotelAddress> HotelAddresses { get; set; }
        public DbSet<Employee_XYZHotels> Employee_XYZHotels { get;set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Login_Logs> Login_Logs { get; set; }
    }
}
