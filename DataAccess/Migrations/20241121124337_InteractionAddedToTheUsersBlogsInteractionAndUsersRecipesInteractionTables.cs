using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InteractionAddedToTheUsersBlogsInteractionAndUsersRecipesInteractionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a0f62981-ee86-4e9b-b9ff-84523b899833", "df618a45-aeb9-46e1-bb52-dd68ab3ab8db" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f81a3e37-9477-47cb-8c3f-2b8104a88d6c", "78756b50-9bca-45c3-b911-a73b83e7d40a" });
        }
    }
}
