﻿// <auto-generated />
using System;
using BigBang_Assessment_26_5_23_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BigBang_Assessment_26_5_23_.Migrations
{
    [DbContext(typeof(XYZHotelDbContext))]
    [Migration("20230530082958_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.Booking", b =>
                {
                    b.Property<string>("BookingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BookedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsBooked")
                        .HasColumnType("bit");

                    b.Property<int>("NoOfRooms")
                        .HasColumnType("int");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BookingId");

                    b.HasIndex("UserId1");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.Employee_XYZHotels", b =>
                {
                    b.Property<string>("EHID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeesEmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("XYZHotelHotelId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EHID");

                    b.HasIndex("EmployeesEmployeeId");

                    b.HasIndex("RoleId");

                    b.HasIndex("XYZHotelHotelId");

                    b.ToTable("Employee_XYZHotels");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.Employees", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeDOB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeDOJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("EmployeePasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("EmployeePasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("EmployeeQualifications")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.HotelAddress", b =>
                {
                    b.Property<string>("HotelAddressId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HotelId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Pincode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HotelAddressId");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelAddresses");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.Login_Logs", b =>
                {
                    b.Property<string>("SessionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SessionId");

                    b.ToTable("Login_Logs");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.Rooms", b =>
                {
                    b.Property<string>("RoomId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("XYZHotelIdHotelId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RoomId");

                    b.HasIndex("XYZHotelIdHotelId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("UserPasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("UserPasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.XYZHotels", b =>
                {
                    b.Property<string>("HotelId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HotelName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HotelId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.Booking", b =>
                {
                    b.HasOne("BigBang_Assessment_26_5_23_.Models.User", "UserId")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId1");

                    b.Navigation("UserId");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.Employee_XYZHotels", b =>
                {
                    b.HasOne("BigBang_Assessment_26_5_23_.Models.Employees", "Employees")
                        .WithMany("Employee_XYZHotels")
                        .HasForeignKey("EmployeesEmployeeId");

                    b.HasOne("BigBang_Assessment_26_5_23_.Models.Role", "Role")
                        .WithMany("Employee_XYZHotels")
                        .HasForeignKey("RoleId");

                    b.HasOne("BigBang_Assessment_26_5_23_.Models.XYZHotels", "XYZHotel")
                        .WithMany("Employee_XYZHotels")
                        .HasForeignKey("XYZHotelHotelId");

                    b.Navigation("Employees");

                    b.Navigation("Role");

                    b.Navigation("XYZHotel");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.HotelAddress", b =>
                {
                    b.HasOne("BigBang_Assessment_26_5_23_.Models.XYZHotels", "Hotel")
                        .WithMany("HotelAddresses")
                        .HasForeignKey("HotelId");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.Rooms", b =>
                {
                    b.HasOne("BigBang_Assessment_26_5_23_.Models.XYZHotels", "XYZHotelId")
                        .WithMany("Rooms")
                        .HasForeignKey("XYZHotelIdHotelId");

                    b.Navigation("XYZHotelId");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.Employees", b =>
                {
                    b.Navigation("Employee_XYZHotels");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.Role", b =>
                {
                    b.Navigation("Employee_XYZHotels");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.User", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("BigBang_Assessment_26_5_23_.Models.XYZHotels", b =>
                {
                    b.Navigation("Employee_XYZHotels");

                    b.Navigation("HotelAddresses");

                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
