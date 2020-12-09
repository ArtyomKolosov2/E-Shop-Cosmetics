using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Shop_Cosmetic.Migrations
{
    public partial class OrdersDetailsNewFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "OrderDetail",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderDetail");
        }
    }
}
