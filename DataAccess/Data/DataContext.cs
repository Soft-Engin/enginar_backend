using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.InteractionModels;
using DataAccess.Data;

namespace BackEngin.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Preferences> Preferences { get; set; }
        public DbSet<IngredientTypes> IngredientTypes { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Recipes_Ingredients> Recipes_Ingredients { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Events_Requirements> Events_Requirements { get; set; }
        public DbSet<Requirements> Requirements { get; set; }


        public DbSet<Users_Interactions> Users_Interactions { get; set; }
        public DbSet<Interactions> Interactions { get; set; }
        public DbSet<Users_Recipes_Interaction> Users_Recipes_Interactions { get; set; }
        public DbSet<Users_Blogs_Interaction> Users_Blogs_Interactions { get; set; }
        public DbSet<Ingredients_Preferences> Ingredients_Preferences { get; set; }
        public DbSet<User_Event_Participations> User_Event_Participations { get; set; }

        public DbSet<Blog_Bookmarks> Blog_Bookmarks { get; set; }
        public DbSet<Blog_Comments> Blog_Comments { get; set; }
        public DbSet<Blog_Likes> Blog_Likes { get; set; }
        public DbSet<Recipe_Bookmarks> Recipe_Bookmarks { get; set; }
        public DbSet<Recipe_Comments> Recipe_Comments { get; set; }
        public DbSet<Recipe_Likes> Recipe_Likes { get; set; }
        public DbSet<Event_Bookmarks> Event_Bookmarks { get; set; }
        public DbSet<Event_Comments> Event_Comments { get; set; }
        public DbSet<Event_Likes> Event_Likes { get; set; }

        public DbSet<User_Allergens> User_Allergens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //** TABLE RELATIONSHIP CONFIGURATIONS **//

            // Configure many-to-many relationship between Ingredients and Preferences
            modelBuilder.Entity<Ingredients_Preferences>()
                .HasOne(ip => ip.Ingredient)
                .WithMany(i => i.Ingredients_Preferences)
                .HasForeignKey(ip => ip.IngredientId);

            modelBuilder.Entity<Ingredients_Preferences>()
                .HasOne(ip => ip.Preference)
                .WithMany(p => p.Ingredients_Preferences)
                .HasForeignKey(ip => ip.PreferenceId);

            // Users Table
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasOne(u => u.Address)
                    .WithMany()
                    .HasForeignKey(u => u.AddressId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.Role)
                    .WithMany()
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Events Table
            modelBuilder.Entity<Events>(entity =>
            {
                // Ensure Date is stored as 'timestamp with time zone' in PostgreSQL
                entity.Property(e => e.Date).HasColumnType("timestamptz");
                entity.Property(e => e.CreatedAt).HasColumnType("timestamptz");

                // Apply value converter for Date and CreatedAt to convert to UTC automatically
                entity.Property(e => e.Date)
                    .HasConversion(
                        v => v.ToUniversalTime(),  // Convert to UTC before saving
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Ensure it's UTC on reading
                    );

                entity.Property(e => e.CreatedAt)
                    .HasConversion(
                        v => v.ToUniversalTime(),  // Convert to UTC before saving
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Ensure it's UTC on reading
                    );

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(e => e.Creator)
                    .WithMany()
                    .HasForeignKey(e => e.CreatorId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Address)
                    .WithMany()
                    .HasForeignKey(e => e.AddressId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // User_Event_Participations Table
            modelBuilder.Entity<User_Event_Participations>(entity =>
            {
                entity.HasKey(uep => uep.Id);

                entity.HasOne(uep => uep.User)
                    .WithMany()
                    .HasForeignKey(uep => uep.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(uep => uep.Event)
                    .WithMany()
                    .HasForeignKey(uep => uep.EventId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Addresses Table
            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.HasOne(a => a.District)
                    .WithMany()
                    .HasForeignKey(a => a.DistrictId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Users_Interactions Relationships
            modelBuilder.Entity<Users_Interactions>()
                .HasOne(ui => ui.InitiatorUser)
                .WithMany()
                .HasForeignKey(ui => ui.InitiatorUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users_Interactions>()
                .HasOne(ui => ui.TargetUser)
                .WithMany()
                .HasForeignKey(ui => ui.TargetUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users_Interactions>()
                .HasOne(ui => ui.Interaction)
                .WithMany()
                .HasForeignKey(ui => ui.InteractionId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Users_Recipes_Interaction>()
               .HasOne(uri => uri.User)
               .WithMany()
               .HasForeignKey(uri => uri.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users_Recipes_Interaction>()
                .HasOne(uri => uri.Recipe)
                .WithMany()
                .HasForeignKey(uri => uri.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users_Recipes_Interaction>()
                .HasOne(uri => uri.Interaction)
                .WithMany()
                .HasForeignKey(uri => uri.InteractionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users_Blogs_Interaction>()
                .HasOne(ubi => ubi.User)
                .WithMany()
                .HasForeignKey(ubi => ubi.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users_Blogs_Interaction>()
                .HasOne(ubi => ubi.Blog)
                .WithMany()
                .HasForeignKey(ubi => ubi.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users_Blogs_Interaction>()
                .HasOne(ubi => ubi.Interaction)
                .WithMany()
                .HasForeignKey(ubi => ubi.InteractionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recipe_Bookmarks>()
               .HasOne(rb => rb.Recipe)
               .WithMany()
               .HasForeignKey(rb => rb.RecipeId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recipe_Comments>()
               .HasOne(rb => rb.Recipe)
               .WithMany()
               .HasForeignKey(rb => rb.RecipeId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recipe_Likes>()
               .HasOne(rb => rb.Recipe)
               .WithMany()
               .HasForeignKey(rb => rb.RecipeId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Blog_Bookmarks>()
              .HasOne(rb => rb.Blog)
              .WithMany()
              .HasForeignKey(rb => rb.BlogId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Blog_Comments>()
               .HasOne(rb => rb.Blog)
               .WithMany()
               .HasForeignKey(rb => rb.BlogId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Blog_Likes>()
               .HasOne(rb => rb.Blog)
               .WithMany()
               .HasForeignKey(rb => rb.BlogId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event_Bookmarks>()
               .HasOne(rb => rb.Event)
               .WithMany()
               .HasForeignKey(rb => rb.EventId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event_Comments>()
               .HasOne(rb => rb.Event)
               .WithMany()
               .HasForeignKey(rb => rb.EventId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event_Likes>()
               .HasOne(rb => rb.Event)
               .WithMany()
               .HasForeignKey(rb => rb.EventId)
               .OnDelete(DeleteBehavior.Cascade);

            // Configure User_Allergens
            modelBuilder.Entity<User_Allergens>()
                .HasOne(ua => ua.User)
                .WithMany()
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<User_Allergens>()
                .HasOne(ua => ua.Preference)
                .WithMany()
                .HasForeignKey(ua => ua.PreferenceId)
                .OnDelete(DeleteBehavior.Cascade); 



            //***  HERE DEFAULT ENTITIES ***//

            // USERS
            var usersSeeding = new UserDefaultData();
            usersSeeding.PopulateRolesData(modelBuilder);
            usersSeeding.PopulateUsersData(modelBuilder);

            // BLOGS
            var blogSeedingData = new BlogDefaultData();
            blogSeedingData.PopulateBlogData(modelBuilder);

            // COUNTRY,CITY, DISTRICT
            var addressSeeding = new AddresDefaultData();
            addressSeeding.PopulateCountryData(modelBuilder);
            addressSeeding.PopulateCitytData(modelBuilder);
            addressSeeding.PopulateDistrictData(modelBuilder);

            // INGREDIENT TYPES, INGREDIENTS
            var ingredinetSeeding = new IngredientsDefaultData();
            ingredinetSeeding.PopulateIngredientTypesData(modelBuilder);
            ingredinetSeeding.PopulateIngredientsData(modelBuilder);

            //ING_PREFERENCES, PREFERENCES
            var preferencesSeeding = new PreferencesDefaultData();
            preferencesSeeding.PopulatePreferencesData(modelBuilder);
            preferencesSeeding.PopulateIngredientPreferencesData(modelBuilder);

            // EVENTS
            var eventSeeding = new EventDefaultData();
            eventSeeding.PopulateAddressData(modelBuilder);
            eventSeeding.PopulateEventData(modelBuilder);

        }
    }
}
