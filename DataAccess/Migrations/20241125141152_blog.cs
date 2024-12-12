using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class blog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blogs_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3", 0, null, "33157474-c8cd-4c1a-8b0e-eae7756dc77c", "Users", null, false, "zeynep", "kara", false, null, null, null, null, null, false, 1, "c88b2936-1e6e-49d3-ab03-36c1185a36e2", false, "zeynep" });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "TypeId" },
                values: new object[,]
                {
                    { 3, "Enginar", 1 },
                    { 4, "Zeytinyağı", 2 }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "BodyText", "Header", "UserId" },
                values: new object[] { 2, "Enginarları küp küp doğra zeytin yağında kavur zart zrut", "Enginar Şöleni", "3" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "BodyText", "Header", "RecipeId", "UserId" },
                values: new object[] { 1, "benimle enginarın sırlarını keşfetmeye yelken açın", "ENGINAR YOLCULUĞU", 2, "3" });

            migrationBuilder.InsertData(
                table: "Recipes_Ingredients",
                columns: new[] { "Id", "IngredientId", "Quantity", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { 3, 3, 2.0, 2, "adet" },
                    { 4, 4, 3.0, 2, "yemek kaşığı" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_RecipeId",
                table: "Blogs",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, null, "4c37ad7a-61f1-4719-868b-3081d23c6793", "Users", null, false, "Zeyn", "Kara", false, null, null, null, null, null, false, 1, "c8aa291a-c82b-4a79-a01d-b7b6e55106ff", false, null });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1, "Enginar", 1 },
                    { 2, "Zeytinyağı", 2 }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "BodyText", "Header", "UserId" },
                values: new object[] { 1, "Enginarları küp küp doğra zeytin yağında kavur zart zrut", "Enginar Şöleni", "1" });

            migrationBuilder.InsertData(
                table: "Recipes_Ingredients",
                columns: new[] { "Id", "IngredientId", "Quantity", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { 1, 1, 2.0, 1, "adet" },
                    { 2, 2, 3.0, 1, "yemek kaşığı" }
                });
        }
    }
}
