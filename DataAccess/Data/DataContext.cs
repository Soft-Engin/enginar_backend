using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Reflection.Emit;
using DataAccess.Migrations;

namespace BackEngin.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Preferences> Preferences { get; set; }        
        public DbSet<IngredientTypes> IngredientTypes { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }       
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Recipes_Ingredients> Recipes_Ingredients { get; set; }
        public DbSet<Blogs> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Name = "User", Description = "Default user role" },
                new Roles { Id = 2, Name = "Admin", Description = "Admin role" }
            );

            modelBuilder.Entity<Users>().HasData(
                new Users { Id = "3", UserName = "zeynep", FirstName = "zeynep", LastName = "kara", RoleId = 1}
            );
                        
            modelBuilder.Entity<IngredientTypes>().HasData(
               new IngredientTypes { Id = 1, Name = "Vegetable", Description = "Fresh vegetables" },
               new IngredientTypes { Id = 2, Name = "Oil", Description = "Cooking oils" }
           );

            modelBuilder.Entity<Ingredients>().HasData(
                new Ingredients { Id = 3, Name = "Enginar", TypeId = 1 },
                new Ingredients { Id = 4, Name = "Zeytinyağı", TypeId = 2 }
            );

            modelBuilder.Entity<Recipes>().HasData(
                new Recipes { Id = 2, Header = "Enginar Şöleni", BodyText = "Enginarları küp küp doğra zeytin yağında kavur zart zrut", UserId = "3" }
            );

            modelBuilder.Entity<Recipes_Ingredients>().HasData(
                new Recipes_Ingredients { Id = 3, RecipeId = 2, IngredientId = 3, Quantity = 2, Unit = "adet" },
                new Recipes_Ingredients { Id = 4, RecipeId = 2, IngredientId = 4, Quantity = 3, Unit = "yemek kaşığı" }
            );

            modelBuilder.Entity<Blogs>().HasData(
                new Blogs { Id = 1, RecipeId = 2, Header = "ENGINAR YOLCULUĞU", BodyText = "benimle enginarın sırlarını keşfetmeye yelken açın", UserId = "3" }
            );

            PopulatePreferences(modelBuilder);
        }

        private void PopulatePreferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Preferences>(entity =>
            {
                entity.HasKey(a => a.Id); // Primary key
                entity.Property(a => a.Id).ValueGeneratedOnAdd(); // Auto-increment
            });

            modelBuilder.Entity<Preferences>().HasData(
                new Preferences { Id = 1, Name = "Gluten", Description = "A type of protein commonly found in wheat, barley, and rye." },
                new Preferences { Id = 2, Name = "Dairy", Description = "Milk and products derived from milk, such as cheese and yogurt." },
                new Preferences { Id = 3, Name = "Nuts", Description = "Tree nuts including almonds, cashews, and walnuts; excludes peanuts." },
                new Preferences { Id = 4, Name = "Peanuts", Description = "A type of legume that is a common allergen, distinct from tree nuts." },
                new Preferences { Id = 5, Name = "Soy", Description = "A legume used in products like tofu, soy milk, and many processed foods." },
                new Preferences { Id = 6, Name = "Eggs", Description = "A common ingredient in baking and cooking derived from chicken eggs." },
                new Preferences { Id = 7, Name = "Fish", Description = "Seafood including cod, salmon, and tuna." },
                new Preferences { Id = 8, Name = "Shellfish", Description = "Crustaceans and mollusks like shrimp, crab, and clams." },
                new Preferences { Id = 9, Name = "Sesame", Description = "Seeds and oils derived from sesame plants, found in many cuisines." },
                new Preferences { Id = 10, Name = "Vegan", Description = "A diet that excludes all animal products, including meat, dairy, and honey." },
                new Preferences { Id = 11, Name = "Vegetarian", Description = "A diet that excludes meat and fish but may include dairy and eggs." },
                new Preferences { Id = 12, Name = "Lactose Intolerant", Description = "Avoidance of dairy products due to difficulty digesting lactose." },
                new Preferences { Id = 13, Name = "Pescatarian", Description = "A diet that includes fish but excludes other forms of meat." },
                new Preferences { Id = 14, Name = "Halal", Description = "Dietary requirements based on Islamic law, including avoidance of pork and alcohol." },
                new Preferences { Id = 15, Name = "Kosher", Description = "Food prepared in compliance with Jewish dietary laws, avoiding non-kosher animals and mixing meat with dairy." },
                new Preferences { Id = 16, Name = "Low FODMAP", Description = "A diet that limits fermentable oligosaccharides, disaccharides, monosaccharides, and polyols to manage digestive symptoms." },
                new Preferences { Id = 17, Name = "Nut-Free", Description = "Avoidance of all nuts, including peanuts and tree nuts." },
                new Preferences { Id = 18, Name = "Plant-Based", Description = "A diet primarily focused on consuming plant-derived foods, minimizing or excluding animal products." },
                new Preferences { Id = 19, Name = "Keto", Description = "A low-carb, high-fat diet focused on inducing ketosis for energy." },
                new Preferences { Id = 20, Name = "Paleo", Description = "A diet based on the presumed eating patterns of ancient humans, focusing on whole, unprocessed foods." }
           );            

        }
    }

}
