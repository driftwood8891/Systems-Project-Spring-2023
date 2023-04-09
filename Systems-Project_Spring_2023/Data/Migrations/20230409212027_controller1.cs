using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Systems_Project_Spring_2023.Data.Migrations
{
    public partial class controller1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabAssistants",
                columns: table => new
                {
                    La_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    La_fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    La_lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    La_camp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    La_sch = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabAssistants", x => x.La_id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Status_code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Status_desc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Status_code);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Student_macid = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Student_fname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Student_lname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Student_cmail = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Student_pmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Student_phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Student_ephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Student_addr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Student_cour = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Student_camp = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Student_instr = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Student_macid);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Item_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Item_barcode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Item_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Item_cost = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Item_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Item_note = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Status_code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Student_macid = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Student_macid1 = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Status_code1 = table.Column<string>(type: "nvarchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Item_id);
                    table.ForeignKey(
                        name: "FK_Items_Statuses_Status_code1",
                        column: x => x.Status_code1,
                        principalTable: "Statuses",
                        principalColumn: "Status_code");
                    table.ForeignKey(
                        name: "FK_Items_Students_Student_macid1",
                        column: x => x.Student_macid1,
                        principalTable: "Students",
                        principalColumn: "Student_macid");
                });

            migrationBuilder.CreateTable(
                name: "Kit_types",
                columns: table => new
                {
                    Kt_id = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Kt_item_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Kt_item_qty = table.Column<int>(type: "int", nullable: false),
                    Kt_item_cost = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Kt_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Item_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Item_id1 = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kit_types", x => x.Kt_id);
                    table.ForeignKey(
                        name: "FK_Kit_types_Items_Item_id1",
                        column: x => x.Item_id1,
                        principalTable: "Items",
                        principalColumn: "Item_id");
                });

            migrationBuilder.CreateTable(
                name: "Kits",
                columns: table => new
                {
                    Kit_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Kit_barcd = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Kit_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Kit_qty = table.Column<int>(type: "int", nullable: false),
                    Kit_desc = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Kit_cost = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Kit_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kit_note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kt_id = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Status_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Student_macid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Student_macid1 = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Status_code1 = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    Kit_typeKt_id = table.Column<string>(type: "nvarchar(8)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kits", x => x.Kit_id);
                    table.ForeignKey(
                        name: "FK_Kits_Kit_types_Kit_typeKt_id",
                        column: x => x.Kit_typeKt_id,
                        principalTable: "Kit_types",
                        principalColumn: "Kt_id");
                    table.ForeignKey(
                        name: "FK_Kits_Statuses_Status_code1",
                        column: x => x.Status_code1,
                        principalTable: "Statuses",
                        principalColumn: "Status_code");
                    table.ForeignKey(
                        name: "FK_Kits_Students_Student_macid1",
                        column: x => x.Student_macid1,
                        principalTable: "Students",
                        principalColumn: "Student_macid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_Status_code1",
                table: "Items",
                column: "Status_code1");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Student_macid1",
                table: "Items",
                column: "Student_macid1");

            migrationBuilder.CreateIndex(
                name: "IX_Kit_types_Item_id1",
                table: "Kit_types",
                column: "Item_id1");

            migrationBuilder.CreateIndex(
                name: "IX_Kits_Kit_typeKt_id",
                table: "Kits",
                column: "Kit_typeKt_id");

            migrationBuilder.CreateIndex(
                name: "IX_Kits_Status_code1",
                table: "Kits",
                column: "Status_code1");

            migrationBuilder.CreateIndex(
                name: "IX_Kits_Student_macid1",
                table: "Kits",
                column: "Student_macid1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kits");

            migrationBuilder.DropTable(
                name: "LabAssistants");

            migrationBuilder.DropTable(
                name: "Kit_types");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
