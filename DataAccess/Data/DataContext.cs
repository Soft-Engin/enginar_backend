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
        public DbSet<Users_Interactions> Users_Interactions { get; set; } 
        public DbSet<Interactions> Interactions { get; set; }
        public DbSet<Users_Recipes_Interaction> Users_Recipes_Interactions { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Users_Blogs_Interaction> Users_Blogs_Interactions { get; set; } 
        public DbSet<Blogs> Blogs { get; set; } 


        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Name = "User", Description = "Default user role"},
                new Roles { Id = 2, Name = "Admin", Description = "Admin role"}
                );

            modelBuilder.Entity<Users>().HasData(
                new Users { Id = "1", FirstName = "Berker ", LastName = "Bayar", RoleId = 1 }
            );

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
    }
}
