using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Systems_Project_Spring_2023.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kit_types",
                columns: table => new
                {
                    Kt_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Kt_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Kt_desc = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Kt_cost = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Kt_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kit_types", x => x.Kt_id);
                });

            migrationBuilder.CreateTable(
                name: "LabAssistant",
                columns: table => new
                {
                    La_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    La_fname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    La_lname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    La_camp = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    La_sch = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabAssistant", x => x.La_id);
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
                    Student_cour = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Student_camp = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Student_instr = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Student_macid);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Item_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Item_barcode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Item_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Item_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                name: "Kits",
                columns: table => new
                {
                    Kit_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Kit_barcd = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Kit_name = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Kit_desc = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Kit_cost = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Kit_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kit_note = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Kt_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Status_code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Student_macid = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Student_macid1 = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Status_code1 = table.Column<string>(type: "nvarchar(2)", nullable: true),
                    Kit_typeKt_id = table.Column<string>(type: "nvarchar(10)", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Status_code1",
                table: "Items",
                column: "Status_code1");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Student_macid1",
                table: "Items",
                column: "Student_macid1");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Kits");

            migrationBuilder.DropTable(
                name: "LabAssistant");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Kit_types");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
