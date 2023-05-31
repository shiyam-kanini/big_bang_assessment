using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigBang_Assessment_26_5_23_.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeDOJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeDOB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeQualifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeePasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    EmployeePasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelId);
                });

            migrationBuilder.CreateTable(
                name: "Login_Logs",
                columns: table => new
                {
                    SessionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login_Logs", x => x.SessionId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserPasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "HotelAddresses",
                columns: table => new
                {
                    HotelAddressId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelAddresses", x => x.HotelAddressId);
                    table.ForeignKey(
                        name: "FK_HotelAddresses_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId");
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    XYZHotelIdHotelId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_XYZHotelIdHotelId",
                        column: x => x.XYZHotelIdHotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId");
                });

            migrationBuilder.CreateTable(
                name: "Employee_XYZHotels",
                columns: table => new
                {
                    EHID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    XYZHotelHotelId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmployeesEmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_XYZHotels", x => x.EHID);
                    table.ForeignKey(
                        name: "FK_Employee_XYZHotels_Employees_EmployeesEmployeeId",
                        column: x => x.EmployeesEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Employee_XYZHotels_Hotels_XYZHotelHotelId",
                        column: x => x.XYZHotelHotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId");
                    table.ForeignKey(
                        name: "FK_Employee_XYZHotels_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoOfRooms = table.Column<int>(type: "int", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId1",
                table: "Bookings",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_XYZHotels_EmployeesEmployeeId",
                table: "Employee_XYZHotels",
                column: "EmployeesEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_XYZHotels_RoleId",
                table: "Employee_XYZHotels",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_XYZHotels_XYZHotelHotelId",
                table: "Employee_XYZHotels",
                column: "XYZHotelHotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelAddresses_HotelId",
                table: "HotelAddresses",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_XYZHotelIdHotelId",
                table: "Rooms",
                column: "XYZHotelIdHotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Employee_XYZHotels");

            migrationBuilder.DropTable(
                name: "HotelAddresses");

            migrationBuilder.DropTable(
                name: "Login_Logs");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
