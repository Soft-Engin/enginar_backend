using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class user_images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "BannerImage",
                table: "AspNetUsers",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImage",
                table: "AspNetUsers",
                type: "bytea",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "BannerImage", "ConcurrencyStamp", "ProfileImage", "SecurityStamp" },
                values: new object[] { null, "81422ce0-6585-4024-9410-625da4ca9569", null, "d1f33fa2-7eb4-43da-9217-45d35e4dd3b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "BannerImage", "ConcurrencyStamp", "ProfileImage", "SecurityStamp" },
                values: new object[] { null, "81e8ab88-faf9-4ccd-8e83-9d4657cf0b0c", null, "7adc282f-6e74-4bdf-af72-a8111ec31efc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "BannerImage", "ConcurrencyStamp", "ProfileImage", "SecurityStamp" },
                values: new object[] { null, "a23f679a-4ea1-4db6-b287-d3a01b9513b6", null, "2aca338e-923a-45a2-915b-4167ea7dedcf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "BannerImage", "ConcurrencyStamp", "ProfileImage", "SecurityStamp" },
                values: new object[] { null, "2dbc633a-4705-4b85-9320-5253133abf9f", null, "e2b02e31-8494-4165-af3d-d3b95eb6204b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "BannerImage", "ConcurrencyStamp", "ProfileImage", "SecurityStamp" },
                values: new object[] { null, "545ee04e-9d6c-4d32-b5e4-ae9327f89a2d", null, "fdf476c4-a64f-44f1-8019-a7cae40c0d19" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "67e84a7e-69ca-4b9e-8596-bcfd90a9f22e", "144a694c-173d-41f2-b937-87c097683e57" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e4b9212b-70c6-4e30-b81a-4ce69b177006", "ca4e33cf-d480-4da3-9c58-d97c9e098028" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3ecb06f0-b1e7-45da-befa-e2ccc1f85fd6", "f27e01b7-e08b-4ede-addd-84e51ac6b7c0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fea5b68b-c47f-40ee-9ea4-6f778c3cb3fd", "af569d70-eefa-4865-8722-5a2f12dd39b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d68c45ed-a13d-45ff-a44e-c1eae8694d3a", "620a1319-e8f4-40f4-bba6-e8d1f1a878fa" });
        }
    }
}
