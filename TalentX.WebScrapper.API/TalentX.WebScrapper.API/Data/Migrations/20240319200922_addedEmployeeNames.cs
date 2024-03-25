using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentX.WebScrapper.API.Data.Migrations
{
    public partial class addedEmployeeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "employeeNames",
                table: "DetailedScrapOutputData",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employeeNames",
                table: "DetailedScrapOutputData");
        }
    }
}
