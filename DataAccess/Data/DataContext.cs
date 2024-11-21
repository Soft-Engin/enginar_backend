using BackEngin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BackEngin.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) 
        {
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Recipes_Ingredients> Recipes_Ingredients { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<IngredientTypes> IngredientTypes { get; set; }
        public DbSet<Blogs> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Name = "User", Description = "Default user role"},
                new Roles { Id = 2, Name = "Admin", Description = "Admin role"}
                );


            modelBuilder.Entity<Blogs>().HasData(
                new Blogs { Id = 1, RecipeId = 1, Header="ENGINAR YOLCULUĞU", BodyText="benimle enginarın sırlarını keşfetmeye yelken açın", UserId= "1" }
            );

        }
    }
}
