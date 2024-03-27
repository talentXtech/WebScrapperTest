using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentX.WebScrapper.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetailedScrapOutputData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    AllabolagUrl = table.Column<string>(type: "TEXT", nullable: false),
                    OrgNo = table.Column<string>(type: "TEXT", nullable: false),
                    CEO = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    YearOfEstablishment = table.Column<string>(type: "TEXT", nullable: false),
                    Revenue = table.Column<string>(type: "TEXT", nullable: false),
                    EmployeeNames = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedScrapOutputData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InitialScrapOutputData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitialScrapOutputData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LayOffScrapInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    elementName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayOffScrapInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailedScrapOutputData");

            migrationBuilder.DropTable(
                name: "InitialScrapOutputData");

            migrationBuilder.DropTable(
                name: "LayOffScrapInfo");
        }
    }
}
