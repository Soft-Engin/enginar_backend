using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRequirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events_Requirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    RequirementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events_Requirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Requirements_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Requirements_Requirements_RequirementId",
                        column: x => x.RequirementId,
                        principalTable: "Requirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Turkey" },
                    { 2, "USA" }
                });

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CityId", "PostCode" },
                values: new object[] { 1, 34710 });

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CityId", "PostCode" },
                values: new object[] { 1, 34353 });

            migrationBuilder.InsertData(
                table: "Requirements",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Guests must confirm attendance before the event.", "RSVP Required" },
                    { 2, "Guests are required to follow the formal dress code.", "Dress Code" },
                    { 3, "Only guests aged 18 and above are allowed to attend.", "Age Limit" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Istanbul" },
                    { 2, 2, "New York" }
                });

            migrationBuilder.InsertData(
                table: "Events_Requirements",
                columns: new[] { "Id", "EventId", "RequirementId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_Requirements_EventId",
                table: "Events_Requirements",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Requirements_RequirementId",
                table: "Events_Requirements",
                column: "RequirementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events_Requirements");

            migrationBuilder.DropTable(
                name: "Requirements");

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1bd9149d-7e9c-4cb3-9ed7-0b7ac5a7fcdc", "9393ad01-eeb2-422e-a658-61f183bdbafc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ab97fbec-d9d1-42f8-8cf1-c11d2bb0e2fa", "ddd2ab2f-f860-4881-908d-c787ac75365b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "99592d1b-36b9-4f32-b79d-ce5fc4bb4994", "2bf5262a-a423-4dcd-b93e-e22151607008" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "41e3255f-8695-4342-8858-0025c902edfd", "665d83f2-c612-4155-bd2f-4817c9debb0e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a39b4451-eda8-4037-9ce8-5a484d741931", "8893cca7-8ec9-40f9-a6c7-0c07eebda3ed" });

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CityId", "PostCode" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CityId", "PostCode" },
                values: new object[] { 0, 0 });
        }
    }
}
