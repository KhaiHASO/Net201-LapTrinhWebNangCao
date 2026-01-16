using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Demo02.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassRooms",
                columns: table => new
                {
                    ClassRoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRooms", x => x.ClassRoomId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GPA = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClassRoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_ClassRooms_ClassRoomId",
                        column: x => x.ClassRoomId,
                        principalTable: "ClassRooms",
                        principalColumn: "ClassRoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClassRooms",
                columns: new[] { "ClassRoomId", "ClassCode", "ClassName" },
                values: new object[,]
                {
                    { 1, "NET201", "Lập trình Web Nâng cao - Sáng" },
                    { 2, "NET202", "Lập trình Web Nâng cao - Chiều" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "ClassRoomId", "DateOfBirth", "Email", "FullName", "GPA", "PhoneNumber", "StudentCode" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2003, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "annv@fpt.edu.vn", "Nguyễn Văn An", 8.5m, "0901234567", "PH12345" },
                    { 2, 1, new DateTime(2003, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "binhtt@fpt.edu.vn", "Trần Thị Bình", 9.2m, "0907654321", "PH12346" },
                    { 3, 2, new DateTime(2003, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "cuonglv@fpt.edu.vn", "Lê Văn Cường", 7.8m, "0912345678", "PH12347" },
                    { 4, 2, new DateTime(2003, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "dungpt@fpt.edu.vn", "Phạm Thị Dung", 8.9m, "0923456789", "PH12348" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassRooms_ClassCode",
                table: "ClassRooms",
                column: "ClassCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassRoomId",
                table: "Students",
                column: "ClassRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "ClassRooms");
        }
    }
}
