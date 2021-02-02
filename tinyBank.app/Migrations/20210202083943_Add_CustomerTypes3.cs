using Microsoft.EntityFrameworkCore.Migrations;

namespace tinyBank.app.Migrations
{
    public partial class Add_CustomerTypes3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerTypes_CustomerTypeName",
                table: "CustomerTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_CustomerTypeName",
                table: "CustomerTypes",
                column: "CustomerTypeName",
                unique: true);
        }
    }
}
