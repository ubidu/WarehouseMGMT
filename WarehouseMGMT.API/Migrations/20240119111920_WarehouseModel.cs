using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseMGMT.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsedCapacity",
                table: "Warehouses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "UsedCapacity",
                table: "Warehouses",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
