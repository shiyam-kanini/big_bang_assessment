using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigBang_Assessment_26_5_23_.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelAddress_Hotels_HotelId",
                table: "HotelAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelAddress",
                table: "HotelAddress");

            migrationBuilder.RenameTable(
                name: "HotelAddress",
                newName: "HotelAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_HotelAddress_HotelId",
                table: "HotelAddresses",
                newName: "IX_HotelAddresses_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelAddresses",
                table: "HotelAddresses",
                column: "HotelAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelAddresses_Hotels_HotelId",
                table: "HotelAddresses",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelAddresses_Hotels_HotelId",
                table: "HotelAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelAddresses",
                table: "HotelAddresses");

            migrationBuilder.RenameTable(
                name: "HotelAddresses",
                newName: "HotelAddress");

            migrationBuilder.RenameIndex(
                name: "IX_HotelAddresses_HotelId",
                table: "HotelAddress",
                newName: "IX_HotelAddress_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelAddress",
                table: "HotelAddress",
                column: "HotelAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelAddress_Hotels_HotelId",
                table: "HotelAddress",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId");
        }
    }
}
