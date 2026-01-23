using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Demo01.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Major = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Age", "FullName", "Major" },
                values: new object[,]
                {
                    { 1, 20, "Nguyen Van A", "IT" },
                    { 2, 19, "Tran Thi B", "Marketing" },
                    { 3, 21, "Le Van C", "IT" },
                    { 4, 22, "Pham Thi D", "Graphic Design" },
                    { 5, 18, "Hoang Van E", "Business" },
                    { 6, 20, "Do Thi F", "IT" },
                    { 7, 23, "Ngo Van G", "Marketing" },
                    { 8, 19, "Bui Thi H", "Graphic Design" },
                    { 9, 21, "Vu Van I", "Business" },
                    { 10, 22, "Dinh Thi J", "IT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
