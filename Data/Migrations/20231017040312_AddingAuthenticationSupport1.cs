using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingAuthenticationSupport1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "PasswordHash", "UserStatusId" },
                values: new object[] { "Reuben", "$2a$11$pqKoPi/x.A6xVqstF6dDGOrHJJWkSbdpLx024v5qpLDQiSLZl96KK", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "Reuben");
        }
    }
}
