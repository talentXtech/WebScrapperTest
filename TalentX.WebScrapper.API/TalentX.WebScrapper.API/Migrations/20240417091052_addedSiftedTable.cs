using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentX.WebScrapper.API.Migrations
{
    public partial class addedSiftedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiftedFinalScrapInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sectorurl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    articleUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiftedFinalScrapInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiftedInitialScrapInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sectors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectorUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiftedInitialScrapInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiftedFinalScrapInfo");

            migrationBuilder.DropTable(
                name: "SiftedInitialScrapInfo");
        }
    }
}
