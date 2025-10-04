using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLotAPP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingNumberPlate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Plate",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Plate",
                table: "Cars");
        }
    }
}
