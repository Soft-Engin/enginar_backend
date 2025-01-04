using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update_cascading_rules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Roles_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Addresses_AddressId",
                table: "Events");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f074fd53-dd53-4f6a-b8ae-9b62ff1c3836", "2d9b0303-6a4a-4e4f-9e67-5864f728184e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c5df4115-388c-4752-bd79-a88157204f1b", "cde12b12-1d2b-4e17-ad9f-f3b4c87dbfb0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "82b382d2-1c8b-4672-8603-76b276c94416", "eea163bd-1bd0-47e4-a066-1c132e011bfe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "74c9bc65-66ad-4b9e-8f06-10c2669c6189", "55c849a0-12d1-4f20-a8ab-51275649fcaf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "20bafec8-df43-49b8-9650-3c6b995a881e", "f838585e-26f9-4e0a-aab1-8bafa26e16b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "13",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d057cd74-3da1-4d82-8509-a6e7055df942", "22e4ec52-4443-4566-99e9-c48804512168" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1bba4196-2db6-43d6-b107-46a32c6f5213", "433766e6-29d1-427e-b19e-52b4a8bd6725" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "15",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a15ba4f7-fd59-4e73-b17e-7107bc67b885", "d6033de1-81c5-420f-81b4-166d2d8736d9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "76005f48-5a84-4b45-930f-445216aa4863", "5fb78205-0485-4b2d-9076-2e5743daa711" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "17",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "62cd839a-c825-4bd5-acd6-a9250486aa98", "97c6c3b4-7815-46be-8fba-142dde727abc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "18",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "60e4ac7f-af24-4300-bf9e-0dd6a132f20f", "cb8d806f-4095-4f84-845e-ac636da9787e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ee0cb3e7-b4dc-4b7d-ab9c-41bf7857e75c", "af18e536-a60d-4ce7-becd-e92aefa9f7e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5c0ae86a-fed3-473f-bd45-265f2f7f2e5b", "cead6430-1496-4ce6-89d1-f4a4bb90e417" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f246a01a-360d-4ec4-be79-d65b553a6206", "e21e7bdc-7186-4f26-826e-60c5e8d25c2a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2f5d54f4-7f48-4557-ab4b-0293bb023d91", "2fcd1178-f454-41c3-aabb-d884e36a58b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5a43a871-4664-48fa-8265-dd1ebeba7d76", "ab5c8559-9380-4a65-ac9b-c999f5ff1887" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "23",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "86c7099e-8064-4bf4-9852-5fd89b9b1735", "439a8ccb-d9c5-4be0-976f-c2efff016a1b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e8dfb50b-1680-464c-8117-3afcbfb853e4", "2dbe7fe1-d579-431b-ae7d-7345d2f7735a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "25",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "18274c73-01b9-4bca-aa2f-b7e78d7a097b", "4329fe11-7d5e-4610-b7de-6d6632894e57" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "26",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "651f622f-f51f-4079-ad97-435c772f180e", "c1c6f705-4b6b-4551-80cd-69a70c0b6be6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5e63332a-8631-449e-90a9-5c6af2887911", "dda88781-7709-4451-ab3d-8e83388a71e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "28",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "22106147-5c13-4f46-868c-5f50204bcb14", "cecd08b1-41a6-441b-8bc2-2670753aa3e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d4cd8c64-6303-425c-8dd0-0ac55f2aa498", "5cd5aaa1-a58c-407b-aa4f-b8b63fa17a99" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "94037da1-6b6f-474e-b393-fcfde396c9af", "35cecd50-e1df-4844-98bb-17f38b1e5f44" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6a954cfe-c89b-4f43-a987-a9676300ce54", "73e865b1-d938-41b8-bb8f-2dde69260bf3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "31",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "64f30cb0-fe24-426d-81bb-2093433fc6b3", "aac1093b-3d33-4425-8873-9ea53a43328a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d631076f-c254-4097-8bb7-64fb3fd2ff5a", "69b0a2b4-34c4-46b4-925c-58cc65474f26" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "33",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "190065fb-8256-43b3-968a-f9a06115f94a", "50019bbc-e6bf-426e-992f-1e6511e8dd76" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6f378810-6d2f-42fa-b29f-f6fc70de7fa4", "bb1ae0e4-b62a-4f84-9cbb-f431de0197c4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5ec8dbe2-311a-4655-b71d-afa5d65e9770", "eb47f485-4751-4d6a-a32d-4dd567a65069" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "36",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "37dee27c-51d9-42fa-8adb-6d3f9415852b", "9e33f39e-a1c5-4b53-b9cd-799e56845b0f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0e79a79b-c2be-4c08-9505-766901ac637d", "aa50a569-f71a-43c0-aef2-bf38a2b22c8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "683151be-77d8-4ac0-a3be-89f33a6a4c14", "61f33dee-7fdb-4b7f-a736-c94c6aad4888" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "39",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c42be2cb-7bd5-4ee1-b0eb-58e8cb46ea1b", "c07d2702-6f74-490f-a3ab-8542c3a6cfda" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4b8342d8-bcb3-4109-b704-68797d14cbed", "4948b533-a6b7-4b86-bf18-17273becea09" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "40",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "aaedfc7e-5e69-40d8-875f-e74fb6f1cd2d", "9c3c9b7b-8d63-4512-b301-f656f4a32924" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "926b6e4d-0ba8-466f-b277-0e0ec04fa646", "15e4bb31-498f-4720-8c92-1854f957b7ce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "160fda1d-b668-4346-a7ec-bd0c10ceda90", "70992b1a-2da1-4807-839b-2588544c65b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "43",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "35ac928c-d018-4a3d-9b35-c58d0462c072", "247835e9-f55e-428e-832b-1df68948ba55" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b788b940-08d7-4377-84b7-0abfacfb8456", "b53f7188-edb5-4fa7-8d2d-eb01ecd1d993" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a9d8345a-da12-420b-842f-7d26aa277bdb", "f0c6a7dd-ff64-4ebe-815a-a1ab8cdf6152" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "46",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8363de7f-cf8f-4e82-a80b-b3d56400722a", "d9de7a21-0621-44b1-abb6-fdabecf0efd4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "47",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5bf462d3-2b34-457d-8a28-be0af63b4658", "6b76c23c-d874-472a-a4ba-02b11f458cc5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "61a6d8ae-be8a-4d62-8e47-b0c18fc99c2a", "c3c432dc-f49a-4776-9f52-b2d08425aadc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "49",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "03a4d90f-fc70-455a-9ad0-2f9d8389b954", "2e808b19-a1d5-4322-8163-cfea5d1b7bdd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "66ced502-e74f-4428-ab07-a92f3351ba51", "948f86a6-bf11-447d-83a3-1177b22d194a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "50",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "57643d02-0621-42d8-a5d1-25af48df7fe1", "c3638f1c-882a-4112-a115-5bdf3046e25f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "76028e5f-c563-43d7-bf9f-0b4db4a01567", "ac231ecc-194a-4c61-acd3-a76682a0a76c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "848f881c-0ada-4dce-b28c-553f895ba0a2", "7cd5e19d-4717-4707-b0b1-0d6d174bf99e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "53",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8204f954-f303-42c1-bc28-298c49bde985", "98408526-bae1-4e9d-959e-4a940b2f61ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3c54211b-2dfa-4371-9883-d4887fad4480", "e8d2598d-05fc-4dab-b340-044b7fc22099" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "55",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "06c3f1b1-7225-4074-bac0-c853ab39ecbc", "47900998-615f-4871-a279-4e8247f69d4a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6df06438-ff18-4a1f-b0d9-95631df100f5", "81b0e94d-0771-480c-a83b-73764b95b543" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5227b142-5f2e-4b0e-a432-5e19a0af426e", "32361a2f-d9f7-4f23-a25e-fea8b88d88e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "58",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c5680446-1b03-460d-a5ec-80002062b8c0", "ce2188bd-6378-4bf8-b42a-da5049d7d4c1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "51035eaf-f9e3-4dd9-aa24-188902253c04", "6885f9ab-2336-4a4c-a37e-3c558986ee36" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8f4aaf61-d6d1-436f-8dca-fa4d98f40681", "d10f0a0e-8870-4d47-9f02-6744c6f83ca7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b3e1af6c-0d3a-405f-9e88-2a0b74c50f5c", "9472acc8-8551-4daf-a6a0-68c6695ad372" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "363683a8-d355-4ea0-a819-e93690e7d710", "f74e7901-ab6f-45b6-bd9d-8df3a1f3baa2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7e435d34-1d75-4f7e-9ef2-0008a78579cf", "95141071-51cb-4fbe-ba73-b2dec0671947" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "63",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2a5693d1-acd1-4ce2-abc4-bfdc8e3efecf", "6fe0fa01-9017-4745-928b-907f2d16d937" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b9ff7732-770e-4aa9-bbd3-976d963c96dc", "4b4503d9-df5e-44b9-9354-0cfffeda29b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "65",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8c3f791f-39cd-47fc-b699-20ea561cbbcb", "04653f96-aac3-4028-924c-79ec922afcd2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1148da7c-3b2e-48a3-baf8-5f8c3656c2ee", "3ca495ba-4450-4c50-b56f-3edb674c45d9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "17c7c669-d836-4ef3-a6f4-2698f5350aee", "5e285e55-62fb-460e-af79-170322c91f4f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "68",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "55053acf-ec9d-4561-b69d-c94b5b6a54a3", "a190ceeb-8c84-441a-9986-7f29156f1193" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8569a2dc-8840-4647-9b39-8ed24fe878c1", "ef75d010-02a6-4e65-bc31-4d2e81888e1a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5f3d0962-5ba4-4bca-abda-108ace9a2cd9", "8449ffc1-8dc8-47e4-b9b0-947f53da2ba8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "70",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ead546b5-5876-4e36-9b13-a8c091202d7c", "d32fad88-e68b-4793-ad24-4469a60d8439" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "521b6d14-6c9b-4a3f-b850-e64848c65685", "362cbc58-02ca-42d0-acb1-01270c7d7639" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "00ff00aa-ffdc-4c23-a3a2-4d93f39ee45a", "0c49ce89-2f0c-4c63-a469-f149148f1d32" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "73",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0f099a17-969e-4516-a062-689e2c888d1a", "5aba09ff-727b-4292-926c-4d170bf2e820" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "db0e02d5-bf6e-4576-8937-65fd0d936960", "e9ba1877-b3d8-454d-8857-8f0e964b4030" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "83e7e8fe-4f16-4e1b-b9d6-9715a8745dc4", "a602e704-f868-4677-8d11-acde22f84ab1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6aeabc2b-cdaf-43dd-bb93-4a92ab185d86", "b18b952d-c2ca-496b-957d-fd89baad7394" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "77",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "afd33efb-1381-40a5-a58a-bed007d8b184", "96de704e-168d-400a-a673-bfebb356c7cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "33721641-074d-459d-8db6-f3417779acde", "c7e9dab5-fad7-4c91-915a-9e14dba46103" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0345e068-0e50-43e5-9626-27ba8e2a48ba", "96199557-1b4d-4d8b-a925-36dfa702571b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fe9c82b6-3915-4f50-89c8-240e727bf8d8", "d45afd90-abfb-4073-9cca-9dbb6fea599f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8fc660ff-d495-4005-afd4-b464772207f2", "c4297237-f045-4a35-8159-c61fbcd7becc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "81",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "78f5efc8-b87e-487f-8800-bf2a6679054b", "94fa7d81-dc37-4e47-96e5-39c0a19b67ac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bcb6702d-b73d-4968-90f9-fc096989d184", "b5160266-9cf4-4220-93bb-29b4e9eee289" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a044a143-9dfe-48ce-a8ec-4bd17d6de551", "d43772e4-5caa-4893-bba5-26713ff0ccd4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "84",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a8f1185e-60f6-4c45-a82b-9adbbdfddb1a", "070e77e1-3dc9-4d39-963f-d955bbd5c846" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "23b66a91-f39f-4a4c-a486-996bf00f6609", "c4cd117a-b799-4ae6-988f-7e64bc2558e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a4dfb558-de22-43ff-9ce9-31f9934b578f", "65fa940c-94bd-482f-b54b-28037bbf5bac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4dd9b270-ca10-46d2-9ebd-cb0059c4f5e2", "5420ea2b-a44b-474a-a67e-d950f14319c8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b4335efc-1b41-4162-b444-0e7f6d53de7a", "119e4756-dd58-4b18-bfdf-880fcfa2f3bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9219347a-2d2e-457f-8cda-bf5fdc7857b9", "ef4eeb26-e8fc-441f-92ce-d13cb8ece90b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3413e109-2c19-4c8d-8cf1-517ff4c4240a", "14520dcc-3179-4d78-a40e-893b0a965f84" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b37fdc7c-b460-454c-b3aa-84a75a9c7bf4", "c892ffd5-d922-437c-98a3-106f9a880944" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0cda0c96-aaf2-4e56-8d5d-757a446210aa", "58fb7f1e-3607-497e-9dc1-e912f2fd8477" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "92",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b6e73b40-0829-44a4-8776-ea0cf1e34391", "425bd038-ac0f-4aa8-adc6-25940047758c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6d075eb7-8559-4d19-a0ca-3fe8de57445c", "34ccf8f7-37f2-4693-8bee-3c84f0a09735" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "323ef2fd-95da-44a4-b650-a199300c54b4", "0b06826c-9ef8-44d3-a538-caa184f878c5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "95",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "21c12dfb-69a9-41c3-bd6f-ea458a9721e0", "fa487b2a-c1f3-4051-a0a5-7b27d47649b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2bc635f0-a8f6-4538-be14-6ec1013edae0", "7f5b74fb-37ab-4fc8-9577-2a2c5786f2eb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ce0324ae-c51b-461e-868e-a13e78a28b47", "1b48507a-2c71-4b4e-9256-29073f556a77" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "76541585-6daf-419f-82bd-a399e194a1cb", "3ece4406-c238-4884-8094-90eb1e0a6ffb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "68b4897c-c454-4657-b925-4b6b29674650", "558dbbea-d383-4a44-9abd-420a414ba935" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Roles_RoleId",
                table: "AspNetUsers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Addresses_AddressId",
                table: "Events",
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

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Roles_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Addresses_AddressId",
                table: "Events");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Roles_RoleId",
                table: "AspNetUsers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Addresses_AddressId",
                table: "Events",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
