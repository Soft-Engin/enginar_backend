using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class remove_deprecated_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users_Blogs_Interactions");

            migrationBuilder.DropTable(
                name: "Users_Recipes_Interactions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fab65a31-7d72-4bd6-9750-8497643d170b", "5042588c-e06e-4aca-adb1-541a6b9ff174" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "61acb390-5790-466f-b2e3-a103177e4b64", "3918f00c-7798-4a20-ad7c-dffec1aa6553" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "47017685-1f42-4d28-a438-c4891580d21c", "a57891f8-f35d-4768-8ae5-546ea62a797e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c6f99cf4-900e-48ed-a9f3-b43bb387004a", "3eb3ed9d-4cef-439b-aecc-d2c18b17faa3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "82e328ea-9a2a-4029-ae5a-10eede4c738a", "eef875fa-8df9-4b6c-b734-42c1d2aa2651" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "13",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "312e7985-6d82-44a5-8db4-8b1836102ed7", "4303988a-ef99-4586-a8ce-1abee7d17986" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a467d7e8-21f9-4a5d-b7fe-4d43251d36e5", "e4f75ed0-ab2c-43d1-9ef4-700efed90d86" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "15",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "416eff74-d0c8-4310-812e-e7aa8812401e", "6106107a-f91d-4967-bbd8-12fa2b46a33c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c73e6a34-88de-4c53-a249-19e1214ce1ae", "ea6938cd-ed19-4bec-ade4-df5caf37775b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "17",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "aa248053-2b38-45e8-bb61-786efef6b03b", "cd76b97f-be14-429f-988d-652c9773da4f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "18",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d49cd6e4-3cac-4a4a-ade1-1826da8ce62a", "deb92ba8-b1af-49e2-8f8c-4f42cc9bb351" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6e3189a0-2f4d-4ecc-8c9a-6a573d004bb8", "d745893a-411b-44c4-ba3e-00b0f26cdb63" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "401ccea1-5eae-4b55-9bfd-d210e67ca279", "a672b877-d5ea-4725-8d67-8a6944161abc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "313c8061-6cc6-4b8e-86d6-928082434415", "9f16a06d-9a16-4394-9ab1-2f9521d4ae20" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a4a696dc-3081-4dbc-94d7-9f939d222d62", "457713fa-d75b-4273-bd59-c8e195324c4e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "083e2bd8-497c-486d-9865-397aa123d267", "b48317bd-10ff-4c78-97cc-0844961c2905" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "23",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2a170333-006d-4455-b2a3-5b3d041a97ee", "2238bc55-ff29-49b4-ba24-6702da005414" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b6b965a0-3f64-435e-9957-fc89a6da6e24", "bbb859e4-6080-48cc-b2e1-c43497d0b959" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "25",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9b890560-96b3-461d-aac6-e47abfe9f035", "744e9e41-43fe-4554-88d6-f0be705f77b9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "26",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5352a0ba-b6d6-457b-b170-699a46490ae4", "dfab3b85-04c1-411f-938c-bda2a96b494d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3521fe41-ecd6-49c2-a8da-ea6fc946d030", "701162f1-c0ec-4110-9df5-badf270b0b0b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "28",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7195ab5f-f5bd-430f-b79e-72793948e0d3", "be8de230-1e61-46b5-8fc6-8a6b49526f75" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "85879766-aa3d-46ad-b4a0-98a09add0056", "d2cbd844-3fc6-4da0-bdd2-457ac6ef34a0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "84ce6a73-1333-4d37-acb0-449bbdf4ea6d", "cb93a189-9107-450d-b1a0-f7352afa75db" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b0a5824e-22d8-4417-b95e-f16f894a83bb", "1d332f30-dcbd-4fa0-83a4-c602f9ea8418" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "31",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7c1c075a-aaa6-407c-8a0c-773d6415c9f5", "4ee4b25e-4f72-44d5-adb6-8432569bb5ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fe47c036-448d-4d0c-a33d-95d8baaddd23", "c6953f46-f74e-4655-bbf9-2f6575d6ce45" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "33",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cc7129f0-f54d-42ca-aa73-0371fd0da10d", "2cdea805-561a-40d0-9be9-28569226aca6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e5f4d9dd-d50b-41ac-a3a2-e4f845210994", "035cb5f2-a13d-42cb-85d0-b1888a2c2405" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5a7f4732-4215-4fe5-8e9a-5884774334e5", "a192c46e-2bf3-481c-92ef-c23c17f1855c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "36",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9c0d6bc8-15e0-49b5-893c-b42cbd5f5ee0", "f1bc0609-72c7-45da-a429-5a83c6766aba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c899a9a4-23a2-4622-bc53-d661da923c9a", "f6473694-a4fb-494e-91e0-7b458be30966" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1722a0d6-00c0-4375-9b16-e1786d9cd49d", "60c4f5aa-d32f-41f4-a1ea-14b6982d60ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "39",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5a065f08-684a-4fca-8ed9-a5400921020b", "794503b2-ab87-4e5f-a8cc-7fe537ffc265" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f66ae0ab-4152-42d4-90b1-671c1bdeea82", "77b94fd4-ff77-4e71-8a2b-3133ffa06cde" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "40",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ab0af1ca-379f-45a8-8876-3bd59f6dedc4", "42b0335d-ceb7-40aa-aafd-59fbb46ced4b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b0e0ae09-30b1-4dda-83bc-d5b11e1e9b04", "2bc60a4e-b8ab-4ac9-959d-99cbb6eca292" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "19d06128-389c-4c5d-9045-55e79c99eb5e", "b5402475-a8e1-45d1-befb-224351e22e9d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "43",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6d56b7ae-bebd-4c68-bc4e-2f5fa25e57bf", "c1a7dc2b-5166-4b4e-9620-96a16b47da0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "46a66c59-59e7-4a1f-94e0-273b83e53a40", "7c0476c1-6e85-41ad-a51c-80571aefce82" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d6012ce3-b9f5-4a68-b06e-01372d93a581", "34b903fb-fde6-4dd0-900d-baa25a6b0841" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "46",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4f2c2345-527c-4fd1-ad2c-8406ad77bc82", "91e9a3de-94db-4f33-a01d-5c835c66f3fa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "47",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6ee6107c-b1c3-4130-87c1-b2fece9f466d", "1143871f-9325-493e-86ef-16fcc4140a37" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "50a8fc1d-31aa-4792-8ac6-d600f3ae3b7d", "0cdb8c80-4139-4b9b-8384-48bcd6a4b900" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "49",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ead1c7a9-6568-4463-a863-6dcde7a2c25c", "deae75f8-509e-4156-80b0-345458e71d14" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8c7f6a4a-ef9f-4be1-995c-3db25648c35f", "7c8f8465-3641-4aae-9dda-43cf4a5a842d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "50",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "20f1ba0f-a1b0-4ffc-b87a-6a9b8459eee1", "d497af27-bdcc-4069-a4ec-752fde7a6d20" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f2cb0b3e-4c92-402d-ba8c-87967e836e10", "121f301a-7ec2-4385-81e2-f0a409be6463" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d452cc2e-19a0-4c30-bba7-4bb6fd90a29b", "ad2e8411-20fb-4e0c-baf7-b820edb6bc0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "53",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "97340a95-43d2-4a28-8ae6-c89870d73d8e", "f61f1820-9eb9-4d26-b959-eae82108922a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "327adc00-5173-4023-bb87-516b49f83cc9", "cf2e05ef-464b-4288-91c6-103d4171db7f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "55",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "26dde347-f97a-4071-9655-ab2fa3d33c55", "10f46e10-ea18-4cd9-8a5c-be5a8d79c384" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c0346480-4953-4cac-b079-3854bf6d22b6", "32aa7df9-5d34-4f71-b9ef-2ffb154fa9b3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7ee6ae6a-250f-4e71-b4ba-e1c6769346c7", "0dd0ccb3-c40c-49a1-b4a6-77ea9e673339" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "58",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b30ba2f1-66d0-413d-8ac9-bc53071cab88", "faf3ab66-36b8-43f9-bb0b-cff78e52b2ef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "82475c79-fb4e-4a04-93e2-c704dc978260", "b8dc30d1-6b4a-4beb-aba8-96705294aa25" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "753e0c64-e099-462b-9f86-aaf6eb062612", "516a844a-f09a-4137-839e-7429c99af15d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fb367673-9b2a-4f51-a903-5094aafd0181", "4ff55bc9-bb42-4b09-9836-1bf8ac79f1cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5af193db-4a68-4bb1-af75-c346a1665d32", "ecc7f049-8975-4502-9e2a-1a1f7a5c8a98" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "42eaccf5-9803-461c-a035-0dedb76b0547", "e2e7ad78-3e57-4643-94d4-1e8f52862363" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "63",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "de4b9e48-deaf-44b5-b0f1-e414b1c4bff5", "4e4bedb5-09cb-444f-94c7-745625b2d075" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f5a86aa0-109a-4a48-9b3a-3d573cec8dff", "729d5d57-667f-47b0-86aa-6d884d893d91" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "65",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "99e2251f-f4e7-4a0a-9cf4-9de250016399", "f7d27f1f-01d7-4191-9e76-62b730fc3cbc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8bdd631e-da68-4d82-b4e9-f09c9a0d5db3", "965d9cec-6991-422a-b9f7-e331f1d8ed4c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5716dab6-2f4f-4a7f-b6f6-9e5cf73d8123", "28c4bb0a-df04-4395-8d89-005da856f9b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "68",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3bf509eb-94b8-4713-9352-fa9418b493d4", "4dd0061c-b8c5-4f87-bd7a-b1f6bdf09a1a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "db365db5-f31c-4ac4-8e73-620191c5c578", "eccb9472-e7dd-403c-abba-84e37cc9c950" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5bc8f1c6-c21f-45eb-9ce4-5803bbec1940", "4dd3ffbb-b5c2-4938-b5cb-43c643d93922" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "70",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a04c43e6-5f06-4fc2-8a66-abe40ac66909", "d9ff1c28-2401-47d0-a006-a88981aa583e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8bb20c8f-4e1c-4211-ad73-60aabd327609", "b4c09aa2-be5e-432d-8ad3-fb3b5ad4c2e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1c0f551f-82ad-4fee-8a41-de4a01650d59", "dcc61826-3c48-4bbf-8486-a77b05fe3f7f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "73",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b7b620b1-7e90-4e4e-9187-ad2bcb413322", "f4bff4a1-bad3-4f74-84f5-0b5ee05b77d3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f6773792-2e81-4cde-83cd-a16e2cf55621", "8288d0d3-4271-4435-9c75-cff0222da3a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bc3f0762-5bc5-476a-a985-74c4d43a80a5", "e0f6e16a-9417-432c-bf8b-04a752963a40" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "548b75f8-a85d-4608-bb5a-1da189b6da6e", "830aa891-37bf-4d8c-b203-3d9e32c8e85c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "77",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bf63a908-f57b-40e3-b6b2-d730be6420c2", "38ef31cc-e709-4f0d-99e0-4a4689d9a50b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bb2ef046-7efe-48d1-a070-5ba53607a8bc", "b6a21dc5-77cc-40bf-aecd-9e3aa59b83bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "06775e6b-03a3-4e9d-a273-d34a9ae236ef", "a1045d93-ff78-44d9-8f36-4f30ddcca6b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f45c2073-97da-4113-83bc-b40650c20e78", "2a35aa11-8356-448c-bd3f-90f76592e6ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "39d92584-ddd4-48d4-afbd-873125489ced", "a4be1030-ad00-46e1-bc48-a026f75ae5b3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "81",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "50030b8f-d4aa-4f15-b8a0-b638868a120b", "59290cf3-2642-413d-8630-341abff0c529" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3806ee62-38d8-4d01-a86e-3e2866476612", "c487e384-77a1-435b-974a-7fe53e4fe5a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6b81435f-59c8-41c1-a16e-307f92976c31", "5b2f55d4-6669-4f3f-bffb-724e49637a07" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "84",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bc15dbca-cf7e-47d5-88da-5c9a18438e31", "360bac26-8d35-45b6-b134-6f5a8dbfc4bd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c444bff7-5ccd-4c0b-9a62-9f07fee85250", "327d7e32-bf4b-469d-94d5-f12f622d7729" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "22f95161-1b6d-42ac-97db-241246091265", "e153e9fe-db27-4c08-9e9e-0ee913570f22" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1961c674-8dc2-453f-b863-3f991307344e", "78ae8729-6dd7-4f86-a8dd-f4df3792121f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d4ac4851-7623-4354-bd2a-059ba32dc016", "94bee874-21b2-4129-9151-2760f8e51f0e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d65af24f-0b80-4986-a8f3-d028d1a9d06c", "4db1d72b-ea77-44da-804d-68d2b6d12801" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "27a7334c-5cf6-4132-ac74-fa448c161675", "3d936e79-e859-4132-bff1-a196965604a5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8f847e18-d65a-441e-a517-3cf5e0dfa526", "64afa3bc-4f97-4301-b640-87627ff89916" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "54ad5577-abb6-4ac5-9c6b-eced20d3b6f4", "c3da8d45-bb4a-4373-833f-bb4837ed3a88" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "92",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b1f51c01-3730-4904-92bd-f26fd1b592c0", "c5911ba7-a3c4-4f59-95f8-14cb5c9b7a76" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cf8c1b76-2e01-41b9-9c32-7847d9680a8d", "1038195e-49dc-4e64-93ec-ffcb747d761c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "94921921-9054-45e5-8945-a455d86caf8a", "f021fb24-7d55-4c3d-842d-afa69d5b18ae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "95",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7e62454a-0ecc-44a1-9b85-852cf252b389", "184af29a-40c1-405e-a5c5-e78dd861daf0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4e4ba191-d0a1-4988-9d88-9fd841762102", "4aa306ce-7e3b-4903-8c9f-fb5a35553455" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "558a4a22-cb49-4e0c-8bca-de8e325aafa8", "0cfa4e59-7497-4f54-8280-42bc533d1fc9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1c7ea8bb-75de-42e9-a603-68e359753f65", "94b620ff-8c1f-4311-b604-96e57f548315" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b2a389f0-234a-4dcc-9345-1baa2ce38f2e", "a0d75e06-00b7-4e27-aaaa-10e95f0555d9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users_Blogs_Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    InteractionId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Blogs_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Blogs_Interactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Blogs_Interactions_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Blogs_Interactions_Interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users_Recipes_Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InteractionId = table.Column<int>(type: "integer", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Recipes_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Recipes_Interactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Recipes_Interactions_Interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Recipes_Interactions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4a1e410e-bb4a-482f-86c2-0e1473a1782b", "659b7873-6fe5-43ed-beaf-005704c610e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "aadca1f7-3a38-4527-afbf-9c8b1e5fc0d8", "936ac748-9b9c-4ae6-b8e0-60f278a47074" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e6ac734f-03df-4b54-ac91-09d9ed32acc7", "d886f6e3-4103-491f-841e-e476ae50deed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6a1bdb2e-98a2-4a0a-bd13-a15d27304437", "fc332d10-e94c-418f-85e8-4b85057e5082" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "abf91fe0-9bed-4a73-96d7-e36e160a7832", "62b953eb-7ee6-4dff-a1cd-b25e7c8f4536" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "13",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a18ecb04-ab63-4772-b5d1-5723c767dfa5", "451e7a29-84b1-413e-afb9-fbe72dc6fb97" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f6084376-6830-40ad-994f-1d3ff8096936", "631b2c81-bbea-4f50-80be-62ae4c223731" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "15",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3178ee4c-a164-4860-a621-81d5b873e045", "0ee93989-b4b7-4ad0-9692-2f461699fd0b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e1cc61f3-710e-43b0-a1ac-4d49e6831417", "4b8f5c27-0bb1-4447-b9dc-0714a9629241" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "17",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "22bd1601-b408-4757-be93-bf1c0e0d734c", "bdd9a36b-bf1c-4539-bfe6-11cafd09146e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "18",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4b1e7398-d32c-4935-95e4-0040f646043d", "b170e1e8-8d3f-4ec0-b8ff-e275424564db" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a41da8a9-5394-48ce-993c-a546f1807347", "d12c3592-c2e9-4e50-bf72-3281b9f26ea3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c08eea4d-e5cd-47db-bcff-fb64455ab3cb", "e45842f1-be86-40e9-9480-08ae2f4c5634" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "db13e0e7-1b74-45a4-9544-b2d469eee245", "db2be30b-c7a0-44dc-82be-960665973e24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b659cc2f-5bdf-427a-b3a5-cfdbb1c1b8e8", "2c406128-ca1c-4586-afac-98b3c009b99b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1cb26c5f-0496-4c35-b810-01a2abeed6ce", "0907ff8f-c4ac-47a7-b985-b3d75ea204ec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "23",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "428065cf-cd68-4cdc-8ff7-a4f200b43c06", "038b85ca-0cb9-4f89-a0b6-dc2a90346135" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f023dce0-03d3-4ddd-a03c-9cf4817f492c", "706cff15-6b95-4678-8272-78177ccd379c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "25",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "10172152-1d22-49f8-b0e9-e02fc45f71c6", "2f40a159-af58-4504-a288-2943a0a8deb6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "26",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "45e715b6-d6fe-48c6-80e4-9e997f866e69", "e2dc5cd3-eaca-4532-a076-0dce7d78a75d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "27",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "eb6c1ad9-0fef-48b7-9185-16a3b82d7607", "039c00b1-0d8c-4f60-898d-657cdf4614a1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "28",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0748efb9-c23f-413e-bdff-cd786f678780", "408abc19-8271-4dac-b178-f200b3d52d29" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "29",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7a729937-d3a8-4c22-aa02-559e2cdb0e6f", "1d00b878-b5b8-472c-aa58-76ae8a724c27" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7459738c-5855-4739-93c0-f357d8f27840", "475d4be2-aae3-4ae5-aee0-d7037ec897bc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ae07bfe4-fffe-46a1-922b-333580671a57", "758acfb5-e200-4e9c-9b08-42220729a64e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "31",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2ff8764e-899a-45dc-b4e4-a6d24f646253", "cee554bf-79fb-47c7-be0e-e4ef4c5c6770" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5f730e28-d853-4d16-a584-0b89f67de4be", "831d4829-7316-415d-bf5f-0215de541892" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "33",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c2fdbfad-d8fe-4ca7-b8bb-12ae22ce5295", "212ad688-fe71-49e2-978b-5506125908cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f503a111-bb03-4648-8ed7-11edac212ba2", "d185f09f-e9fb-4191-99df-a141fdd99540" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "77357c7c-9e27-4b84-87e4-1437bd15bc05", "91595e84-86c5-46b2-b77a-a6328278a495" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "36",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8368a7a7-c587-49f5-b06b-027c7484a5b3", "0cb7ea69-c543-4a89-9075-b9d1891e81a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "64831e48-b4f7-481b-8455-a9a5e6366667", "da2df4d9-15f4-401a-9ac4-ebe461747d79" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "38",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3d28539f-af77-44ac-b52c-d0f12a3c923c", "8bd14d9c-8ce4-4750-b2e6-24bcb4fc16a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "39",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "679b58a2-cfa0-4dc8-9ebe-28d0da88c3ff", "17561b58-5c81-4099-b6b1-c4f841d127e0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4a886070-4a71-442a-bc3c-53319302f57a", "89b5faca-52b3-46f1-9420-c1144e1e20a1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "40",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5e6dad12-f8e7-44ac-860c-e26c95eab291", "375d47f2-a353-4f75-9168-b31c7e01f8f7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "57772bd4-eb50-45f9-9901-ee93a88c3d21", "20e31db5-311e-449d-8196-08db5a1fa51c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "42",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9d30471b-36c0-4c80-b66c-8c5e83fbe5ed", "0aa640f1-ab6b-406c-86e1-52d1fb35482d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "43",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b0876d35-7b08-4db0-973e-283450016fc7", "12840c68-f06c-4b90-b0a0-c314a9104cd5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4a517e7d-8cac-4ab6-9396-e468ab4ed00d", "eb1518d4-bb25-4bbd-9906-b404b895b172" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0706ba57-dc70-436a-9427-05cc3d672d2e", "d6200f88-53fd-4faf-bddc-b4e82a1f67c3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "46",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "da998f46-af12-4891-a62c-ae264b028ed8", "329d847d-c334-401a-8d32-895c960f17f6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "47",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "af78f2dc-3ebb-48dc-ba0a-4a31df5f1d4a", "6479e1c0-4cc9-4d35-b743-e4087685f90e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b2b64e9e-8bb5-44a0-87dd-d322d3d0390b", "30a2e2a9-45b3-43b3-99e4-6e88b3112f65" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "49",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "73140f7c-40c3-41e0-a549-59a913c28d01", "2e3be33c-4ee0-4c43-80f4-3652691a925e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a73f3aac-98cd-4d1b-b20f-6a6f7e8be8af", "1c004def-5f85-44d2-91d3-d0a4af643e48" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "50",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cbb7be8c-b64c-4c00-9863-3af85638f9b0", "207c1c5b-13b3-400b-bf46-d34389e2c9a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f4cca073-9505-44b9-81f8-db8177ba9e8d", "9a9969b4-5f1c-44f5-8d42-548646f69d37" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a9a1d1da-4123-426d-ba63-1168be07a569", "3438cb2a-4a70-47e3-aeec-88ee2786055f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "53",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d34404e0-3c52-4753-82fe-f24088ae8faa", "69bba1b5-6274-4019-8d18-cbdf415e8b8c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "aba1225d-d608-49ca-a92c-2a40338d2222", "3e20e6f2-f1a1-4520-9b2f-df86192683ac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "55",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4c7648bf-f1f7-49b5-b987-553ac1da0bb0", "da622cb9-9d6b-4932-aa47-d8608111270f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b7982806-7ce9-4b1a-ad23-ad4d42e95597", "baeabf0d-060c-4357-9b00-aeb4460622cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "32cf41fd-0a60-4368-819b-1ea8f313ccb5", "b0c5576c-2742-4c6c-9bba-f50fe229ca2b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "58",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0d22306b-ec67-4954-b286-001db675e42b", "f6018da5-979e-4ba7-9fb5-5028ca0b0fd7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "67173dd2-6b35-4c34-81cb-5091f22839ed", "7c544b3d-336e-4530-a35c-f75fa19cefde" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3a2c6c45-27ad-468e-878b-8690976253fe", "01ec1e8e-432d-4106-9567-02c4b0500449" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "47076b38-7386-4ad9-a817-1cf470857063", "b82707b1-1fc8-44bc-9de0-0e398c14e302" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c5ea451e-41ed-423a-8c2e-5d91d8533383", "c69f4b0f-5ded-447c-b46e-397f5fc9bda8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a70d9cfa-af30-4f8c-94a9-5f5f5c7caca4", "9377fd55-5eb5-472b-ae82-e477641ce34d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "63",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b349da69-f680-4166-ba9e-592483d41480", "ee75afd2-c1fc-498c-8c81-61427214c822" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3635a755-77bf-4c62-8446-e0035514bd22", "d1f3d818-35d4-4ae7-814a-6e78023c1c8d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "65",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4c09dbb1-075f-4fc5-a03e-5d468459739a", "348c4d6c-72d2-4403-96a9-0648c7d17b40" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2ed23592-2f24-4bda-acce-d43b45466e66", "76de895c-7237-457a-9bf6-695f0141aaab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6f333817-0777-4c36-a368-5302b7c71e4f", "c208a8c2-3f4b-4497-a596-b326ef777c75" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "68",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c172ec56-fb22-4bf3-82b7-f4589f567af9", "0f464fc4-bf2b-4987-a1df-6e4dcf4f8c65" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3f559a0c-d699-4cd2-b09f-d134903fa169", "1dc71dcf-8be2-4ea5-9fee-2cbf420836d1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0defc1d8-4a22-4e0b-96d2-a3b375a593a0", "0402ed4a-7033-4181-90ab-52791019fc16" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "70",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "01e1cb9c-fd89-47a5-a733-450e701497ef", "b0fc1a64-dbfa-464a-86b3-3d530e76427c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "74cd9d43-42af-4d62-aca9-84590dfb8b2b", "c976802c-c28e-40e5-ac4a-98c87c134533" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "25e0210a-f95f-432a-8684-ffeb3096e98b", "b08eed1c-425e-4add-832c-8fa73a5f2818" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "73",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "14295168-8883-4234-a4de-7d974a3d91a8", "881f579f-0fbf-475b-8a99-032b45b2bb7d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "98fc2c20-5760-4ed2-b947-f1056726cbfb", "5e8aafae-f44b-4777-9d28-6eb24458bfc4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e6d921d6-b349-439d-abac-4fe0b3119651", "e064bce4-1439-4cfb-81c7-d6a17adb38ad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a1537273-e66d-4621-8b02-7980ad1e6af7", "366c29c9-279a-4e27-94d0-03ec2c3ab903" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "77",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "38792730-3c6a-4211-8e0b-14206e220014", "39acef72-ff95-4c63-8e06-71170ece703a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "44aaa307-eb76-44f5-99fd-9c120e2e4633", "25d53d68-d7fa-4d9f-bb2f-be9f02a9f8f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8cf9f3d9-e7db-4d2b-8515-88ea97518ce4", "45ce3a3f-8d29-4642-8d19-4aa095935836" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a27ca1cc-4ecc-4ec3-b475-79fb45e45537", "43dd15b3-1138-4bd2-a737-bfaba5df4cf8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9eba00e3-6fe2-49e6-ba0c-9f3c5f6e40cd", "8d694718-ffe4-45df-a909-b1283af5749f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "81",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e7fb15d2-0461-472e-ae3c-e12fa9c05829", "8abbe28b-72c6-4cf1-8b5c-a1c794dac810" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "82",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1a9d9314-1e50-4b4a-bb5f-8feb18bacb73", "e2dc673a-684b-487a-b9ff-8a16411693ff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "24017790-bcbe-4999-9fc4-74519b7b6ec9", "236bb638-cc5d-41b2-a86d-e8128ac02320" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "84",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "73f9cbf3-ad01-4b2c-8878-f3e4270ca625", "e54c5ad3-cbf1-4872-b3e7-116e20382be9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1d91a840-454b-489e-bdeb-b92ee2642440", "371dfc52-5037-4d41-ac57-0c107e3e9c9b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b486d3e1-3e0b-492d-983b-32a5cd73e133", "c5d6f4c9-838a-40ce-99bf-6634c5c37170" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "24139242-12a5-4682-9036-701a4b435ff6", "25cd90e5-c629-4df2-a99f-6dc3bf3002b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3bbc99e7-aabe-438e-9bbb-5c389cbe381e", "8ea63f34-f418-4b21-bf11-c1b6c8fa1d68" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b514850e-7d70-4a9c-b279-e7c2ecd56ff0", "dc4a33c3-b880-4106-be60-36477c1f7a5f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fad22d4d-bc31-4367-ae8d-d74f2532929c", "a1072c37-507c-4d3e-b623-e98d2fc09747" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6ed1bc51-0191-4951-b513-568df5302162", "b0ac814d-b2ee-49cf-8a23-712d77b221ef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c126bd3e-5453-446a-b175-fd2d29ecf5a0", "0a378878-ae0a-49a3-b89e-da4b2a967bc5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "92",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4a7f6b2b-7f38-42ad-8c1b-7d18714d6bed", "72d08678-d22c-404a-a237-d90538f7bcc0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f090c74b-fbfd-4c4c-baa8-3ee5849b9e26", "0baa1314-e571-47c8-b010-2475208800e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3818f2dd-ef21-49b8-9ab1-e50ec5c5f343", "db8ba970-1da5-4728-9834-9c5a432a195c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "95",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "82853257-a603-4a40-b9fb-75a26bf939c3", "852ff3a2-96cf-4bb2-b501-7a7cd57bc559" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "87eefdb5-72c7-42a1-9673-7cb26f87209e", "19ec8890-bfc4-4f5e-8e82-a403a102e245" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c3be4013-e826-45a8-bbbe-dd2ced2e9fe2", "9013350b-7bde-4aa3-b882-f230190cc1bf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "07b65c16-c2cd-41f3-8ea0-2e2a56f30713", "962d3803-3b03-454d-8190-68067a9dc0d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "21b0ed5d-2900-40bd-a071-7e21344fbea9", "3355b35e-f010-45d5-a311-7df12d75ca50" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Blogs_Interactions_BlogId",
                table: "Users_Blogs_Interactions",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Blogs_Interactions_InteractionId",
                table: "Users_Blogs_Interactions",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Blogs_Interactions_UserId",
                table: "Users_Blogs_Interactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Recipes_Interactions_InteractionId",
                table: "Users_Recipes_Interactions",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Recipes_Interactions_RecipeId",
                table: "Users_Recipes_Interactions",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Recipes_Interactions_UserId",
                table: "Users_Recipes_Interactions",
                column: "UserId");
        }
    }
}
