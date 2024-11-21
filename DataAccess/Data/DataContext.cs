using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackEngin.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Recipes_Ingredients> Recipes_Ingredients { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<IngredientTypes> IngredientTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Name = "User", Description = "Default user role" },
                new Roles { Id = 2, Name = "Admin", Description = "Admin role" }
            );

            modelBuilder.Entity<Users>().HasData(
                new Users { Id = "1", FirstName = "Zeyn", LastName = "Kara", RoleId = 1 }
            );

            modelBuilder.Entity<Recipes>().HasData(
                new Recipes { Id = 1, Header = "Enginar Şöleni", BodyText = "Enginarları küp küp doğra zeytin yağında kavur zart zrut", UserId = "1" }
            );

            modelBuilder.Entity<IngredientTypes>().HasData(
               new IngredientTypes { Id = 1, Name = "Vegetable", Description = "Fresh vegetables" },
               new IngredientTypes { Id = 2, Name = "Oil", Description = "Cooking oils" }
           );

            modelBuilder.Entity<Ingredients>().HasData(
                new Ingredients { Id = 1, Name = "Enginar", TypeId = 1 },
                new Ingredients { Id = 2, Name = "Zeytinyağı", TypeId = 2 }
            );

            modelBuilder.Entity<Recipes_Ingredients>().HasData(
                new Recipes_Ingredients { Id = 1, RecipeId = 1, IngredientId = 1, Quantity = 2, Unit = "adet" },
                new Recipes_Ingredients { Id = 2, RecipeId = 1, IngredientId = 2, Quantity = 3, Unit = "yemek kaşığı" }
            );

        }
    }

}
