using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketBooking.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "LoginDetails",
                columns: table => new
                {
                    LoginID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LoginType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginDetails", x => x.LoginID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DirectorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProducerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    Story = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieID);
                });

            migrationBuilder.CreateTable(
                name: "Theatres",
                columns: table => new
                {
                    TheatreID = table.Column<int>(type: "int", nullable: false),
                    TheatreName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NumberofSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theatres", x => x.TheatreID);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    ShowID = table.Column<int>(type: "int", nullable: false),
                    MovieID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TheatreID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlatinumSeatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GoldSeatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SilverSeatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.ShowID);
                    table.ForeignKey(
                        name: "FK_Shows_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shows_Theatres_TheatreID",
                        column: x => x.TheatreID,
                        principalTable: "Theatres",
                        principalColumn: "TheatreID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShowID = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    SeatType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BookingStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeatNumbers = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_Shows_ShowID",
                        column: x => x.ShowID,
                        principalTable: "Shows",
                        principalColumn: "ShowID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ShowID",
                table: "Bookings",
                column: "ShowID");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_MovieID",
                table: "Shows",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_TheatreID",
                table: "Shows",
                column: "TheatreID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "LoginDetails");

            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Theatres");
        }
    }
}
