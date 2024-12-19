using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixDateTimeKind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b6a3c32d-ed6b-492c-a308-7c47e5090e06", "54931b6b-3e61-430b-9e51-215a1102abc2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e25c33b0-fc76-4324-b4e2-4e372446193d", "eb85f64f-c467-4654-9c05-1630e05923e0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f0e9007f-9490-4125-90df-4abaa49510f7", "eb1e7e7a-69ae-4c78-bb31-398779868fa9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "79127b85-9c72-420a-92b7-00467f61eb4f", "a7e7dd33-feb7-4cf9-b001-fb17cc27b8d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1f491a12-7c00-4bfa-a9dc-e48876e51c7f", "6ada84f5-66a9-4b1d-9a26-a67c6bb6a28f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "998c4d74-15dd-4147-a7b5-0310a75fa9ff", "c5c9e8a8-91df-4da7-b0f9-01b7ecc5e139" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a6992c24-9c42-4e3f-85e6-fe307009f840", "bd5f42c7-b5c7-4d7a-b8a7-e5193338031f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2c6075ab-655e-4dae-b198-292fe1153770", "6d007c95-b5e5-46fb-b202-e9fe460f4432" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e5130e3b-8cb2-4197-8ecd-6cc9a7af9aed", "8f1e3606-2498-4198-b717-1295d8b0fb10" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "22bdb7fb-2cd4-47a5-aadf-899d43cb84c0", "dee7168a-9346-4d19-9ac3-84255b603595" });
        }
    }
}
