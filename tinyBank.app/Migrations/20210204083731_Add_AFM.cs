using Microsoft.EntityFrameworkCore.Migrations;

namespace tinyBank.app.Migrations
{
    public partial class Add_AFM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AFM",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AFM",
                table: "Customer");
        }
    }
}
