using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class image_overhaul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBlob",
                table: "Recipe_Comments");

            migrationBuilder.DropColumn(
                name: "ImageBlob",
                table: "Blog_Comments");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Recipes",
                newName: "BannerImage");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Blogs",
                newName: "BannerImage");

            migrationBuilder.AddColumn<byte[][]>(
                name: "StepImages",
                table: "Recipes",
                type: "bytea[]",
                nullable: true);

            migrationBuilder.AddColumn<byte[][]>(
                name: "Images",
                table: "Recipe_Comments",
                type: "bytea[]",
                nullable: true);

            migrationBuilder.AddColumn<byte[][]>(
                name: "Images",
                table: "Blogs",
                type: "bytea[]",
                nullable: true);

            migrationBuilder.AddColumn<byte[][]>(
                name: "Images",
                table: "Blog_Comments",
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

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "StepImages",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepImages",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Recipe_Comments");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Blog_Comments");

            migrationBuilder.RenameColumn(
                name: "BannerImage",
                table: "Recipes",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "BannerImage",
                table: "Blogs",
                newName: "Image");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBlob",
                table: "Recipe_Comments",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBlob",
                table: "Blog_Comments",
                type: "bytea",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "dbcfaa40-14a8-4678-95ed-40b86cf28899", "9c0a2ef4-220a-4c75-974a-daa30a222035" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cd9226a4-b234-4029-b761-7dd01fae91f8", "75acaee1-4d3c-441e-be52-846bd6759073" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e40605bb-4591-4fca-8f81-3c29ed07cbb7", "0cb1e2e3-afde-46d2-98a8-3ccfde032d22" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "833366ae-2dec-46a5-b2a2-3b771dbf66aa", "fbdb9e4b-5d46-4caa-a17d-53e9c63ab6b7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e747c990-98e4-4631-908c-13e3c332b63a", "96013185-c77b-4dd9-b489-9737d5d91804" });
        }
    }
}
