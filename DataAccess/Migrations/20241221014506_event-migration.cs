using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class eventmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Recipes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Recipe_Likes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Recipe_Comments",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Recipe_Bookmarks",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blogs",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blog_Likes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blog_Comments",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blog_Bookmarks",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatorId = table.Column<string>(type: "text", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    BodyText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "User_Event_Participations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Event_Participations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Event_Participations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Event_Participations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                values: new object[] { "cc101e71-31c3-4617-a937-bf803453fd84", "945da122-702a-43d0-a1ba-48027dc89a87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "07125bb8-98cf-469f-8aa3-b3614eb5cba1", "abfbf528-ef93-4a68-ba33-84b060cbc7d1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e313c33f-1067-410c-9859-b8faa3c4b417", "b99c1df9-7914-47a1-894d-7204a48c3252" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "691537e8-7251-4e13-8a62-6c9f53d61e22", "adec1588-b5d1-4fe6-b527-fe29834a2c4a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "27c45632-9a27-4fc5-9993-3ad7cc3fb664", "4db157a4-2fb6-4b16-b9d7-dd87e311fa2d" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Turkey" },
                    { 2, "USA" }
                });

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
                table: "Districts",
                columns: new[] { "Id", "CityId", "Name", "PostCode" },
                values: new object[,]
                {
                    { 1, 1, "Kadikoy", 34710 },
                    { 2, 1, "Besiktas", 34353 }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "DistrictId", "Name", "Street" },
                values: new object[,]
                {
                    { 1, 1, "Office Address", "Main Avenue" },
                    { 2, 2, "Home Address", "Second Street" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AddressId", "BodyText", "CreatedAt", "CreatorId", "Date", "Title" },
                values: new object[] { 1, 1, "Celebrate the New Year with us!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "1", new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "New Year's Eve Party" });

            migrationBuilder.InsertData(
                table: "Events_Requirements",
                columns: new[] { "Id", "EventId", "RequirementId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "User_Event_Participations",
                columns: new[] { "Id", "EventId", "UserId" },
                values: new object[] { 1, 1, "1" });

            migrationBuilder.CreateIndex(
                name: "IX_Events_AddressId",
                table: "Events",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatorId",
                table: "Events",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Requirements_EventId",
                table: "Events_Requirements",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Requirements_RequirementId",
                table: "Events_Requirements",
                column: "RequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Event_Participations_EventId",
                table: "User_Event_Participations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Event_Participations_UserId",
                table: "User_Event_Participations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Events_Requirements");

            migrationBuilder.DropTable(
                name: "User_Event_Participations");

            migrationBuilder.DropTable(
                name: "Requirements");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Recipes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Recipe_Likes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Recipe_Comments",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Recipe_Bookmarks",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blogs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blog_Likes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blog_Comments",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Blog_Bookmarks",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
