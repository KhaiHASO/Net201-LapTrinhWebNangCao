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
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "IsActive", "Name", "Status" },
                values: new object[,]
                {
                    { 1, true, "Nguyen Van A", "Active" },
                    { 2, false, "Tran Thi B", "Inactive" },
                    { 3, true, "Le Van C", "Active" },
                    { 4, true, "Pham Thi D", "New" },
                    { 5, false, "Hoang Van E", "Inactive" },
                    { 6, true, "Do Thi F", "Active" },
                    { 7, true, "Ngo Van G", "Active" }
                });

            // Create Stored Procedure: GetActiveCustomers
            var spGetActive = @"
                CREATE PROCEDURE GetActiveCustomers
                AS
                BEGIN
                    SELECT * FROM Customers WHERE IsActive = 1
                END";
            migrationBuilder.Sql(spGetActive);

            // Create Stored Procedure: UpdateCustomer
            var spUpdate = @"
                CREATE PROCEDURE UpdateCustomer
                    @CustomerId INT,
                    @NewStatus NVARCHAR(50)
                AS
                BEGIN
                    UPDATE Customers
                    SET Status = @NewStatus
                    WHERE Id = @CustomerId
                END";
            migrationBuilder.Sql(spUpdate);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetActiveCustomers");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateCustomer");
        }
    }
}
