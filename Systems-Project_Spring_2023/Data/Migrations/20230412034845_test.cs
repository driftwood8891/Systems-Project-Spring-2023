using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Systems_Project_Spring_2023.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabAssistants");

            migrationBuilder.AddColumn<int>(
                name: "Item_qty",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item_qty",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "LabAssistants",
                columns: table => new
                {
                    La_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    La_camp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    La_fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    La_lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    La_sch = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabAssistants", x => x.La_id);
                });
        }
    }
}
