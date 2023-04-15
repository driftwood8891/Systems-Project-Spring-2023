using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Systems_Project_Spring_2023.Data.Migrations
{
    public partial class LabAssistantFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabAssistant",
                columns: table => new
                {
                    La_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    La_fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    La_lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    La_camp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    La_sch = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabAssistant", x => x.La_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabAssistant");
        }
    }
}
