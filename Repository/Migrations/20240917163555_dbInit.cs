using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class dbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerBirthday = table.Column<DateOnly>(type: "date", nullable: true),
                    CustomerStatus = table.Column<byte>(type: "tinyint", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    RoomTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeNote = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.RoomTypeId);
                });

            migrationBuilder.CreateTable(
                name: "BookingReservations",
                columns: table => new
                {
                    BookingReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BookingStatus = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingReservations", x => x.BookingReservationId);
                    table.ForeignKey(
                        name: "FK_BookingReservations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomInformations",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomDetailDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomMaxCapacity = table.Column<int>(type: "int", nullable: true),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    RoomStatus = table.Column<byte>(type: "tinyint", nullable: true),
                    RoomPricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomInformations", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_RoomInformations_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    BookingReservationId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ActualPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => new { x.BookingReservationId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_BookingDetails_BookingReservations_BookingReservationId",
                        column: x => x.BookingReservationId,
                        principalTable: "BookingReservations",
                        principalColumn: "BookingReservationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetails_RoomInformations_RoomId",
                        column: x => x.RoomId,
                        principalTable: "RoomInformations",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerBirthday", "CustomerFullName", "CustomerStatus", "EmailAddress", "Password", "Telephone" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 1, 1), "John Doe", (byte)1, "johndoe@example.com", "password123", "123456789" },
                    { 2, new DateOnly(1985, 5, 15), "Jane Smith", (byte)1, "janesmith@example.com", "password456", "987654321" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "RoomTypeId", "RoomTypeName", "TypeDescription", "TypeNote" },
                values: new object[,]
                {
                    { 1, "Standard", "Basic room", "No note" },
                    { 2, "Deluxe", "Luxurious room", "Includes breakfast" }
                });

            migrationBuilder.InsertData(
                table: "BookingReservations",
                columns: new[] { "BookingReservationId", "BookingDate", "BookingStatus", "CustomerId", "TotalPrice" },
                values: new object[,]
                {
                    { 1, new DateOnly(2023, 9, 1), (byte)1, 1, 300m },
                    { 2, new DateOnly(2023, 9, 5), (byte)2, 2, 450m }
                });

            migrationBuilder.InsertData(
                table: "RoomInformations",
                columns: new[] { "RoomId", "RoomDetailDescription", "RoomMaxCapacity", "RoomNumber", "RoomPricePerDay", "RoomStatus", "RoomTypeId" },
                values: new object[,]
                {
                    { 1, "A standard room", 2, "101", 100m, (byte)1, 1 },
                    { 2, "A deluxe room", 3, "102", 150m, (byte)1, 2 }
                });

            migrationBuilder.InsertData(
                table: "BookingDetails",
                columns: new[] { "BookingReservationId", "RoomId", "ActualPrice", "EndDate", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, 200m, new DateOnly(2023, 9, 3), new DateOnly(2023, 9, 1) },
                    { 2, 2, 400m, new DateOnly(2023, 9, 8), new DateOnly(2023, 9, 5) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_RoomId",
                table: "BookingDetails",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingReservations_CustomerId",
                table: "BookingReservations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomInformations_RoomTypeId",
                table: "RoomInformations",
                column: "RoomTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "BookingReservations");

            migrationBuilder.DropTable(
                name: "RoomInformations");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
