using Microsoft.EntityFrameworkCore.Migrations;

namespace P1_RepositoryLayer.Migrations
{
    public partial class UpdateOrderIsCartActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCartActive",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCartActive",
                table: "Orders");
        }
    }
}
