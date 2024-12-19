using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InteractionsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Recipes_Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.CreateTable(
                name: "Blog_Bookmarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Bookmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_Bookmarks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blog_Bookmarks_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blog_Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    CommentText = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ImageBlob = table.Column<byte[]>(type: "bytea", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blog_Comments_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blog_Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_Likes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blog_Likes_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Bookmarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Bookmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_Bookmarks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipe_Bookmarks_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    CommentText = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ImageBlob = table.Column<byte[]>(type: "bytea", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipe_Comments_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_Likes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipe_Likes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c85a82d4-12cb-4fa1-8550-e7cdea70a628", "d0916b18-e36f-47f0-9628-d65644d6ad4e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d5a1c5b7-4e34-48c4-a061-63bb4179474c", "4915fcd5-b3b6-4f86-9b08-af0bc4b359ba" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3", 0, null, "1f409006-dbd1-4064-a4f6-c94375a4ca5a", "Users", null, false, "Engin", "Çocuk", false, null, null, null, null, null, false, 1, "2dcd5905-0b82-47c8-9b57-3d76004d1f4c", false, null },
                    { "4", 0, null, "d4707db1-bfbe-4329-8a7b-39aa9742b6d2", "Users", null, false, "Engin", "Yaşlı", false, null, null, null, null, null, false, 1, "6c00c55c-e825-4062-bea9-a384c651727a", false, null },
                    { "5", 0, null, "51316e86-1fe0-4ccc-9703-b5914b8faf47", "Users", null, false, "Engin", "Enginar", false, null, null, null, null, null, false, 2, "a3256b78-6a11-4ae2-a284-ff3d73cb6ae3", false, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Bookmarks_BlogId",
                table: "Blog_Bookmarks",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Bookmarks_UserId",
                table: "Blog_Bookmarks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Comments_BlogId",
                table: "Blog_Comments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Comments_UserId",
                table: "Blog_Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Likes_BlogId",
                table: "Blog_Likes",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Likes_UserId",
                table: "Blog_Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Bookmarks_RecipeId",
                table: "Recipe_Bookmarks",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Bookmarks_UserId",
                table: "Recipe_Bookmarks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Comments_RecipeId",
                table: "Recipe_Comments",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Comments_UserId",
                table: "Recipe_Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Likes_RecipeId",
                table: "Recipe_Likes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Likes_UserId",
                table: "Recipe_Likes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog_Bookmarks");

            migrationBuilder.DropTable(
                name: "Blog_Comments");

            migrationBuilder.DropTable(
                name: "Blog_Likes");

            migrationBuilder.DropTable(
                name: "Recipe_Bookmarks");

            migrationBuilder.DropTable(
                name: "Recipe_Comments");

            migrationBuilder.DropTable(
                name: "Recipe_Likes");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "09a48430-5dba-49a3-96b6-ee01398f48ff", "33f0d857-c578-4004-86d6-cf4f9e636a4b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d4998fb3-7788-4478-81d1-2da059e32419", "8ef5ef2d-c01e-4350-87e6-2a118baf9b24" });

            migrationBuilder.InsertData(
                table: "Recipes_Ingredients",
                columns: new[] { "Id", "IngredientId", "Quantity", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { 3, 3, 2.0, 2, "adet" },
                    { 4, 4, 3.0, 2, "yemek kaşığı" }
                });
        }
    }
}
