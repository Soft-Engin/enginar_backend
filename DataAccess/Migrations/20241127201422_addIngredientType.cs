using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addIngredientType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.CreateTable(
                name: "Ingredients_Preferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    PreferenceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients_Preferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Preferences_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredients_Preferences_Preferences_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "Preferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Edible plants or their parts, intended for cooking or eating raw.");

            migrationBuilder.UpdateData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Sweet or savory product of a plant that contains seeds and can be eaten as food.", "Fruit" });

            migrationBuilder.InsertData(
                table: "IngredientTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 3, "Animal flesh that is eaten as food.", "Meat" },
                    { 4, "Food produced from or containing the milk of mammals.", "Dairy" },
                    { 5, "Small, hard, dry seeds harvested for human or animal consumption.", "Grain" },
                    { 6, "Sea life regarded as food by humans, includes fish and shellfish.", "Seafood" },
                    { 7, "Substance used to flavor food, typically dried seeds, fruits, roots, or bark.", "Spice" },
                    { 8, "Plants with savory or aromatic properties used for flavoring and garnishing food.", "Herb" },
                    { 9, "Dry, edible fruits or seeds that usually have a high fat content.", "Nuts & Seeds" },
                    { 10, "Drinkable liquids other than water, may be hot or cold.", "Beverage" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Preferences_IngredientId",
                table: "Ingredients_Preferences",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Preferences_PreferenceId",
                table: "Ingredients_Preferences",
                column: "PreferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients_Preferences");

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, null, "4c37ad7a-61f1-4719-868b-3081d23c6793", "Users", null, false, "Zeyn", "Kara", false, null, null, null, null, null, false, 1, "c8aa291a-c82b-4a79-a01d-b7b6e55106ff", false, null });

            migrationBuilder.UpdateData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Fresh vegetables");

            migrationBuilder.UpdateData(
                table: "IngredientTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Cooking oils", "Oil" });
        }
    }
}
