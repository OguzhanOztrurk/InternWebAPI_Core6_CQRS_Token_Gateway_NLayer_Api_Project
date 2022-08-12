using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interns",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interns", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Interns_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workplaces",
                columns: table => new
                {
                    WorkplaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkplaceName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WorkplaceExplanation = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    EmployeesCount = table.Column<int>(type: "int", nullable: false),
                    Vision = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    Mission = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workplaces", x => x.WorkplaceId);
                    table.ForeignKey(
                        name: "FK_Workplaces_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EducationLevelEnum = table.Column<int>(type: "int", nullable: false),
                    StartYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    EndYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    EducationStateEnum = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_Educations_Interns_InternId",
                        column: x => x.InternId,
                        principalTable: "Interns",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Talents",
                columns: table => new
                {
                    TalentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TalentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TalentExplanation = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    TalentLevelEnum = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talents", x => x.TalentId);
                    table.ForeignKey(
                        name: "FK_Talents_Interns_InternId",
                        column: x => x.InternId,
                        principalTable: "Interns",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkHistories",
                columns: table => new
                {
                    WorkHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkplaceName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OperationTime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    WorkStateEnum = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHistories", x => x.WorkHistoryId);
                    table.ForeignKey(
                        name: "FK_WorkHistories_Interns_InternId",
                        column: x => x.InternId,
                        principalTable: "Interns",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Educations_InternId",
                table: "Educations",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_Talents_InternId",
                table: "Talents",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHistories_InternId",
                table: "WorkHistories",
                column: "InternId");

            migrationBuilder.CreateIndex(
                name: "IX_Workplaces_AdminId",
                table: "Workplaces",
                column: "AdminId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Talents");

            migrationBuilder.DropTable(
                name: "WorkHistories");

            migrationBuilder.DropTable(
                name: "Workplaces");

            migrationBuilder.DropTable(
                name: "Interns");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
