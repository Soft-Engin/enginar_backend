using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedUsernameDefaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "UserName" },
                values: new object[] { "87fe00bb-3470-4d4a-8cff-2f1b068efefc", "070455e1-503a-488c-9650-ac8414d58a9e", "EnginarAdam" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "UserName" },
                values: new object[] { "ff015020-b6d4-48a1-aa14-41455a138afa", "d1b01a88-6de0-4d8e-9c06-3885edb81385", "EnginarKadın" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3", 0, null, "2a34591b-59be-488d-a092-09e74b03e65a", "Users", null, false, "Engin", "Çocuk", false, null, null, null, null, null, false, 1, "ca36665b-d769-43e1-aecf-e673518fdbea", false, "EnginarÇocuk" },
                    { "4", 0, null, "855b9a24-1d85-404e-a44a-1b1b90277c27", "Users", null, false, "Engin", "Yaşlı", false, null, null, null, null, null, false, 1, "121c145a-5f5f-4842-a7e9-d693d6be49a6", false, "EnginarYaşlı" },
                    { "5", 0, null, "3b0a24d9-bf61-4c44-8bc4-5de1d37ec310", "Users", null, false, "Engin", "Enginar", false, null, null, null, null, null, false, 2, "00d980d2-aafa-49f7-b1f0-e8bf139fe5b9", false, "EnginarDouble" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "UserName" },
                values: new object[] { "09a48430-5dba-49a3-96b6-ee01398f48ff", "33f0d857-c578-4004-86d6-cf4f9e636a4b", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "UserName" },
                values: new object[] { "d4998fb3-7788-4478-81d1-2da059e32419", "8ef5ef2d-c01e-4350-87e6-2a118baf9b24", null });
        }
    }
}
