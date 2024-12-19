using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SizeTimeColumnsRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreperationTime",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServingSize",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ae7f2868-8869-4368-8671-416475758f5c", "c1075fe1-0a82-499e-8a47-20af72dcab20" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "52f86069-fa74-453d-a316-e4467e7de884", "a444e79d-4cd3-47de-8825-81794dd1108f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7e452018-a16b-4846-8215-cb8fed0f431e", "a611a7ef-3716-4827-b7d8-4cf9b0266422" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "314e1db6-b2be-4e5b-b7f5-304df91651a4", "4d1397ea-a0c9-48d2-87e4-81fa8aaf2d40" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4c0a2a3d-7235-4aee-acdf-3c273610915e", "c6cb7f0e-c3dc-4954-9b43-4d0b38cf711a" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PreperationTime", "ServingSize" },
                values: new object[] { 45, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreperationTime",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ServingSize",
                table: "Recipes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5bb2b362-6920-4596-a1b7-a011f61f0881", "d4b2d6bd-b096-4eaf-85de-05bb214fd3b2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "66e7468b-a04a-407e-bce8-983d524fc64e", "9c6baae5-4fc9-428d-8f97-842f36664049" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "19bff6c4-265a-49b3-803c-3e11ddd4ec70", "b17632a8-e2cb-49e5-b8e1-f8f650468b6d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1fef66d8-56e3-4ce8-bb94-c8ab6ebd1fab", "d9c268ac-91e7-4f3f-8888-28cfee5453e8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4a9da2f0-f478-4f67-99ae-8019e705706f", "9de5aa5b-9035-4cfd-8afb-35153720f7e3" });
        }
    }
}
