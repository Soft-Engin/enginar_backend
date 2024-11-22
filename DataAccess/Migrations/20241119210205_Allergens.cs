using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Allergens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Preferences",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "A type of protein commonly found in wheat, barley, and rye.", "Gluten" },
                    { 2, "Milk and products derived from milk, such as cheese and yogurt.", "Dairy" },
                    { 3, "Tree nuts including almonds, cashews, and walnuts; excludes peanuts.", "Nuts" },
                    { 4, "A type of legume that is a common allergen, distinct from tree nuts.", "Peanuts" },
                    { 5, "A legume used in products like tofu, soy milk, and many processed foods.", "Soy" },
                    { 6, "A common ingredient in baking and cooking derived from chicken eggs.", "Eggs" },
                    { 7, "Seafood including cod, salmon, and tuna.", "Fish" },
                    { 8, "Crustaceans and mollusks like shrimp, crab, and clams.", "Shellfish" },
                    { 9, "Seeds and oils derived from sesame plants, found in many cuisines.", "Sesame" },
                    { 10, "A diet that excludes all animal products, including meat, dairy, and honey.", "Vegan" },
                    { 11, "A diet that excludes meat and fish but may include dairy and eggs.", "Vegetarian" },
                    { 12, "Avoidance of dairy products due to difficulty digesting lactose.", "Lactose Intolerant" },
                    { 13, "A diet that includes fish but excludes other forms of meat.", "Pescatarian" },
                    { 14, "Dietary requirements based on Islamic law, including avoidance of pork and alcohol.", "Halal" },
                    { 15, "Food prepared in compliance with Jewish dietary laws, avoiding non-kosher animals and mixing meat with dairy.", "Kosher" },
                    { 16, "A diet that limits fermentable oligosaccharides, disaccharides, monosaccharides, and polyols to manage digestive symptoms.", "Low FODMAP" },
                    { 17, "Avoidance of all nuts, including peanuts and tree nuts.", "Nut-Free" },
                    { 18, "A diet primarily focused on consuming plant-derived foods, minimizing or excluding animal products.", "Plant-Based" },
                    { 19, "A low-carb, high-fat diet focused on inducing ketosis for energy.", "Keto" },
                    { 20, "A diet based on the presumed eating patterns of ancient humans, focusing on whole, unprocessed foods.", "Paleo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BodyText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");
        }
    }
}
