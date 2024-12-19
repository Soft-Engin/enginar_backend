using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EventMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_CreatorId",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7000efdf-81d3-4573-afd4-2f25a3837025", "2e45abee-5ec2-473e-be63-260d6c905e9d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "507d2226-94b7-48af-9f70-87ba37d388a6", "c32b71ea-093a-44d2-9ed4-18dcfd9ccb43" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9b70fb94-83cd-4926-9ccd-1df5bf297305", "1b4a929d-a859-4d49-8bd7-3b5a84e179a1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7dbc5f87-6760-453f-a4ca-5989f45b870f", "e280137a-42cf-47a3-bac0-239042354d3f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "59949897-f69e-4f87-947f-a6680457c82a", "95953d5d-f407-439c-8a1e-06f3b4db18e5" });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "CityId", "Name", "PostCode" },
                values: new object[,]
                {
                    { 1, 0, "Kadikoy", 0 },
                    { 2, 0, "Besiktas", 0 }
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
                columns: new[] { "Id", "AddressId", "BodyText", "CreatorId", "Date", "Title" },
                values: new object[] { 1, 1, "Celebrate the New Year with us!", "1", new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "New Year's Eve Party" });

            migrationBuilder.InsertData(
                table: "User_Event_Participations",
                columns: new[] { "Id", "EventId", "UserId" },
                values: new object[] { 1, 1, "1" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_CreatorId",
                table: "Events",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_CreatorId",
                table: "Events");

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User_Event_Participations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bf50b1b7-1f53-4772-a7e7-35e31d7ff672", "8bfd5b79-6472-4512-ba29-2c8f08e626fb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "33c29f00-183a-4306-a37f-8a4cea624005", "437eaa93-ce3a-4223-9eaa-2bb6808759ee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bf29cc47-ee02-49e6-8fbb-6dff5be72844", "98b236e4-c91c-4440-8632-e801f7272c28" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d054eced-dc09-4f77-8418-504505beab56", "3f76e0dd-e623-4386-950b-b023d658f0ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c498b1d1-75af-4a16-aaf5-fe3b5938c432", "b6bdaf1c-4bc8-4384-9696-f9af082cf16f" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_CreatorId",
                table: "Events",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
