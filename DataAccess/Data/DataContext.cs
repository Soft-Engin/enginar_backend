using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Reflection.Emit;
using DataAccess.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL;

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




        public DbSet<Users_Interactions> Users_Interactions { get; set; }
        public DbSet<Interactions> Interactions { get; set; }
        public DbSet<Users_Recipes_Interaction> Users_Recipes_Interactions { get; set; }
        public DbSet<Users_Blogs_Interaction> Users_Blogs_Interactions { get; set; }

        public DbSet<Ingredients_Preferences> Ingredients_Preferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Name = "User", Description = "Default user role" },
                new Roles { Id = 2, Name = "Admin", Description = "Admin role" }
            );

            modelBuilder.Entity<Ingredients>().HasData(
                new Ingredients { Id = 3, Name = "Enginar", TypeId = 1 },
                new Ingredients { Id = 4, Name = "Zeytinyağı", TypeId = 2 }
            );

            modelBuilder.Entity<Users>().HasData(
                new Users { Id = "1", FirstName = "Engin", LastName = "Adam", UserName = "EnginarAdam",  RoleId = 1 },
                new Users { Id = "2", FirstName = "Engin", LastName = "Kadın", UserName = "EnginarKadın", RoleId = 1 },
                new Users { Id = "3", FirstName = "Engin", LastName = "Çocuk", UserName = "EnginarÇocuk", RoleId = 1 },
                new Users { Id = "4", FirstName = "Engin", LastName = "Yaşlı", UserName = "EnginarYaşlı", RoleId = 1 },
                new Users { Id = "5", FirstName = "Engin", LastName = "Enginar", UserName = "EnginarDouble", RoleId = 2 }
            );

            modelBuilder.Entity<Recipes>().HasData(
                new Recipes { Id = 2, Header = "Enginar Şöleni", BodyText = "Enginarları küp küp doğra zeytin yağında kavur zart zrut", UserId = "1" }
            );

            modelBuilder.Entity<Recipes_Ingredients>().HasData(
                new Recipes_Ingredients { Id = 3, RecipeId = 2, IngredientId = 3, Quantity = 2, Unit = "adet" },
                new Recipes_Ingredients { Id = 4, RecipeId = 2, IngredientId = 4, Quantity = 3, Unit = "yemek kaşığı" }
            );

            modelBuilder.Entity<Blogs>().HasData(
                new Blogs { Id = 1, RecipeId = 2, Header = "ENGINAR YOLCULUĞU", BodyText = "benimle enginarın sırlarını keşfetmeye yelken açın", UserId = "1" }
            );
            // Configure many-to-many relationship between Ingredients and Preferences
            modelBuilder.Entity<Ingredients_Preferences>()
                .HasOne(ip => ip.Ingredient)
                .WithMany(i => i.Ingredients_Preferences)
                .HasForeignKey(ip => ip.IngredientId);

            modelBuilder.Entity<Ingredients_Preferences>()
                .HasOne(ip => ip.Preference)
                .WithMany(p => p.Ingredients_Preferences)
                .HasForeignKey(ip => ip.PreferenceId);

            PopulatePreferences(modelBuilder);
            ConfigureUserInteractions(modelBuilder);
            PopulateIngredientTypes(modelBuilder);
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

        private void ConfigureUserInteractions(ModelBuilder modelBuilder)
        {
            // Seed Interactions
            modelBuilder.Entity<Interactions>().HasData(
                new Interactions { Id = 1, Name = "Follow", Description = "User follows another user" },
                new Interactions { Id = 2, Name = "BookmarkRecipe", Description = "User bookmarks a recipe" },
                new Interactions { Id = 3, Name = "BookmarkBlog", Description = "User bookmarks a blog" }
            );

            // Configure Users_Interactions Relationships
            modelBuilder.Entity<Users_Interactions>()
                .HasOne(ui => ui.InitiatorUser)
                .WithMany()
                .HasForeignKey(ui => ui.InitiatorUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Interactions>()
                .HasOne(ui => ui.TargetUser)
                .WithMany()
                .HasForeignKey(ui => ui.TargetUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Interactions>()
                .HasOne(ui => ui.Interaction)
                .WithMany()
                .HasForeignKey(ui => ui.InteractionId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Users_Recipes_Interaction>()
               .HasOne(uri => uri.User)
               .WithMany()
               .HasForeignKey(uri => uri.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Recipes_Interaction>()
                .HasOne(uri => uri.Recipe)
                .WithMany()
                .HasForeignKey(uri => uri.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Recipes_Interaction>()
                .HasOne(uri => uri.Interaction)
                .WithMany()
                .HasForeignKey(uri => uri.InteractionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Blogs_Interaction>()
                .HasOne(ubi => ubi.User)
                .WithMany()
                .HasForeignKey(ubi => ubi.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Blogs_Interaction>()
                .HasOne(ubi => ubi.Blog)
                .WithMany()
                .HasForeignKey(ubi => ubi.BlogId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Blogs_Interaction>()
                .HasOne(ubi => ubi.Interaction)
                .WithMany()
                .HasForeignKey(ubi => ubi.InteractionId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void PopulateIngredientTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientTypes>(entity =>
            {
                entity.HasKey(it => it.Id); // Primary key
                entity.Property(it => it.Id).ValueGeneratedOnAdd(); // Auto-increment
            });

            modelBuilder.Entity<IngredientTypes>().HasData(
                new IngredientTypes { Id = 1, Name = "Vegetable", Description = "Edible plants or their parts, intended for cooking or eating raw." },
                new IngredientTypes { Id = 2, Name = "Fruit", Description = "Sweet or savory product of a plant that contains seeds and can be eaten as food." },
                new IngredientTypes { Id = 3, Name = "Meat", Description = "Animal flesh that is eaten as food." },
                new IngredientTypes { Id = 4, Name = "Dairy", Description = "Food produced from or containing the milk of mammals." },
                new IngredientTypes { Id = 5, Name = "Grain", Description = "Small, hard, dry seeds harvested for human or animal consumption." },
                new IngredientTypes { Id = 6, Name = "Seafood", Description = "Sea life regarded as food by humans, includes fish and shellfish." },
                new IngredientTypes { Id = 7, Name = "Spice", Description = "Substance used to flavor food, typically dried seeds, fruits, roots, or bark." },
                new IngredientTypes { Id = 8, Name = "Herb", Description = "Plants with savory or aromatic properties used for flavoring and garnishing food." },
                new IngredientTypes { Id = 9, Name = "Nuts & Seeds", Description = "Dry, edible fruits or seeds that usually have a high fat content." },
                new IngredientTypes { Id = 10, Name = "Beverage", Description = "Drinkable liquids other than water, may be hot or cold." }
            );
        }
    }

}
