using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Practice.APIs.Migrations
{
    /// <inheritdoc />
    public partial class AddStockLocationHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockLocationHistories",
                columns: table => new
                {
                    StockLocationHistoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialSubLotId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    MovementType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockLocationHistories", x => x.StockLocationHistoryId);
                    table.ForeignKey(
                        name: "FK_StockLocationHistories_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockLocationHistories_LocationId",
                table: "StockLocationHistories",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockLocationHistories");
        }
    }
}
