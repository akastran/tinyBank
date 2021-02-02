using Microsoft.EntityFrameworkCore.Migrations;

namespace tinyBank.app.Migrations
{
    public partial class Add_CustomerTypes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                columns: table => new
                {
                    CustomerTypeName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.CustomerTypeName);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_CustomerTypeName",
                table: "CustomerTypes",
                column: "CustomerTypeName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerTypes");
        }
    }
}
