using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class cascade : Migration
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

            migrationBuilder.CreateTable(
                name: "Event_Bookmarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Bookmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Bookmarks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Bookmarks_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event_Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    CommentText = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Images = table.Column<byte[][]>(type: "bytea[]", nullable: true),
                    ImagesCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Comments_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event_Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Likes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Likes_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8d4d87f3-10df-426b-9be6-4a16dd1899a7", "5b62377b-592c-4b80-8f18-d282a650c088" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4fcb33d1-7fbe-4a99-a08c-1ee3463b3876", "6d5537dc-f6df-4e4c-b0ec-901669ae6419" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "40283faa-b97d-4d31-b96f-43ce92ba0d2e", "8a8c0220-1b37-4abe-acce-a154600ef05c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "405a87ef-6d56-4b57-b716-9a4126715ea8", "6a951b6a-b439-478c-94d6-908c8b440ab0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6c690d5d-266c-4410-8aba-7dea3244546d", "74e42eb0-ad5e-4d30-90b8-e37018852eea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "13",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "97643a0e-f1d7-42a4-a51f-daa0b749a56d", "42bfc49e-f0c8-40d0-938b-35439cf68a4c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1c890f9e-9d1c-44cb-89c9-57baf22fc537", "f95f5d29-f4ad-41ca-8814-6c3f5c0e707e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "15",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7dc95f12-287b-41b6-9dc9-141da6e91cfb", "3368d13e-a4b1-4aca-b208-bf83efbc7d94" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e6bb694e-c6a2-416f-bd17-ba63aa725036", "b99ee3cf-fa40-4444-bebb-a9fb2941755c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "17",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "45768533-d767-411c-9220-dd687099c5fe", "092a9b7d-b282-4c7c-8622-dd2fe0fdc0fd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "18",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "50cd9af4-44ae-4e78-992c-346b5d32a90a", "3ec402d8-8e8e-4aac-abe6-f467ca3abd71" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "311c8405-5c10-4144-9908-fa11d44773f3", "df7f9b02-8bab-48fd-8fb6-3b0b06cffbb7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9570ccd3-275b-42c4-878e-37a5193611ec", "7efd4e13-0d31-4b84-b29e-d3d47fe7c334" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "71a3ac0c-1bdc-42ba-bf59-39982fc8fad3", "f72addb8-eb5f-4132-9372-8ede4f5cfd2e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ab150d4a-1c8e-4086-9c98-5608bd0a8090", "24870df3-390a-4fd9-a262-1900b7e820c0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "294ffccb-dbee-45a9-8c9f-07326ce9e71a", "fe1dc25b-0242-4a48-be4a-3731a9f1898e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "23",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "13ea7ac5-e1fc-429d-a843-f431432453a4", "ffdd3667-b32a-414e-8720-48b5aa855501" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e7cdfa25-faed-4618-88ba-9a2cce8f1ea3", "07afd576-04dc-4416-a67b-73e3a127cdb5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "25",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "65155b83-7d14-4eec-8578-33c7280a479e", "c8fa8b2b-26d2-486c-aa75-795cda863cb1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "26",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e6166acd-2a57-474d-8c62-7ef49899652d", "acb9058d-48b1-480d-a0f8-a9965d66ba7d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0de268f4-6768-4021-8e96-aeb2a4f080b2", "018102c4-9b70-4b44-8062-1418e30d9ce0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "28",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5fbd8b11-244c-4296-be57-5419749ec14e", "31e1841c-c7b4-431a-ab16-dd31b0f016a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e418f717-2632-4aff-b9ad-039be6795681", "f434bf59-5c69-42e3-a795-f6b7b441f934" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d1cdf24a-4092-44c5-90b6-9e15e7f26ae5", "83f0b16c-7357-4938-afa2-a2a8f5343676" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1316fa82-f923-4ad7-af8a-582472862679", "9ff113d0-6c12-4b40-a2f2-a1b0de381ee6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "31",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9cd78c53-4fe0-4cd7-a9ae-3658892107a8", "0bbd9870-3f87-44bf-adee-7b9d49050907" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9290e6df-4cce-4894-872e-cea1a4c2ca20", "25bb40d7-69fe-4f74-84c2-85f476b42325" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "33",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b189816f-9b26-49fc-a7f1-cf58e0ad39db", "9c686e97-89ef-448f-b6ab-0409ba725b3b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2f89fefb-be72-4fdb-a87a-c8c2214a98b4", "b383ec9e-e584-419f-be07-fc97606385a0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "500c2058-fdd6-4843-9914-5b7c95e324e2", "dcebcdcb-4a45-43ad-8700-5c0ff1cee9e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "36",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "79fe5fc6-8101-4b9a-8660-edf5fd6df821", "49123f55-0938-415e-b174-240b6e7e4b5c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2e8b97a1-45ea-4b02-b938-e745f41ba40a", "2a13f1cb-4838-4f2b-845c-c44ba03db986" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "57b3b082-9131-4855-af70-21ec9c9a77c3", "9d898a57-e19e-4f56-8ad9-1875ca358510" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "39",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "efddd9eb-30af-4025-a9d5-23e074280d33", "7ef16a5e-4f7a-4d77-8b3d-aab96894230d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "63f0a863-bfa8-4297-ad7b-38a90e8cb1df", "cf8b0ce6-967b-47a4-978b-b53907ebd405" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "40",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "51812011-263d-45b0-90f6-b70f2c083cc4", "c4b2caea-3226-4b30-bb4f-d1a1c75d623e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e8e86cec-aa4a-443f-b506-4e25071aa8ba", "4aae8259-8eb4-4163-8c92-d46ba3925eb3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "31bef7f5-22db-4556-8f67-cd027a4b30bd", "33e18ee5-791b-4b24-b5c0-ee120bab94d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "43",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "dd9a7c43-e85e-4f00-a010-d936ad3e3f3c", "afab3fa8-77d9-44b3-a747-66500dec4ae3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0eb95123-c3fa-45d4-8a61-aad31c0985c3", "6e0780ac-a799-4f5c-84e0-e919823ad846" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9db4b49a-a29c-4fdc-b7c3-add2ced4b798", "50fd3bce-d094-439f-ace0-273989193a5a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "46",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4ff1bdc5-ad3a-415a-9790-ec785793eb61", "9e816e18-1b0a-4dd1-94f9-4732741bac80" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "47",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5f170001-417a-40fe-9fbf-bd4f32511100", "9eeeefbb-964d-41cb-b74e-065a464f7910" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f4e5d5b5-0175-40d4-91f0-0cf661f5800c", "2ed256f7-a923-4ac2-8fb2-bc344971ad6f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "49",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "13d012db-b601-4dd8-86ca-770e8ff6cd61", "9f77ad7e-2d3b-4513-97db-4bf73c39c70c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "619582ac-88c4-4ac1-ab02-006f2ba474bd", "e1bf91db-b32e-4079-ae67-68ba85b345db" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "50",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2e8568f4-3fde-4263-884b-9659683e3318", "0a4c5f54-2605-4fa5-a4f9-9453bd2b68b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2d5d5073-1025-4ae6-8086-dbd374247f09", "7b694323-7177-40a1-b541-65ab479c32bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a0770a53-4518-40d4-89b2-ce8289284942", "2910458f-d7a4-4073-a43f-a5be4cc6d4fa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "53",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0b2c95f5-c4db-4186-a25b-884d82433bf2", "a5f2d04f-15cf-4030-a4cb-4e8d46197a8a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6b9ece7f-28d8-4a56-bb38-f40ef39653cd", "f117123a-5840-44a1-ac99-4789a1c73b52" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "55",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c83dd063-a1b7-4d2e-b47c-120f807ddee3", "e48ce06e-e978-4b7b-8a3c-4783a8cc56ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "41a084d9-410a-4141-8620-6aaed1d4a14c", "ee242579-0108-417a-ad09-b898aa9f3b4d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "21a8649d-26bd-4949-86c6-a049d451bd38", "6fd34f8d-7f03-43ae-9067-e8d71474353b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "58",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8a36462e-827e-4d08-b828-c73641a0a3ba", "86759b6c-c35c-4ce1-8ea7-8304d27eb65a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "07c4fe76-625a-4fee-aa6d-74e7da3ce6a6", "b021fc2f-37a1-46e8-8752-0ce5eeb0d8e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c6527a54-f5bf-4ed1-ba99-534f3f61a1bc", "df245b64-4567-498f-8b0b-76613093f40a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "547ad1d9-fa32-4a93-95d1-292d72cd7371", "6b7fde2c-92ce-4fdd-b4b3-404e35c856f2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "dca33377-ac4e-4daa-9b6f-443abdc89559", "29671d1e-b39a-4b39-bd63-f5362dd8f1a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cd0b9ed8-2d2c-4075-a8ea-afb97b6ef51d", "6c3b060e-8319-477a-bd65-2bb7ddf82dad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "63",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "69cb35be-66db-472b-aa03-8c405f944f9e", "23e6156d-269b-4d9e-a688-6500df61adcc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fd0019bf-9451-4258-8e2c-dceb21b60540", "4ab82086-1d74-4f8c-abb6-4b3e81b14022" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "65",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9a7db34d-b70b-4e00-bbe4-8c59398f0fb0", "85ef9970-db1f-444b-a8ad-c30ee36364f2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "39ea46d0-0a80-4a66-8538-95049c44912e", "c193a717-a32a-4223-941b-df9709401540" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "13527a27-ef87-4389-9eb3-48b94f4cb19c", "feec9cc1-6a0c-4c93-9856-8eacd8a5eeac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "68",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8dd6e8df-20cf-4592-9937-c7db13da9a68", "f88009ad-2e3a-4b93-b873-ffc61d429be2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "739ea5c9-c470-45c9-a024-38ac0d78e130", "5276b038-424c-41bb-b658-265bf1c2ce47" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8f6dea1b-e485-465b-9404-45570a7ca44e", "fdf3b667-1784-4a24-9929-0879597a898b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "70",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6b5047db-37fb-49b7-a287-49cad861c879", "5d173d80-688a-4991-b717-b17a36cbbb2c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ca6b4299-b3e0-436c-a8eb-2469ec8fa5af", "cfa35922-cd1c-4ae9-9be3-8e1a5b6a741e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b2a95d51-dd4b-4d30-8cd4-ba2cf6d9a048", "ee513e2c-b830-4e17-aaaf-a5e3688d8cb2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "73",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "76e3ba27-e849-45a9-a5a7-7df5026a27d4", "831f572f-cb7d-4409-b7e4-5396d7935cf9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6b853a83-5a42-4617-b2b0-c8aeb0ab19a2", "01f14baf-4fd6-4b4a-8ac2-1e76ecb7276a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bc3a7b0b-057b-4afc-92da-dfc106bdc22d", "c9166a71-75d6-4273-b629-621fbc555bc3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1deca600-9bc1-4f89-85ca-7f9a8dafb9ea", "c553fb6a-e99d-4221-bc27-fde80d30f5bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "77",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9243124e-a242-44f3-984b-2a5815293153", "a29cff86-dcf3-4ad2-af8e-5cad57366ae9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a44c46f1-5822-40bb-8645-ecd35cf33769", "8640576a-0a58-4ef9-b040-48be598963a0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "befbfd80-a086-48db-b3d6-5950dd34b9e9", "46aec2ca-a932-415f-8a59-3578ea82afa0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "826e67eb-b0e0-4ae0-a2e0-89e60ee3642e", "3040b493-c198-4632-a854-c9d42ca25dfb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "76be8f47-32f7-4a03-919d-340209d43d47", "33d17719-c903-479d-b34b-9875238e6ad6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "81",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ca2807da-58ed-4fb1-9e79-9099662d5bcc", "44779b88-4adf-4fc2-ac3b-d6f8d7290493" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b4577ef3-0223-4f0d-8a2a-b3ac9ce413f5", "dae129ef-54ca-41d4-b614-265b953e2438" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "69475996-2a86-4d3e-a0db-0f622ee221b9", "8eef58b5-7014-4578-a628-f20a8db5c58f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "84",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f07d9841-b43a-4620-b488-f79cbc2a968d", "0172e3e6-b237-4c24-92ea-4376e23b3a5f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "193af0c1-a85f-4f98-bbd2-89f875d46b96", "e5656560-e784-47e1-812e-c0bc9f80dc3e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "508d882f-e703-4202-8f03-effced0ddc53", "cd582f56-b484-4dbc-9ba0-13de0922a0ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "44956f7e-9e8d-4548-8649-7f736dc33ce8", "29eaf5dc-c611-4598-87f6-e45315b772fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "800c9fa0-40d0-4d54-b173-46caa481f223", "c09259dd-3afa-4c79-8247-d93bd446494d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ae9e708a-a2c9-4270-86e4-c48d66e51eb7", "12c83d3d-d43b-401b-b54a-77c76a4781f5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0ac09316-d68a-49cf-87c9-97b0369d6e47", "e69313a7-4cc0-4cce-8945-5e378d1150e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9c27b193-1764-4721-b066-d73bb803c719", "cf1b2d24-f048-43da-854a-c07ef2ef0ea1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ed1c159c-5200-443b-8e3e-e574c28597c4", "9ccd93a4-d11d-45d1-8668-e20fb3777552" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "92",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c4268eb7-f49c-440f-9250-23e3459bef63", "d47001db-7438-4352-a838-df5294577907" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8d41d386-e350-4b30-b480-6ce8e0d62f80", "741d648e-8f86-4f69-886f-98b4481f59ea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6af95c97-db0e-4829-ae7e-9eba2144496c", "1e1f7c27-f61d-4616-8a31-7551c9253f3b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "95",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "541ab80c-5ddb-495f-8f0f-b4e38c4f77fb", "d71c5779-ccf9-4b67-93d7-effffb0c0acd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2dcddb2f-8d5a-4d06-b890-636c60790c55", "f72e1e00-780c-42ab-beaa-e5a8c2e7f7fb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0e484a84-9e61-46ec-a49a-36bc1cdf8653", "1a29116a-f13d-4702-ac8a-a5f559d4b302" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b5ba07ae-e332-4f2a-bbf9-0487e7d53f33", "650752d3-7185-4912-b243-952271508639" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5e5741fa-49d4-4c4f-9ab4-0baf2e7d04b4", "ae0dc5a2-abcc-42ba-b0b1-fb1b7aaa7e91" });

            migrationBuilder.CreateIndex(
                name: "IX_Event_Bookmarks_EventId",
                table: "Event_Bookmarks",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Bookmarks_UserId",
                table: "Event_Bookmarks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Comments_EventId",
                table: "Event_Comments",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Comments_UserId",
                table: "Event_Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Likes_EventId",
                table: "Event_Likes",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Likes_UserId",
                table: "Event_Likes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_CreatorId",
                table: "Events",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropTable(
                name: "Event_Bookmarks");

            migrationBuilder.DropTable(
                name: "Event_Comments");

            migrationBuilder.DropTable(
                name: "Event_Likes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "675be8b8-779a-41a2-9835-3da006e4b5ca", "3fa1e29a-6863-4fd3-82cb-b8d35e4f92b9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a5d1af28-ceac-4cdc-81b6-8cdf386c5086", "b392dbe1-9b56-4b4b-9fdf-824d117b8089" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ea1b9100-9173-4b77-823d-df5709f85328", "73bb91e7-c274-4383-bd3c-1fc879427389" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d912fb8f-4b29-44a4-b9fc-ae5cca55ce20", "34d727ed-2f11-44b6-9b27-662b32c94eb0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b7479444-4636-42a7-a9e3-7c100ba8acca", "6793eed1-b2cc-4f74-8f43-04d5bef582c5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "13",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "931efa51-b81c-4a40-b729-559f668173d3", "4053ee65-2cae-4632-94a9-40bdc411e019" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ba8628c5-5eeb-42a0-878d-34e57063f724", "02500c29-455e-446c-b3b7-55d1fd4fb07f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "15",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b01ea342-2d69-4f94-8417-91da620ce9dd", "c42ce762-300f-4edd-8ccd-0c7febd15909" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "02d11d8d-dc13-4286-aea9-02b067d35ba8", "1df50371-2a83-41ac-aeda-9cde35606268" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "17",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4a5b6248-580f-41e8-a268-5f9e323b24da", "d3da2714-dd83-4691-b298-6fcafc8bb79a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "18",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "69d434a0-7d8d-48ae-a7d0-ffce9e634aa7", "af20d3da-fc63-4aac-a523-e2c4f1c06789" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8e7a9b13-1e47-4e66-94ce-9dcc7d477920", "46c584f0-8d88-411d-8392-54dc6b231dc2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f4b57f51-9925-4cc6-87c2-c9b4790bf25b", "48744a02-5d62-498f-97fb-f9ffffe820be" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bdeca71f-5d04-418a-85f5-c14c47125614", "00633a4a-e0d5-413f-a57a-b82df9c779a0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b135abc8-e454-4dbf-bc3d-52030f3568c8", "3b99cf74-b481-4499-9290-3c94e2312a5a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8e7bf77f-0595-4148-a977-73cfdd439bae", "8d4469d8-0117-40e0-8e56-3aaa48d5fbc5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "23",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "84f1a90d-561e-4093-9f78-e0888c654a7d", "62267587-e0a0-44af-9f2e-19f1491ecca6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "47ebc8bc-13e7-4c3c-a4b1-4c4d0b29a6d9", "304b064e-4525-4313-aeec-0df8b3650d3e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "25",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f11062a9-ea2c-4a4f-976e-402f537d733f", "dd0cf0fe-250c-4e92-b91c-c1d5e35c60d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "26",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "82a82432-e08c-4d76-842d-8fdb1d0c56db", "c10bc1f3-eb36-475d-970f-6ccbdc770f61" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "22dc873b-d0b4-4183-8511-b204d6173dc0", "f288b680-d5df-45d0-a65d-fc7d77c3f506" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "28",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5ba28def-35be-4d41-a51a-c9b3932ee467", "1e9b1b7a-239c-4079-b462-74388fe31022" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "15d5b701-2893-48c4-b240-5ca9abb293c7", "3789f94c-cdc5-4c8e-965d-be8d97255b76" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "384da396-68db-4240-80d7-73a740a91904", "646320a5-d718-4d68-8b8b-2154591e4f9c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e9210e4a-2082-4675-adc5-280cc0573ce0", "580f8c39-3fac-47b9-b98e-61914d1de001" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "31",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "70addb58-af42-4609-9d78-989bbf52a121", "44bf4815-65cd-4822-9521-aa4145a66403" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b14d82a6-a555-48c1-9471-029f4fb9393a", "a7674ea2-a459-442e-88f2-5f8e2c1197f9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "33",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4872a3ca-d4ef-4be8-b273-deb862bc34f0", "3845b6d0-edd5-4969-a3ea-681ef5e0c47f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "448e521f-8e37-42f5-945e-6a5fd8180a26", "f8a4e51c-c6f6-40d9-af31-3a16a3a4b5d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e41e0192-b4e8-4ff8-be88-5d48f75b1d3c", "cdb50b40-8a44-4fe8-b132-978f4fd375b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "36",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f6c052f7-50bd-451a-9fab-3f30e7f0e276", "c93f3fe6-73fb-421a-a0b5-d0dfe0f3e38b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6025d3cc-26d3-454d-a2bd-e20b0b15521a", "3231dfbd-ec9b-43de-b005-649b9ca93e2e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "49f1033b-437b-498f-b550-454fa21fadcc", "66a0b92c-9efe-4bba-9c50-f7cbfe2a1999" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "39",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0ba9f9c5-1a89-41aa-9e33-3261dd5dc89e", "869ddcf1-46bd-412b-9606-110d14d49c1d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "35dfd496-7f24-4d5a-b51b-196dd5b00337", "ea29e1ec-420c-487d-b764-cc935e808229" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "40",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c1308225-4313-4bd2-8d9a-702151bee938", "1303ae11-80cc-4f1b-991c-faa73cb71c96" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8dc747dc-b8e1-42c9-8a26-c4c646d071f1", "d222eb0d-d59a-492a-b82c-7810b9fb5a74" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bae9fcdf-7b73-494d-82ec-892bf7a6e5eb", "99eb51de-d6a7-4802-9183-26970067196e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "43",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a2f05c9d-605f-4ff6-becd-e38cac6916c6", "99133fd3-4062-4187-96ee-3683755d063a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "490c3a23-c750-410a-b5ca-a582e9b443ac", "5c2db534-64c1-4b77-9085-7e14c989c8b9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3d42f0af-1eba-495e-9af1-4cd0c6b89f2e", "b67952e5-ec26-440e-b7c6-8c1b191c5330" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "46",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4cbe78ee-b80d-4a21-b712-a6cac50a9ff1", "9c23a50e-e938-4e7c-9065-bf358e44c0d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "47",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ea1e4127-68ba-4721-8742-51039e2266fd", "ca84aa57-44a8-439e-ba5d-9e905fd426ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "00635264-a087-4267-b8ed-238494f1067a", "52f9d674-113e-4c08-87d8-f012b8671228" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "49",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0fdf3cf8-4e8a-43cd-8ff9-e1ad3975e909", "3190346c-c4ff-4b38-bcd9-e07e3248e13b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "58200070-bbf9-4d3f-a043-8b86bb8be68c", "29997d84-86d3-4274-bce5-d0b758d6bc64" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "50",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1f5aa9d0-c923-4414-b048-dd087e5f63a9", "875fe690-356a-4e4b-8875-4f0bd7e87c90" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7d53c9d0-832b-4d43-bb63-a10315dede4b", "caa8fba5-afde-40a6-ac10-4b959c5dd2e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cb0225b5-6524-459b-8431-6a0dfc42d3d0", "ae1f96d2-dd1b-43f7-8659-f0fdd7e78404" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "53",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "96f72033-99f8-4dc1-8912-e2c133ce2784", "81fe58fa-c649-4092-8cb2-4569f447aff1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b13c7466-cba3-4e47-98b0-3f15866eddd3", "a8fe3d0c-8eb3-408b-8118-d8d2a48727b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "55",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "506c6a63-e6ea-4154-a985-61e541c491ac", "c3043490-4fe5-4a78-b12a-3c1003904909" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c547f594-53ae-41c6-835a-a1430c6f4af1", "7c89f7b9-35ae-481d-88dd-c4d022bede17" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d01ca9a3-895e-46ae-8e88-6b41ca9ca80b", "17261a3e-b26a-434b-aa2f-0231147c5112" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "58",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "65c19cfd-d129-4ffd-86e0-5f8de96ba308", "86c2bc38-792e-47b0-8a07-fcf9ff6eb567" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "12ec9431-ca1a-45fb-a244-d8f3baca1a54", "0aab8401-d795-4f30-97fd-58f20b88ba98" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1e324895-96d1-4e8e-94c3-93dab380aff2", "519f922d-39d1-4d8f-b34b-50cbed1c5895" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f2594ec5-1a61-43d2-9cd0-92f294ad1499", "1f5001ad-88be-4731-be91-38dcdfc0b71c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a30e7e6f-b998-449e-9919-b072c977e3b8", "e6c9e282-7680-4593-ba9b-3c3802093310" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a6726c95-07ac-4c1b-b0ad-334ccd0966d0", "0637bf8a-d9d3-47e3-b59c-b8855df05df5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "63",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d99ce9c6-d2fb-4079-bad2-84adaa945484", "75a39bea-9aaf-4a9d-8027-8ff0f9429de5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "be361c7a-6a88-4b10-82f6-d7f41d578598", "70c34ef2-3817-41f6-b961-71d6a6efe8ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "65",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b7c9ebce-c3c1-49b1-b17c-ace7064ac5f1", "62b20fe0-cb15-47ba-8521-76f8b70d66e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d0790a7a-fce0-465a-aa79-c73fe44e7be7", "dc4a8081-10e9-4023-b195-b3f495e508f2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "848f228d-57e0-4374-af55-76ef9faea399", "b8ec0fd4-76bd-44c2-87c3-27c7f5bd04d3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "68",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5123974e-471f-4ae2-9657-aaa8f77145c6", "97683b25-6297-4ba3-87ad-e6932f385c26" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6e64d3dd-f07e-4b8a-acb8-a36e343db94a", "6579b2e2-4d55-4efa-92aa-696ce7d8a780" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e808d4df-4dff-491c-be2c-b8f94eed7487", "27ae8871-3b36-4b95-bbf0-a78171d2342d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "70",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "511d8612-90cf-4e88-b84f-22c00090bd97", "3d78cd60-e71a-4370-894a-fc955f48b020" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "19ac82d2-9d02-4903-bd42-4ec8eeec8881", "6f97ab30-dcb5-40f8-b157-6b69123418dc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cffa6184-4ce0-48d1-9034-c5a6187ff14d", "20906bec-cd31-47d2-a761-c7b466bb5c6c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "73",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c492e5ec-ec6f-4d17-a938-4e281704971d", "ad09f215-66ff-481e-b52f-7208bbb470d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e395fc62-b776-4a5d-af34-0284c1b400fe", "be9b94fe-7a42-4f56-8193-ccf5f0cb8140" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1e6de260-fb26-49c5-b7e8-201e50a9ccc8", "285e17ee-91f0-4ec2-8819-9c5c35731b6b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2ae5e6ac-06ea-4d5b-ba0a-3878d7eb5e80", "30154723-da85-4401-af12-a054f61dd1ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "77",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ba5f6f39-acea-4815-9b4f-6e4f8012ec62", "2758119f-69e4-4e31-a925-f2b20edac3ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f2d22d20-c72e-465b-bd48-79b2dd0da8b0", "d727e434-cc0a-416c-95ba-2703716421c9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "aa3e466b-91f9-438d-ae3a-12a37625f963", "61e1271d-56a3-4755-b6bd-43e54ca3bc4b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "42e65eb4-27f5-4866-b0e2-0e4e76667d2a", "b5abcab2-e0c0-48f4-90c3-839e23c49f44" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "df879ddc-2da4-4b11-8042-59f57730d492", "64f05d37-704d-4ebe-98d5-d5c2ea337a42" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "81",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1146613e-ed55-4e37-931f-0b1101a92a8a", "777f083b-d4ea-41a9-a0c1-b3d705cb2618" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a9d3074c-7315-4b94-a8c8-46ec8e5341c7", "8a3a6df5-7cfe-4ee4-b4e9-72f9a954ba41" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "16917833-35f1-44c7-b76e-2fd7f3b22e31", "60bc69c7-5283-4e16-b79f-6c0fadcc6030" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "84",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4905b4cb-adca-4888-98d5-3a42072679d3", "44252e18-d69f-4656-a9b7-4cba8cbb8ed5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "59451246-45e1-481e-953a-5f6f7220287c", "917664d6-deac-496a-b2c3-8bf95e3860f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "884ceeb1-9e04-4a08-88db-7c3cff6e1671", "6cdbea9b-59bc-4582-b3df-cbc90c6ff53a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8fac2902-0703-4dd0-abf8-5ee86ffd537b", "84b8997f-1f20-4c8e-82db-1f7f3b6bc850" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "50035991-a499-413c-b412-88d4bf79d0c9", "512e1b47-227d-49a8-b2ba-577870c11907" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1608b8c4-a391-4c2b-b48b-87a7cd74d7a6", "95e00832-b19e-43f6-80d0-6eb691684fca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c5a983d7-36ea-4960-a936-4aa17c258652", "55655e5d-8f94-4e0e-baad-c6f309da8743" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "49107a6a-37fb-4c25-8e95-6e2c601454fe", "69b0bea7-e5d3-4f41-830a-bfb34e1758b7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7c982a89-3637-42bb-a6e8-78ea6787c4d9", "ff6fa6c2-66fd-415e-9af3-195f02fcca1e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "92",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9f1f567d-ee46-497a-85fa-8158209d8e18", "6d94eb22-1cb7-4af9-917f-bacae1227afe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e3eb8457-c349-46e2-86b2-f3bf5da8f293", "9e6154ad-17f1-40fd-927d-ec64aba904eb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cbb4948c-ac57-4bf6-9f24-41b457faf598", "712062e0-3e5b-4674-87ae-829f999e2b7e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "95",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "432995cd-6227-403f-af86-cd9517633bf6", "2602995c-d759-4473-8e25-2783bf8e8e42" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "49081173-84e1-4bc5-8feb-5a53e98a870d", "3d13f9a0-5366-4533-82d1-c67e3e7f661b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5f04ecc6-29fb-4e6b-b841-1115d6d884d0", "149b3880-0f54-4756-a0a4-82256a63f64e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "30337fe7-8cc6-4a6d-abba-16682a2cb956", "0bce0706-bf69-4265-8e0e-8e0e6a463771" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e3179e13-301a-4b85-bb14-64f6b07b7820", "d23edd42-06bf-4b99-ae1f-625de6364fcf" });

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
    }
}
