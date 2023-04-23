using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Systems_Project_Spring_2023.Data.Migrations
{
    public partial class seeddatastatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Status_code", "Status_desc" },
                values: new object[,]
                {
                    { "1", "Checked_In" },
                    { "2", "Checked_Out" },
                    { "3", "Dead" },
                    { "4", "Lost" },
                    { "5", "In_Transit" },
                    { "6", "Needs_Repair" },
                    { "7", "Pending" },
                    { "8", "Ready" },
                    { "9", "Unknown" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Status_code",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Status_code",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Status_code",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Status_code",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Status_code",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Status_code",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Status_code",
                keyValue: "7");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Status_code",
                keyValue: "8");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Status_code",
                keyValue: "9");
        }
    }
}
