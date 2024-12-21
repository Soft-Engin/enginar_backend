using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class remove_blog_images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "ImagesCount",
                table: "Recipe_Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImagesCount",
                table: "Blog_Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagesCount",
                table: "Recipe_Comments");

            migrationBuilder.DropColumn(
                name: "ImagesCount",
                table: "Blog_Comments");

            migrationBuilder.AddColumn<byte[][]>(
                name: "Images",
                table: "Blogs",
                type: "bytea[]",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2b0ed863-501d-4244-a23c-10610de8065f", "d29b2abd-f964-4cad-8dbb-5fb703cedef3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8f993bec-7706-49c0-abc4-272cc223f1e5", "30d82e47-f477-4170-8407-e7b6483be932" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "50da4878-f1db-448c-9423-d7045a24c737", "408fa33f-0a30-4112-a6df-a29a89ae6f19" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6ad4f051-2577-4cdd-ae2c-0cdc2aacc184", "1b03ecb2-114d-4bc8-a9ef-ea4a74aeb2f1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5ed8234f-358b-43cc-9677-95d8ca94e954", "3b74cc0b-579e-4ef4-8b68-00bd12e7935f" });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Images",
                value: null);
        }
    }
}
