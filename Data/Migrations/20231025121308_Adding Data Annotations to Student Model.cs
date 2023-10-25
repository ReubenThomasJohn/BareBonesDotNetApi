using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingDataAnnotationstoStudentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "Reuben",
                column: "PasswordHash",
                value: "$2a$11$XNRqV4KMaKKfefMSUtvhMu3T6HHKw8hpPobTjKNHPw2dMoWG54uta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "Reuben",
                column: "PasswordHash",
                value: "$2a$11$pqKoPi/x.A6xVqstF6dDGOrHJJWkSbdpLx024v5qpLDQiSLZl96KK");
        }
    }
}
