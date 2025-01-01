using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.InteractionModels;

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
        public DbSet<User_Allergens> User_Allergens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TABLE RELATIONSHIP CONFIGURATIONS

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
                    .OnDelete(DeleteBehavior.Restrict);

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
                    .OnDelete(DeleteBehavior.Restrict);

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

            modelBuilder.Entity<Recipe_Bookmarks>()
               .HasOne(rb => rb.Recipe)
               .WithMany()
               .HasForeignKey(rb => rb.RecipeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recipe_Comments>()
               .HasOne(rb => rb.Recipe)
               .WithMany()
               .HasForeignKey(rb => rb.RecipeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recipe_Likes>()
               .HasOne(rb => rb.Recipe)
               .WithMany()
               .HasForeignKey(rb => rb.RecipeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Blog_Bookmarks>()
              .HasOne(rb => rb.Blog)
              .WithMany()
              .HasForeignKey(rb => rb.BlogId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Blog_Comments>()
               .HasOne(rb => rb.Blog)
               .WithMany()
               .HasForeignKey(rb => rb.BlogId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Blog_Likes>()
               .HasOne(rb => rb.Blog)
               .WithMany()
               .HasForeignKey(rb => rb.BlogId)
               .OnDelete(DeleteBehavior.Restrict);

            // Configure User_Allergens
            modelBuilder.Entity<User_Allergens>()
                .HasOne(ua => ua.User)
                .WithMany()
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User_Allergens>()
                .HasOne(ua => ua.Preference)
                .WithMany()
                .HasForeignKey(ua => ua.PreferenceId)
                .OnDelete(DeleteBehavior.Restrict);



            //***  HERE DEFAULT ENTITIES ***//

            // USERS


            // COUNTRY
            modelBuilder.Entity<Countries>().HasData(
                new Countries { Id = 1, Name = "Turkey" }
            );

            // CITY
            modelBuilder.Entity<Cities>().HasData(
                new Cities { Id = 1, Name = "ADANA", CountryId = 1 },
                new Cities { Id = 2, Name = "ADIYAMAN", CountryId = 1 },
                new Cities { Id = 3, Name = "AFYONKARAHİSAR", CountryId = 1 },
                new Cities { Id = 4, Name = "AĞRI", CountryId = 1 },
                new Cities { Id = 5, Name = "AMASYA", CountryId = 1 },
                new Cities { Id = 6, Name = "ANKARA", CountryId = 1 },
                new Cities { Id = 7, Name = "ANTALYA", CountryId = 1 },
                new Cities { Id = 8, Name = "ARTVİN", CountryId = 1 },
                new Cities { Id = 9, Name = "AYDIN", CountryId = 1 },
                new Cities { Id = 10, Name = "BALIKESİR", CountryId = 1 },
                new Cities { Id = 11, Name = "BİLECİK", CountryId = 1 },
                new Cities { Id = 12, Name = "BİNGÖL", CountryId = 1 },
                new Cities { Id = 13, Name = "BİTLİS", CountryId = 1 },
                new Cities { Id = 14, Name = "BOLU", CountryId = 1 },
                new Cities { Id = 15, Name = "BURDUR", CountryId = 1 },
                new Cities { Id = 16, Name = "BURSA", CountryId = 1 },
                new Cities { Id = 17, Name = "ÇANAKKALE", CountryId = 1 },
                new Cities { Id = 18, Name = "ÇANKIRI", CountryId = 1 },
                new Cities { Id = 19, Name = "ÇORUM", CountryId = 1 },
                new Cities { Id = 20, Name = "DENİZLİ", CountryId = 1 },
                new Cities { Id = 21, Name = "DİYARBAKIR", CountryId = 1 },
                new Cities { Id = 22, Name = "EDİRNE", CountryId = 1 },
                new Cities { Id = 23, Name = "ELAZIĞ", CountryId = 1 },
                new Cities { Id = 24, Name = "ERZİNCAN", CountryId = 1 },
                new Cities { Id = 25, Name = "ERZURUM", CountryId = 1 },
                new Cities { Id = 26, Name = "ESKİŞEHİR", CountryId = 1 },
                new Cities { Id = 27, Name = "GAZİANTEP", CountryId = 1 },
                new Cities { Id = 28, Name = "GİRESUN", CountryId = 1 },
                new Cities { Id = 29, Name = "GÜMÜŞHANE", CountryId = 1 },
                new Cities { Id = 30, Name = "HAKKARİ", CountryId = 1 },
                new Cities { Id = 31, Name = "HATAY", CountryId = 1 },
                new Cities { Id = 32, Name = "ISPARTA", CountryId = 1 },
                new Cities { Id = 33, Name = "MERSİN", CountryId = 1 },
                new Cities { Id = 34, Name = "İSTANBUL", CountryId = 1 },
                new Cities { Id = 35, Name = "İZMİR", CountryId = 1 },
                new Cities { Id = 36, Name = "KARS", CountryId = 1 },
                new Cities { Id = 37, Name = "KASTAMONU", CountryId = 1 },
                new Cities { Id = 38, Name = "KAYSERİ", CountryId = 1 },
                new Cities { Id = 39, Name = "KIRKLARELİ", CountryId = 1 },
                new Cities { Id = 40, Name = "KIRŞEHİR", CountryId = 1 },
                new Cities { Id = 41, Name = "KOCAELİ", CountryId = 1 },
                new Cities { Id = 42, Name = "KONYA", CountryId = 1 },
                new Cities { Id = 43, Name = "KÜTAHYA", CountryId = 1 },
                new Cities { Id = 44, Name = "MALATYA", CountryId = 1 },
                new Cities { Id = 45, Name = "MANİSA", CountryId = 1 },
                new Cities { Id = 46, Name = "KAHRAMANMARAŞ", CountryId = 1 },
                new Cities { Id = 47, Name = "MARDİN", CountryId = 1 },
                new Cities { Id = 48, Name = "MUĞLA", CountryId = 1 },
                new Cities { Id = 49, Name = "MUŞ", CountryId = 1 },
                new Cities { Id = 50, Name = "NEVŞEHİR", CountryId = 1 },
                new Cities { Id = 51, Name = "NİĞDE", CountryId = 1 },
                new Cities { Id = 52, Name = "ORDU", CountryId = 1 },
                new Cities { Id = 53, Name = "RİZE", CountryId = 1 },
                new Cities { Id = 54, Name = "SAKARYA", CountryId = 1 },
                new Cities { Id = 55, Name = "SAMSUN", CountryId = 1 },
                new Cities { Id = 56, Name = "SİİRT", CountryId = 1 },
                new Cities { Id = 57, Name = "SİNOP", CountryId = 1 },
                new Cities { Id = 58, Name = "SİVAS", CountryId = 1 },
                new Cities { Id = 59, Name = "TEKİRDAĞ", CountryId = 1 },
                new Cities { Id = 60, Name = "TOKAT", CountryId = 1 },
                new Cities { Id = 61, Name = "TRABZON", CountryId = 1 },
                new Cities { Id = 62, Name = "TUNCELİ", CountryId = 1 },
                new Cities { Id = 63, Name = "ŞANLIURFA", CountryId = 1 },
                new Cities { Id = 64, Name = "UŞAK", CountryId = 1 },
                new Cities { Id = 65, Name = "VAN", CountryId = 1 },
                new Cities { Id = 66, Name = "YOZGAT", CountryId = 1 },
                new Cities { Id = 67, Name = "ZONGULDAK", CountryId = 1 },
                new Cities { Id = 68, Name = "AKSARAY", CountryId = 1 },
                new Cities { Id = 69, Name = "BAYBURT", CountryId = 1 },
                new Cities { Id = 70, Name = "KARAMAN", CountryId = 1 },
                new Cities { Id = 71, Name = "KIRIKKALE", CountryId = 1 },
                new Cities { Id = 72, Name = "BATMAN", CountryId = 1 },
                new Cities { Id = 73, Name = "ŞIRNAK", CountryId = 1 },
                new Cities { Id = 74, Name = "BARTIN", CountryId = 1 },
                new Cities { Id = 75, Name = "ARDAHAN", CountryId = 1 },
                new Cities { Id = 76, Name = "IĞDIR", CountryId = 1 },
                new Cities { Id = 77, Name = "YALOVA", CountryId = 1 },
                new Cities { Id = 78, Name = "KARABÜK", CountryId = 1 },
                new Cities { Id = 79, Name = "KİLİS", CountryId = 1 },
                new Cities { Id = 80, Name = "OSMANİYE", CountryId = 1 },
                new Cities { Id = 81, Name = "DÜZCE", CountryId = 1 }
            );

            // DISTRICT
            modelBuilder.Entity<Districts>().HasData(
                new Districts { Id = 1, Name = "ALADAĞ", CityId = 1, PostCode = 1757 },
                new Districts { Id = 2, Name = "CEYHAN", CityId = 1, PostCode = 1219 },
                new Districts { Id = 3, Name = "ÇUKUROVA", CityId = 1, PostCode = 2033 },
                new Districts { Id = 4, Name = "FEKE", CityId = 1, PostCode = 1329 },
                new Districts { Id = 5, Name = "İMAMOĞLU", CityId = 1, PostCode = 1806 },
                new Districts { Id = 6, Name = "KARAİSALI", CityId = 1, PostCode = 1437 },
                new Districts { Id = 7, Name = "KARATAŞ", CityId = 1, PostCode = 1443 },
                new Districts { Id = 8, Name = "KOZAN", CityId = 1, PostCode = 1486 },
                new Districts { Id = 9, Name = "POZANTI", CityId = 1, PostCode = 1580 },
                new Districts { Id = 10, Name = "SAİMBEYLİ", CityId = 1, PostCode = 1588 },
                new Districts { Id = 11, Name = "SARIÇAM", CityId = 1, PostCode = 2032 },
                new Districts { Id = 12, Name = "SEYHAN", CityId = 1, PostCode = 1104 },
                new Districts { Id = 13, Name = "TUFANBEYLİ", CityId = 1, PostCode = 1687 },
                new Districts { Id = 14, Name = "YUMURTALIK", CityId = 1, PostCode = 1734 },
                new Districts { Id = 15, Name = "YÜREĞİR", CityId = 1, PostCode = 1748 },
                new Districts { Id = 16, Name = "BESNİ", CityId = 2, PostCode = 1182 },
                new Districts { Id = 17, Name = "ÇELİKHAN", CityId = 2, PostCode = 1246 },
                new Districts { Id = 18, Name = "GERGER", CityId = 2, PostCode = 1347 },
                new Districts { Id = 19, Name = "GÖLBAŞI", CityId = 2, PostCode = 1354 },
                new Districts { Id = 20, Name = "KAHTA", CityId = 2, PostCode = 1425 },
                new Districts { Id = 21, Name = "SAMSAT", CityId = 2, PostCode = 1592 },
                new Districts { Id = 22, Name = "SİNCİK", CityId = 2, PostCode = 1985 },
                new Districts { Id = 23, Name = "TUT", CityId = 2, PostCode = 1989 },
                new Districts { Id = 24, Name = "BAŞMAKÇI", CityId = 3, PostCode = 1771 },
                new Districts { Id = 25, Name = "BAYAT", CityId = 3, PostCode = 1773 },
                new Districts { Id = 26, Name = "BOLVADİN", CityId = 3, PostCode = 1200 },
                new Districts { Id = 27, Name = "ÇAY", CityId = 3, PostCode = 1239 },
                new Districts { Id = 28, Name = "ÇOBANLAR", CityId = 3, PostCode = 1906 },
                new Districts { Id = 29, Name = "DAZKIRI", CityId = 3, PostCode = 1267 },
                new Districts { Id = 30, Name = "DİNAR", CityId = 3, PostCode = 1281 },
                new Districts { Id = 31, Name = "EMİRDAĞ", CityId = 3, PostCode = 1306 },
                new Districts { Id = 32, Name = "EVCİLER", CityId = 3, PostCode = 1923 },
                new Districts { Id = 33, Name = "HOCALAR", CityId = 3, PostCode = 1944 },
                new Districts { Id = 34, Name = "İHSANİYE", CityId = 3, PostCode = 1404 },
                new Districts { Id = 35, Name = "İSCEHİSAR", CityId = 3, PostCode = 1809 },
                new Districts { Id = 36, Name = "KIZILÖREN", CityId = 3, PostCode = 1961 },
                new Districts { Id = 37, Name = "SANDIKLI", CityId = 3, PostCode = 1594 },
                new Districts { Id = 38, Name = "SİNANPAŞA", CityId = 3, PostCode = 1626 },
                new Districts { Id = 39, Name = "SULTANDAĞI", CityId = 3, PostCode = 1639 },
                new Districts { Id = 40, Name = "ŞUHUT", CityId = 3, PostCode = 1664 },
                new Districts { Id = 41, Name = "DİYADİN", CityId = 4, PostCode = 1283 },
                new Districts { Id = 42, Name = "DOĞUBAYAZIT", CityId = 4, PostCode = 1287 },
                new Districts { Id = 43, Name = "ELEŞKİRT", CityId = 4, PostCode = 1301 },
                new Districts { Id = 44, Name = "HAMUR", CityId = 4, PostCode = 1379 },
                new Districts { Id = 45, Name = "PATNOS", CityId = 4, PostCode = 1568 },
                new Districts { Id = 46, Name = "TAŞLIÇAY", CityId = 4, PostCode = 1667 },
                new Districts { Id = 47, Name = "TUTAK", CityId = 4, PostCode = 1691 },
                new Districts { Id = 48, Name = "GÖYNÜCEK", CityId = 5, PostCode = 1363 },
                new Districts { Id = 49, Name = "GÜMÜŞHACIKÖY", CityId = 5, PostCode = 1368 },
                new Districts { Id = 50, Name = "HAMAMÖZÜ", CityId = 5, PostCode = 1938 },
                new Districts { Id = 51, Name = "MERZİFON", CityId = 5, PostCode = 1524 },
                new Districts { Id = 52, Name = "SULUOVA", CityId = 5, PostCode = 1641 },
                new Districts { Id = 53, Name = "TAŞOVA", CityId = 5, PostCode = 1668 },
                new Districts { Id = 54, Name = "AKYURT", CityId = 6, PostCode = 1872 },
                new Districts { Id = 55, Name = "ALTINDAĞ", CityId = 6, PostCode = 1130 },
                new Districts { Id = 56, Name = "AYAŞ", CityId = 6, PostCode = 1157 },
                new Districts { Id = 57, Name = "BALA", CityId = 6, PostCode = 1167 },
                new Districts { Id = 58, Name = "BEYPAZARI", CityId = 6, PostCode = 1187 },
                new Districts { Id = 59, Name = "ÇAMLIDERE", CityId = 6, PostCode = 1227 },
                new Districts { Id = 60, Name = "ÇANKAYA", CityId = 6, PostCode = 1231 },
                new Districts { Id = 61, Name = "ÇUBUK", CityId = 6, PostCode = 1260 },
                new Districts { Id = 62, Name = "ELMADAĞ", CityId = 6, PostCode = 1302 },
                new Districts { Id = 63, Name = "ETİMESGUT", CityId = 6, PostCode = 1922 },
                new Districts { Id = 64, Name = "EVREN", CityId = 6, PostCode = 1924 },
                new Districts { Id = 65, Name = "GÖLBAŞI", CityId = 6, PostCode = 1744 },
                new Districts { Id = 66, Name = "GÜDÜL", CityId = 6, PostCode = 1365 },
                new Districts { Id = 67, Name = "HAYMANA", CityId = 6, PostCode = 1387 },
                new Districts { Id = 68, Name = "KAHRAMANKAZAN", CityId = 6, PostCode = 1815 },
                new Districts { Id = 69, Name = "KALECİK", CityId = 6, PostCode = 1427 },
                new Districts { Id = 70, Name = "KEÇİÖREN", CityId = 6, PostCode = 1745 },
                new Districts { Id = 71, Name = "KIZILCAHAMAM", CityId = 6, PostCode = 1473 },
                new Districts { Id = 72, Name = "MAMAK", CityId = 6, PostCode = 1746 },
                new Districts { Id = 73, Name = "NALLIHAN", CityId = 6, PostCode = 1539 },
                new Districts { Id = 74, Name = "POLATLI", CityId = 6, PostCode = 1578 },
                new Districts { Id = 75, Name = "PURSAKLAR", CityId = 6, PostCode = 2034 },
                new Districts { Id = 76, Name = "SİNCAN", CityId = 6, PostCode = 1747 },
                new Districts { Id = 77, Name = "ŞEREFLİKOÇHİSAR", CityId = 6, PostCode = 1658 },
                new Districts { Id = 78, Name = "YENİMAHALLE", CityId = 6, PostCode = 1723 },
                new Districts { Id = 79, Name = "AKSEKİ", CityId = 7, PostCode = 1121 },
                new Districts { Id = 80, Name = "AKSU", CityId = 7, PostCode = 2035 },
                new Districts { Id = 81, Name = "ALANYA", CityId = 7, PostCode = 1126 },
                new Districts { Id = 82, Name = "DEMRE", CityId = 7, PostCode = 1811 },
                new Districts { Id = 83, Name = "DÖŞEMEALTI", CityId = 7, PostCode = 2036 },
                new Districts { Id = 84, Name = "ELMALI", CityId = 7, PostCode = 1303 },
                new Districts { Id = 85, Name = "FİNİKE", CityId = 7, PostCode = 1333 },
                new Districts { Id = 86, Name = "GAZİPAŞA", CityId = 7, PostCode = 1337 },
                new Districts { Id = 87, Name = "GÜNDOĞMUŞ", CityId = 7, PostCode = 1370 },
                new Districts { Id = 88, Name = "İBRADI", CityId = 7, PostCode = 1946 },
                new Districts { Id = 89, Name = "KAŞ", CityId = 7, PostCode = 1451 },
                new Districts { Id = 90, Name = "KEMER", CityId = 7, PostCode = 1959 },
                new Districts { Id = 91, Name = "KEPEZ", CityId = 7, PostCode = 2037 },
                new Districts { Id = 92, Name = "KONYAALTI", CityId = 7, PostCode = 2038 },
                new Districts { Id = 93, Name = "KORKUTELİ", CityId = 7, PostCode = 1483 },
                new Districts { Id = 94, Name = "KUMLUCA", CityId = 7, PostCode = 1492 },
                new Districts { Id = 95, Name = "MANAVGAT", CityId = 7, PostCode = 1512 },
                new Districts { Id = 96, Name = "MURATPAŞA", CityId = 7, PostCode = 2039 },
                new Districts { Id = 97, Name = "SERİK", CityId = 7, PostCode = 1616 },
                new Districts { Id = 98, Name = "ARDANUÇ", CityId = 8, PostCode = 1145 },
                new Districts { Id = 99, Name = "ARHAVİ", CityId = 8, PostCode = 1147 },
                new Districts { Id = 100, Name = "BORÇKA", CityId = 8, PostCode = 1202 },
                new Districts { Id = 101, Name = "HOPA", CityId = 8, PostCode = 1395 },
                new Districts { Id = 102, Name = "KEMALPAŞA", CityId = 8, PostCode = 2105 },
                new Districts { Id = 103, Name = "MURGUL", CityId = 8, PostCode = 1828 },
                new Districts { Id = 104, Name = "ŞAVŞAT", CityId = 8, PostCode = 1653 },
                new Districts { Id = 105, Name = "YUSUFELİ", CityId = 8, PostCode = 1736 },
                new Districts { Id = 106, Name = "BOZDOĞAN", CityId = 9, PostCode = 1206 },
                new Districts { Id = 107, Name = "BUHARKENT", CityId = 9, PostCode = 1781 },
                new Districts { Id = 108, Name = "ÇİNE", CityId = 9, PostCode = 1256 },
                new Districts { Id = 109, Name = "DİDİM", CityId = 9, PostCode = 2000 },
                new Districts { Id = 110, Name = "EFELER", CityId = 9, PostCode = 2076 },
                new Districts { Id = 111, Name = "GERMENCİK", CityId = 9, PostCode = 1348 },
                new Districts { Id = 112, Name = "İNCİRLİOVA", CityId = 9, PostCode = 1807 },
                new Districts { Id = 113, Name = "KARACASU", CityId = 9, PostCode = 1435 },
                new Districts { Id = 114, Name = "KARPUZLU", CityId = 9, PostCode = 1957 },
                new Districts { Id = 115, Name = "KOÇARLI", CityId = 9, PostCode = 1479 },
                new Districts { Id = 116, Name = "KÖŞK", CityId = 9, PostCode = 1968 },
                new Districts { Id = 117, Name = "KUŞADASI", CityId = 9, PostCode = 1497 },
                new Districts { Id = 118, Name = "KUYUCAK", CityId = 9, PostCode = 1498 },
                new Districts { Id = 119, Name = "NAZİLLİ", CityId = 9, PostCode = 1542 },
                new Districts { Id = 120, Name = "SÖKE", CityId = 9, PostCode = 1637 },
                new Districts { Id = 121, Name = "SULTANHİSAR", CityId = 9, PostCode = 1640 },
                new Districts { Id = 122, Name = "YENİPAZAR", CityId = 9, PostCode = 1724 },
                new Districts { Id = 123, Name = "ALTIEYLÜL", CityId = 10, PostCode = 2077 },
                new Districts { Id = 124, Name = "AYVALIK", CityId = 10, PostCode = 1161 },
                new Districts { Id = 125, Name = "BALYA", CityId = 10, PostCode = 1169 },
                new Districts { Id = 126, Name = "BANDIRMA", CityId = 10, PostCode = 1171 },
                new Districts { Id = 127, Name = "BİGADİÇ", CityId = 10, PostCode = 1191 },
                new Districts { Id = 128, Name = "BURHANİYE", CityId = 10, PostCode = 1216 },
                new Districts { Id = 129, Name = "DURSUNBEY", CityId = 10, PostCode = 1291 },
                new Districts { Id = 130, Name = "EDREMİT", CityId = 10, PostCode = 1294 },
                new Districts { Id = 131, Name = "ERDEK", CityId = 10, PostCode = 1310 },
                new Districts { Id = 132, Name = "GÖMEÇ", CityId = 10, PostCode = 1928 },
                new Districts { Id = 133, Name = "GÖNEN", CityId = 10, PostCode = 1360 },
                new Districts { Id = 134, Name = "HAVRAN", CityId = 10, PostCode = 1384 },
                new Districts { Id = 135, Name = "İVRİNDİ", CityId = 10, PostCode = 1418 },
                new Districts { Id = 136, Name = "KARESİ", CityId = 10, PostCode = 2078 },
                new Districts { Id = 137, Name = "KEPSUT", CityId = 10, PostCode = 1462 },
                new Districts { Id = 138, Name = "MANYAS", CityId = 10, PostCode = 1514 },
                new Districts { Id = 139, Name = "MARMARA", CityId = 10, PostCode = 1824 },
                new Districts { Id = 140, Name = "SAVAŞTEPE", CityId = 10, PostCode = 1608 },
                new Districts { Id = 141, Name = "SINDIRGI", CityId = 10, PostCode = 1619 },
                new Districts { Id = 142, Name = "SUSURLUK", CityId = 10, PostCode = 1644 },
                new Districts { Id = 143, Name = "BOZÜYÜK", CityId = 11, PostCode = 1210 },
                new Districts { Id = 144, Name = "GÖLPAZARI", CityId = 11, PostCode = 1359 },
                new Districts { Id = 145, Name = "İNHİSAR", CityId = 11, PostCode = 1948 },
                new Districts { Id = 146, Name = "OSMANELİ", CityId = 11, PostCode = 1559 },
                new Districts { Id = 147, Name = "PAZARYERİ", CityId = 11, PostCode = 1571 },
                new Districts { Id = 148, Name = "SÖĞÜT", CityId = 11, PostCode = 1636 },
                new Districts { Id = 149, Name = "YENİPAZAR", CityId = 11, PostCode = 1857 },
                new Districts { Id = 150, Name = "ADAKLI", CityId = 12, PostCode = 1750 },
                new Districts { Id = 151, Name = "GENÇ", CityId = 12, PostCode = 1344 },
                new Districts { Id = 152, Name = "KARLIOVA", CityId = 12, PostCode = 1446 },
                new Districts { Id = 153, Name = "KİĞI", CityId = 12, PostCode = 1475 },
                new Districts { Id = 154, Name = "SOLHAN", CityId = 12, PostCode = 1633 },
                new Districts { Id = 155, Name = "YAYLADERE", CityId = 12, PostCode = 1855 },
                new Districts { Id = 156, Name = "YEDİSU", CityId = 12, PostCode = 1996 },
                new Districts { Id = 157, Name = "ADİLCEVAZ", CityId = 13, PostCode = 1106 },
                new Districts { Id = 158, Name = "AHLAT", CityId = 13, PostCode = 1112 },
                new Districts { Id = 159, Name = "GÜROYMAK", CityId = 13, PostCode = 1798 },
                new Districts { Id = 160, Name = "HİZAN", CityId = 13, PostCode = 1394 },
                new Districts { Id = 161, Name = "MUTKİ", CityId = 13, PostCode = 1537 },
                new Districts { Id = 162, Name = "TATVAN", CityId = 13, PostCode = 1669 },
                new Districts { Id = 163, Name = "DÖRTDİVAN", CityId = 14, PostCode = 1916 },
                new Districts { Id = 164, Name = "GEREDE", CityId = 14, PostCode = 1346 },
                new Districts { Id = 165, Name = "GÖYNÜK", CityId = 14, PostCode = 1364 },
                new Districts { Id = 166, Name = "KIBRISCIK", CityId = 14, PostCode = 1466 },
                new Districts { Id = 167, Name = "MENGEN", CityId = 14, PostCode = 1522 },
                new Districts { Id = 168, Name = "MUDURNU", CityId = 14, PostCode = 1531 },
                new Districts { Id = 169, Name = "SEBEN", CityId = 14, PostCode = 1610 },
                new Districts { Id = 170, Name = "YENİÇAĞA", CityId = 14, PostCode = 1997 },
                new Districts { Id = 171, Name = "AĞLASUN", CityId = 15, PostCode = 1109 },
                new Districts { Id = 172, Name = "ALTINYAYLA", CityId = 15, PostCode = 1874 },
                new Districts { Id = 173, Name = "BUCAK", CityId = 15, PostCode = 1211 },
                new Districts { Id = 174, Name = "ÇAVDIR", CityId = 15, PostCode = 1899 },
                new Districts { Id = 175, Name = "ÇELTİKÇİ", CityId = 15, PostCode = 1903 },
                new Districts { Id = 176, Name = "GÖLHİSAR", CityId = 15, PostCode = 1357 },
                new Districts { Id = 177, Name = "KARAMANLI", CityId = 15, PostCode = 1813 },
                new Districts { Id = 178, Name = "KEMER", CityId = 15, PostCode = 1816 },
                new Districts { Id = 179, Name = "TEFENNİ", CityId = 15, PostCode = 1672 },
                new Districts { Id = 180, Name = "YEŞİLOVA", CityId = 15, PostCode = 1728 },
                new Districts { Id = 181, Name = "BÜYÜKORHAN", CityId = 16, PostCode = 1783 },
                new Districts { Id = 182, Name = "GEMLİK", CityId = 16, PostCode = 1343 },
                new Districts { Id = 183, Name = "GÜRSU", CityId = 16, PostCode = 1935 },
                new Districts { Id = 184, Name = "HARMANCIK", CityId = 16, PostCode = 1799 },
                new Districts { Id = 185, Name = "İNEGÖL", CityId = 16, PostCode = 1411 },
                new Districts { Id = 186, Name = "İZNİK", CityId = 16, PostCode = 1420 },
                new Districts { Id = 187, Name = "KARACABEY", CityId = 16, PostCode = 1434 },
                new Districts { Id = 188, Name = "KELES", CityId = 16, PostCode = 1457 },
                new Districts { Id = 189, Name = "KESTEL", CityId = 16, PostCode = 1960 },
                new Districts { Id = 190, Name = "MUDANYA", CityId = 16, PostCode = 1530 },
                new Districts { Id = 191, Name = "MUSTAFAKEMALPAŞA", CityId = 16, PostCode = 1535 },
                new Districts { Id = 192, Name = "NİLÜFER", CityId = 16, PostCode = 1829 },
                new Districts { Id = 193, Name = "ORHANELİ", CityId = 16, PostCode = 1553 },
                new Districts { Id = 194, Name = "ORHANGAZİ", CityId = 16, PostCode = 1554 },
                new Districts { Id = 195, Name = "OSMANGAZİ", CityId = 16, PostCode = 1832 },
                new Districts { Id = 196, Name = "YENİŞEHİR", CityId = 16, PostCode = 1725 },
                new Districts { Id = 197, Name = "YILDIRIM", CityId = 16, PostCode = 1859 },
                new Districts { Id = 198, Name = "AYVACIK", CityId = 17, PostCode = 1160 },
                new Districts { Id = 199, Name = "BAYRAMİÇ", CityId = 17, PostCode = 1180 },
                new Districts { Id = 200, Name = "BİGA", CityId = 17, PostCode = 1190 },
                new Districts { Id = 201, Name = "BOZCAADA", CityId = 17, PostCode = 1205 },
                new Districts { Id = 202, Name = "ÇAN", CityId = 17, PostCode = 1229 },
                new Districts { Id = 203, Name = "ECEABAT", CityId = 17, PostCode = 1293 },
                new Districts { Id = 204, Name = "EZİNE", CityId = 17, PostCode = 1326 },
                new Districts { Id = 205, Name = "GELİBOLU", CityId = 17, PostCode = 1340 },
                new Districts { Id = 206, Name = "GÖKÇEADA", CityId = 17, PostCode = 1408 },
                new Districts { Id = 207, Name = "LAPSEKİ", CityId = 17, PostCode = 1503 },
                new Districts { Id = 208, Name = "YENİCE", CityId = 17, PostCode = 1722 },
                new Districts { Id = 209, Name = "ATKARACALAR", CityId = 18, PostCode = 1765 },
                new Districts { Id = 210, Name = "BAYRAMÖREN", CityId = 18, PostCode = 1885 },
                new Districts { Id = 211, Name = "ÇERKEŞ", CityId = 18, PostCode = 1248 },
                new Districts { Id = 212, Name = "ELDİVAN", CityId = 18, PostCode = 1300 },
                new Districts { Id = 213, Name = "ILGAZ", CityId = 18, PostCode = 1399 },
                new Districts { Id = 214, Name = "KIZILIRMAK", CityId = 18, PostCode = 1817 },
                new Districts { Id = 215, Name = "KORGUN", CityId = 18, PostCode = 1963 },
                new Districts { Id = 216, Name = "KURŞUNLU", CityId = 18, PostCode = 1494 },
                new Districts { Id = 217, Name = "ORTA", CityId = 18, PostCode = 1555 },
                new Districts { Id = 218, Name = "ŞABANÖZÜ", CityId = 18, PostCode = 1649 },
                new Districts { Id = 219, Name = "YAPRAKLI", CityId = 18, PostCode = 1718 },
                new Districts { Id = 220, Name = "ALACA", CityId = 19, PostCode = 1124 },
                new Districts { Id = 221, Name = "BAYAT", CityId = 19, PostCode = 1177 },
                new Districts { Id = 222, Name = "BOĞAZKALE", CityId = 19, PostCode = 1778 },
                new Districts { Id = 223, Name = "DODURGA", CityId = 19, PostCode = 1911 },
                new Districts { Id = 224, Name = "İSKİLİP", CityId = 19, PostCode = 1414 },
                new Districts { Id = 225, Name = "KARGI", CityId = 19, PostCode = 1445 },
                new Districts { Id = 226, Name = "LAÇİN", CityId = 19, PostCode = 1972 },
                new Districts { Id = 227, Name = "MECİTÖZÜ", CityId = 19, PostCode = 1520 },
                new Districts { Id = 228, Name = "OĞUZLAR", CityId = 19, PostCode = 1976 },
                new Districts { Id = 229, Name = "ORTAKÖY", CityId = 19, PostCode = 1556 },
                new Districts { Id = 230, Name = "OSMANCIK", CityId = 19, PostCode = 1558 },
                new Districts { Id = 231, Name = "SUNGURLU", CityId = 19, PostCode = 1642 },
                new Districts { Id = 232, Name = "UĞURLUDAĞ", CityId = 19, PostCode = 1850 },
                new Districts { Id = 233, Name = "ACIPAYAM", CityId = 20, PostCode = 1102 },
                new Districts { Id = 234, Name = "BABADAĞ", CityId = 20, PostCode = 1769 },
                new Districts { Id = 235, Name = "BAKLAN", CityId = 20, PostCode = 1881 },
                new Districts { Id = 236, Name = "BEKİLLİ", CityId = 20, PostCode = 1774 },
                new Districts { Id = 237, Name = "BEYAĞAÇ", CityId = 20, PostCode = 1888 },
                new Districts { Id = 238, Name = "BOZKURT", CityId = 20, PostCode = 1889 },
                new Districts { Id = 239, Name = "BULDAN", CityId = 20, PostCode = 1214 },
                new Districts { Id = 240, Name = "ÇAL", CityId = 20, PostCode = 1224 },
                new Districts { Id = 241, Name = "ÇAMELİ", CityId = 20, PostCode = 1226 },
                new Districts { Id = 242, Name = "ÇARDAK", CityId = 20, PostCode = 1233 },
                new Districts { Id = 243, Name = "ÇİVRİL", CityId = 20, PostCode = 1257 },
                new Districts { Id = 244, Name = "GÜNEY", CityId = 20, PostCode = 1371 },
                new Districts { Id = 245, Name = "HONAZ", CityId = 20, PostCode = 1803 },
                new Districts { Id = 246, Name = "KALE", CityId = 20, PostCode = 1426 },
                new Districts { Id = 247, Name = "MERKEZEFENDİ", CityId = 20, PostCode = 2079 },
                new Districts { Id = 248, Name = "PAMUKKALE", CityId = 20, PostCode = 1871 },
                new Districts { Id = 249, Name = "SARAYKÖY", CityId = 20, PostCode = 1597 },
                new Districts { Id = 250, Name = "SERİNHİSAR", CityId = 20, PostCode = 1840 },
                new Districts { Id = 251, Name = "TAVAS", CityId = 20, PostCode = 1670 },
                new Districts { Id = 252, Name = "BAĞLAR", CityId = 21, PostCode = 2040 },
                new Districts { Id = 253, Name = "BİSMİL", CityId = 21, PostCode = 1195 },
                new Districts { Id = 254, Name = "ÇERMİK", CityId = 21, PostCode = 1249 },
                new Districts { Id = 255, Name = "ÇINAR", CityId = 21, PostCode = 1253 },
                new Districts { Id = 256, Name = "ÇÜNGÜŞ", CityId = 21, PostCode = 1263 },
                new Districts { Id = 257, Name = "DİCLE", CityId = 21, PostCode = 1278 },
                new Districts { Id = 258, Name = "EĞİL", CityId = 21, PostCode = 1791 },
                new Districts { Id = 259, Name = "ERGANİ", CityId = 21, PostCode = 1315 },
                new Districts { Id = 260, Name = "HANİ", CityId = 21, PostCode = 1381 },
                new Districts { Id = 261, Name = "HAZRO", CityId = 21, PostCode = 1389 },
                new Districts { Id = 262, Name = "KAYAPINAR", CityId = 21, PostCode = 2041 },
                new Districts { Id = 263, Name = "KOCAKÖY", CityId = 21, PostCode = 1962 },
                new Districts { Id = 264, Name = "KULP", CityId = 21, PostCode = 1490 },
                new Districts { Id = 265, Name = "LİCE", CityId = 21, PostCode = 1504 },
                new Districts { Id = 266, Name = "SİLVAN", CityId = 21, PostCode = 1624 },
                new Districts { Id = 267, Name = "SUR", CityId = 21, PostCode = 2042 },
                new Districts { Id = 268, Name = "YENİŞEHİR", CityId = 21, PostCode = 2043 },
                new Districts { Id = 269, Name = "ENEZ", CityId = 22, PostCode = 1307 },
                new Districts { Id = 270, Name = "HAVSA", CityId = 22, PostCode = 1385 },
                new Districts { Id = 271, Name = "İPSALA", CityId = 22, PostCode = 1412 },
                new Districts { Id = 272, Name = "KEŞAN", CityId = 22, PostCode = 1464 },
                new Districts { Id = 273, Name = "LALAPAŞA", CityId = 22, PostCode = 1502 },
                new Districts { Id = 274, Name = "MERİÇ", CityId = 22, PostCode = 1523 },
                new Districts { Id = 275, Name = "SÜLOĞLU", CityId = 22, PostCode = 1988 },
                new Districts { Id = 276, Name = "UZUNKÖPRÜ", CityId = 22, PostCode = 1705 },
                new Districts { Id = 277, Name = "AĞIN", CityId = 23, PostCode = 1110 },
                new Districts { Id = 278, Name = "ALACAKAYA", CityId = 23, PostCode = 1873 },
                new Districts { Id = 279, Name = "ARICAK", CityId = 23, PostCode = 1762 },
                new Districts { Id = 280, Name = "BASKİL", CityId = 23, PostCode = 1173 },
                new Districts { Id = 281, Name = "KARAKOÇAN", CityId = 23, PostCode = 1438 },
                new Districts { Id = 282, Name = "KEBAN", CityId = 23, PostCode = 1455 },
                new Districts { Id = 283, Name = "KOVANCILAR", CityId = 23, PostCode = 1820 },
                new Districts { Id = 284, Name = "MADEN", CityId = 23, PostCode = 1506 },
                new Districts { Id = 285, Name = "PALU", CityId = 23, PostCode = 1566 },
                new Districts { Id = 286, Name = "SİVRİCE", CityId = 23, PostCode = 1631 },
                new Districts { Id = 287, Name = "ÇAYIRLI", CityId = 24, PostCode = 1243 },
                new Districts { Id = 288, Name = "İLİÇ", CityId = 24, PostCode = 1406 },
                new Districts { Id = 289, Name = "KEMAH", CityId = 24, PostCode = 1459 },
                new Districts { Id = 290, Name = "KEMALİYE", CityId = 24, PostCode = 1460 },
                new Districts { Id = 291, Name = "OTLUKBELİ", CityId = 24, PostCode = 1977 },
                new Districts { Id = 292, Name = "REFAHİYE", CityId = 24, PostCode = 1583 },
                new Districts { Id = 293, Name = "TERCAN", CityId = 24, PostCode = 1675 },
                new Districts { Id = 294, Name = "ÜZÜMLÜ", CityId = 24, PostCode = 1853 },
                new Districts { Id = 295, Name = "AŞKALE", CityId = 25, PostCode = 1153 },
                new Districts { Id = 296, Name = "AZİZİYE", CityId = 25, PostCode = 1945 },
                new Districts { Id = 297, Name = "ÇAT", CityId = 25, PostCode = 1235 },
                new Districts { Id = 298, Name = "HINIS", CityId = 25, PostCode = 1392 },
                new Districts { Id = 299, Name = "HORASAN", CityId = 25, PostCode = 1396 },
                new Districts { Id = 300, Name = "İSPİR", CityId = 25, PostCode = 1416 },
                new Districts { Id = 301, Name = "KARAÇOBAN", CityId = 25, PostCode = 1812 },
                new Districts { Id = 302, Name = "KARAYAZI", CityId = 25, PostCode = 1444 },
                new Districts { Id = 303, Name = "KÖPRÜKÖY", CityId = 25, PostCode = 1967 },
                new Districts { Id = 304, Name = "NARMAN", CityId = 25, PostCode = 1540 },
                new Districts { Id = 305, Name = "OLTU", CityId = 25, PostCode = 1550 },
                new Districts { Id = 306, Name = "OLUR", CityId = 25, PostCode = 1551 },
                new Districts { Id = 307, Name = "PALANDÖKEN", CityId = 25, PostCode = 2044 },
                new Districts { Id = 308, Name = "PASİNLER", CityId = 25, PostCode = 1567 },
                new Districts { Id = 309, Name = "PAZARYOLU", CityId = 25, PostCode = 1865 },
                new Districts { Id = 310, Name = "ŞENKAYA", CityId = 25, PostCode = 1657 },
                new Districts { Id = 311, Name = "TEKMAN", CityId = 25, PostCode = 1674 },
                new Districts { Id = 312, Name = "TORTUM", CityId = 25, PostCode = 1683 },
                new Districts { Id = 313, Name = "UZUNDERE", CityId = 25, PostCode = 1851 },
                new Districts { Id = 314, Name = "YAKUTİYE", CityId = 25, PostCode = 2045 },
                new Districts { Id = 315, Name = "ALPU", CityId = 26, PostCode = 1759 },
                new Districts { Id = 316, Name = "BEYLİKOVA", CityId = 26, PostCode = 1777 },
                new Districts { Id = 317, Name = "ÇİFTELER", CityId = 26, PostCode = 1255 },
                new Districts { Id = 318, Name = "GÜNYÜZÜ", CityId = 26, PostCode = 1934 },
                new Districts { Id = 319, Name = "HAN", CityId = 26, PostCode = 1939 },
                new Districts { Id = 320, Name = "İNÖNÜ", CityId = 26, PostCode = 1808 },
                new Districts { Id = 321, Name = "MAHMUDİYE", CityId = 26, PostCode = 1508 },
                new Districts { Id = 322, Name = "MİHALGAZİ", CityId = 26, PostCode = 1973 },
                new Districts { Id = 323, Name = "MİHALIÇÇIK", CityId = 26, PostCode = 1527 },
                new Districts { Id = 324, Name = "ODUNPAZARI", CityId = 26, PostCode = 2046 },
                new Districts { Id = 325, Name = "SARICAKAYA", CityId = 26, PostCode = 1599 },
                new Districts { Id = 326, Name = "SEYİTGAZİ", CityId = 26, PostCode = 1618 },
                new Districts { Id = 327, Name = "SİVRİHİSAR", CityId = 26, PostCode = 1632 },
                new Districts { Id = 328, Name = "TEPEBAŞI", CityId = 26, PostCode = 2047 },
                new Districts { Id = 329, Name = "ARABAN", CityId = 27, PostCode = 1139 },
                new Districts { Id = 330, Name = "İSLAHİYE", CityId = 27, PostCode = 1415 },
                new Districts { Id = 331, Name = "KARKAMIŞ", CityId = 27, PostCode = 1956 },
                new Districts { Id = 332, Name = "NİZİP", CityId = 27, PostCode = 1546 },
                new Districts { Id = 333, Name = "NURDAĞI", CityId = 27, PostCode = 1974 },
                new Districts { Id = 334, Name = "OĞUZELİ", CityId = 27, PostCode = 1549 },
                new Districts { Id = 335, Name = "ŞAHİNBEY", CityId = 27, PostCode = 1841 },
                new Districts { Id = 336, Name = "ŞEHİTKAMİL", CityId = 27, PostCode = 1844 },
                new Districts { Id = 337, Name = "YAVUZELİ", CityId = 27, PostCode = 1720 },
                new Districts { Id = 338, Name = "ALUCRA", CityId = 28, PostCode = 1133 },
                new Districts { Id = 339, Name = "BULANCAK", CityId = 28, PostCode = 1212 },
                new Districts { Id = 340, Name = "ÇAMOLUK", CityId = 28, PostCode = 1893 },
                new Districts { Id = 341, Name = "ÇANAKÇI", CityId = 28, PostCode = 1894 },
                new Districts { Id = 342, Name = "DERELİ", CityId = 28, PostCode = 1272 },
                new Districts { Id = 343, Name = "DOĞANKENT", CityId = 28, PostCode = 1912 },
                new Districts { Id = 344, Name = "ESPİYE", CityId = 28, PostCode = 1320 },
                new Districts { Id = 345, Name = "EYNESİL", CityId = 28, PostCode = 1324 },
                new Districts { Id = 346, Name = "GÖRELE", CityId = 28, PostCode = 1361 },
                new Districts { Id = 347, Name = "GÜCE", CityId = 28, PostCode = 1930 },
                new Districts { Id = 348, Name = "KEŞAP", CityId = 28, PostCode = 1465 },
                new Districts { Id = 349, Name = "PİRAZİZ", CityId = 28, PostCode = 1837 },
                new Districts { Id = 350, Name = "ŞEBİNKARAHİSAR", CityId = 28, PostCode = 1654 },
                new Districts { Id = 351, Name = "TİREBOLU", CityId = 28, PostCode = 1678 },
                new Districts { Id = 352, Name = "YAĞLIDERE", CityId = 28, PostCode = 1854 },
                new Districts { Id = 353, Name = "KELKİT", CityId = 29, PostCode = 1458 },
                new Districts { Id = 354, Name = "KÖSE", CityId = 29, PostCode = 1822 },
                new Districts { Id = 355, Name = "KÜRTÜN", CityId = 29, PostCode = 1971 },
                new Districts { Id = 356, Name = "ŞİRAN", CityId = 29, PostCode = 1660 },
                new Districts { Id = 357, Name = "TORUL", CityId = 29, PostCode = 1684 },
                new Districts { Id = 358, Name = "ÇUKURCA", CityId = 30, PostCode = 1261 },
                new Districts { Id = 359, Name = "DERECİK", CityId = 30, PostCode = 2107 },
                new Districts { Id = 360, Name = "ŞEMDİNLİ", CityId = 30, PostCode = 1656 },
                new Districts { Id = 361, Name = "YÜKSEKOVA", CityId = 30, PostCode = 1737 },
                new Districts { Id = 362, Name = "ALTINÖZÜ", CityId = 31, PostCode = 1131 },
                new Districts { Id = 363, Name = "ANTAKYA", CityId = 31, PostCode = 2080 },
                new Districts { Id = 364, Name = "ARSUZ", CityId = 31, PostCode = 2081 },
                new Districts { Id = 365, Name = "BELEN", CityId = 31, PostCode = 1887 },
                new Districts { Id = 366, Name = "DEFNE", CityId = 31, PostCode = 2082 },
                new Districts { Id = 367, Name = "DÖRTYOL", CityId = 31, PostCode = 1289 },
                new Districts { Id = 368, Name = "ERZİN", CityId = 31, PostCode = 1792 },
                new Districts { Id = 369, Name = "HASSA", CityId = 31, PostCode = 1382 },
                new Districts { Id = 370, Name = "İSKENDERUN", CityId = 31, PostCode = 1413 },
                new Districts { Id = 371, Name = "KIRIKHAN", CityId = 31, PostCode = 1468 },
                new Districts { Id = 372, Name = "KUMLU", CityId = 31, PostCode = 1970 },
                new Districts { Id = 373, Name = "PAYAS", CityId = 31, PostCode = 2083 },
                new Districts { Id = 374, Name = "REYHANLI", CityId = 31, PostCode = 1585 },
                new Districts { Id = 375, Name = "SAMANDAĞ", CityId = 31, PostCode = 1591 },
                new Districts { Id = 376, Name = "YAYLADAĞI", CityId = 31, PostCode = 1721 },
                new Districts { Id = 377, Name = "AKSU", CityId = 32, PostCode = 1755 },
                new Districts { Id = 378, Name = "ATABEY", CityId = 32, PostCode = 1154 },
                new Districts { Id = 379, Name = "EĞİRDİR", CityId = 32, PostCode = 1297 },
                new Districts { Id = 380, Name = "GELENDOST", CityId = 32, PostCode = 1341 },
                new Districts { Id = 381, Name = "GÖNEN", CityId = 32, PostCode = 1929 },
                new Districts { Id = 382, Name = "KEÇİBORLU", CityId = 32, PostCode = 1456 },
                new Districts { Id = 383, Name = "SENİRKENT", CityId = 32, PostCode = 1615 },
                new Districts { Id = 384, Name = "SÜTÇÜLER", CityId = 32, PostCode = 1648 },
                new Districts { Id = 385, Name = "ŞARKİKARAAĞAÇ", CityId = 32, PostCode = 1651 },
                new Districts { Id = 386, Name = "ULUBORLU", CityId = 32, PostCode = 1699 },
                new Districts { Id = 387, Name = "YALVAÇ", CityId = 32, PostCode = 1717 },
                new Districts { Id = 388, Name = "YENİŞARBADEMLİ", CityId = 32, PostCode = 2001 },
                new Districts { Id = 389, Name = "AKDENİZ", CityId = 33, PostCode = 2064 },
                new Districts { Id = 390, Name = "ANAMUR", CityId = 33, PostCode = 1135 },
                new Districts { Id = 391, Name = "AYDINCIK", CityId = 33, PostCode = 1766 },
                new Districts { Id = 392, Name = "BOZYAZI", CityId = 33, PostCode = 1779 },
                new Districts { Id = 393, Name = "ÇAMLIYAYLA", CityId = 33, PostCode = 1892 },
                new Districts { Id = 394, Name = "ERDEMLİ", CityId = 33, PostCode = 1311 },
                new Districts { Id = 395, Name = "GÜLNAR", CityId = 33, PostCode = 1366 },
                new Districts { Id = 396, Name = "MEZİTLİ", CityId = 33, PostCode = 2065 },
                new Districts { Id = 397, Name = "MUT", CityId = 33, PostCode = 1536 },
                new Districts { Id = 398, Name = "SİLİFKE", CityId = 33, PostCode = 1621 },
                new Districts { Id = 399, Name = "TARSUS", CityId = 33, PostCode = 1665 },
                new Districts { Id = 400, Name = "TOROSLAR", CityId = 33, PostCode = 2066 },
                new Districts { Id = 401, Name = "YENİŞEHİR", CityId = 33, PostCode = 2067 },
                new Districts { Id = 402, Name = "ADALAR", CityId = 34, PostCode = 1103 },
                new Districts { Id = 403, Name = "ARNAVUTKÖY", CityId = 34, PostCode = 2048 },
                new Districts { Id = 404, Name = "ATAŞEHİR", CityId = 34, PostCode = 2049 },
                new Districts { Id = 405, Name = "AVCILAR", CityId = 34, PostCode = 2003 },
                new Districts { Id = 406, Name = "BAĞCILAR", CityId = 34, PostCode = 2004 },
                new Districts { Id = 407, Name = "BAHÇELİEVLER", CityId = 34, PostCode = 2005 },
                new Districts { Id = 408, Name = "BAKIRKÖY", CityId = 34, PostCode = 1166 },
                new Districts { Id = 409, Name = "BAŞAKŞEHİR", CityId = 34, PostCode = 2050 },
                new Districts { Id = 410, Name = "BAYRAMPAŞA", CityId = 34, PostCode = 1886 },
                new Districts { Id = 411, Name = "BEŞİKTAŞ", CityId = 34, PostCode = 1183 },
                new Districts { Id = 412, Name = "BEYKOZ", CityId = 34, PostCode = 1185 },
                new Districts { Id = 413, Name = "BEYLİKDÜZÜ", CityId = 34, PostCode = 2051 },
                new Districts { Id = 414, Name = "BEYOĞLU", CityId = 34, PostCode = 1186 },
                new Districts { Id = 415, Name = "BÜYÜKÇEKMECE", CityId = 34, PostCode = 1782 },
                new Districts { Id = 416, Name = "ÇATALCA", CityId = 34, PostCode = 1237 },
                new Districts { Id = 417, Name = "ÇEKMEKÖY", CityId = 34, PostCode = 2052 },
                new Districts { Id = 418, Name = "ESENLER", CityId = 34, PostCode = 2016 },
                new Districts { Id = 419, Name = "ESENYURT", CityId = 34, PostCode = 2053 },
                new Districts { Id = 420, Name = "EYÜPSULTAN", CityId = 34, PostCode = 1325 },
                new Districts { Id = 421, Name = "FATİH", CityId = 34, PostCode = 1327 },
                new Districts { Id = 422, Name = "GAZİOSMANPAŞA", CityId = 34, PostCode = 1336 },
                new Districts { Id = 423, Name = "GÜNGÖREN", CityId = 34, PostCode = 2010 },
                new Districts { Id = 424, Name = "KADIKÖY", CityId = 34, PostCode = 1421 },
                new Districts { Id = 425, Name = "KAĞITHANE", CityId = 34, PostCode = 1810 },
                new Districts { Id = 426, Name = "KARTAL", CityId = 34, PostCode = 1449 },
                new Districts { Id = 427, Name = "KÜÇÜKÇEKMECE", CityId = 34, PostCode = 1823 },
                new Districts { Id = 428, Name = "MALTEPE", CityId = 34, PostCode = 2012 },
                new Districts { Id = 429, Name = "PENDİK", CityId = 34, PostCode = 1835 },
                new Districts { Id = 430, Name = "SANCAKTEPE", CityId = 34, PostCode = 2054 },
                new Districts { Id = 431, Name = "SARIYER", CityId = 34, PostCode = 1604 },
                new Districts { Id = 432, Name = "SİLİVRİ", CityId = 34, PostCode = 1622 },
                new Districts { Id = 433, Name = "SULTANBEYLİ", CityId = 34, PostCode = 2014 },
                new Districts { Id = 434, Name = "SULTANGAZİ", CityId = 34, PostCode = 2055 },
                new Districts { Id = 435, Name = "ŞİLE", CityId = 34, PostCode = 1659 },
                new Districts { Id = 436, Name = "ŞİŞLİ", CityId = 34, PostCode = 1663 },
                new Districts { Id = 437, Name = "TUZLA", CityId = 34, PostCode = 2015 },
                new Districts { Id = 438, Name = "ÜMRANİYE", CityId = 34, PostCode = 1852 },
                new Districts { Id = 439, Name = "ÜSKÜDAR", CityId = 34, PostCode = 1708 },
                new Districts { Id = 440, Name = "ZEYTİNBURNU", CityId = 34, PostCode = 1739 },
                new Districts { Id = 441, Name = "ALİAĞA", CityId = 35, PostCode = 1128 },
                new Districts { Id = 442, Name = "BALÇOVA", CityId = 35, PostCode = 2006 },
                new Districts { Id = 443, Name = "BAYINDIR", CityId = 35, PostCode = 1178 },
                new Districts { Id = 444, Name = "BAYRAKLI", CityId = 35, PostCode = 2056 },
                new Districts { Id = 445, Name = "BERGAMA", CityId = 35, PostCode = 1181 },
                new Districts { Id = 446, Name = "BEYDAĞ", CityId = 35, PostCode = 1776 },
                new Districts { Id = 447, Name = "BORNOVA", CityId = 35, PostCode = 1203 },
                new Districts { Id = 448, Name = "BUCA", CityId = 35, PostCode = 1780 },
                new Districts { Id = 449, Name = "ÇEŞME", CityId = 35, PostCode = 1251 },
                new Districts { Id = 450, Name = "ÇİĞLİ", CityId = 35, PostCode = 2007 },
                new Districts { Id = 451, Name = "DİKİLİ", CityId = 35, PostCode = 1280 },
                new Districts { Id = 452, Name = "FOÇA", CityId = 35, PostCode = 1334 },
                new Districts { Id = 453, Name = "GAZİEMİR", CityId = 35, PostCode = 2009 },
                new Districts { Id = 454, Name = "GÜZELBAHÇE", CityId = 35, PostCode = 2018 },
                new Districts { Id = 455, Name = "KARABAĞLAR", CityId = 35, PostCode = 2057 },
                new Districts { Id = 456, Name = "KARABURUN", CityId = 35, PostCode = 1432 },
                new Districts { Id = 457, Name = "KARŞIYAKA", CityId = 35, PostCode = 1448 },
                new Districts { Id = 458, Name = "KEMALPAŞA", CityId = 35, PostCode = 1461 },
                new Districts { Id = 459, Name = "KINIK", CityId = 35, PostCode = 1467 },
                new Districts { Id = 460, Name = "KİRAZ", CityId = 35, PostCode = 1477 },
                new Districts { Id = 461, Name = "KONAK", CityId = 35, PostCode = 1819 },
                new Districts { Id = 462, Name = "MENDERES", CityId = 35, PostCode = 1826 },
                new Districts { Id = 463, Name = "MENEMEN", CityId = 35, PostCode = 1521 },
                new Districts { Id = 464, Name = "NARLIDERE", CityId = 35, PostCode = 2013 },
                new Districts { Id = 465, Name = "ÖDEMİŞ", CityId = 35, PostCode = 1563 },
                new Districts { Id = 466, Name = "SEFERİHİSAR", CityId = 35, PostCode = 1611 },
                new Districts { Id = 467, Name = "SELÇUK", CityId = 35, PostCode = 1612 },
                new Districts { Id = 468, Name = "TİRE", CityId = 35, PostCode = 1677 },
                new Districts { Id = 469, Name = "TORBALI", CityId = 35, PostCode = 1682 },
                new Districts { Id = 470, Name = "URLA", CityId = 35, PostCode = 1703 },
                new Districts { Id = 471, Name = "AKYAKA", CityId = 36, PostCode = 1756 },
                new Districts { Id = 472, Name = "ARPAÇAY", CityId = 36, PostCode = 1149 },
                new Districts { Id = 473, Name = "DİGOR", CityId = 36, PostCode = 1279 },
                new Districts { Id = 474, Name = "KAĞIZMAN", CityId = 36, PostCode = 1424 },
                new Districts { Id = 475, Name = "SARIKAMIŞ", CityId = 36, PostCode = 1601 },
                new Districts { Id = 476, Name = "SELİM", CityId = 36, PostCode = 1614 },
                new Districts { Id = 477, Name = "SUSUZ", CityId = 36, PostCode = 1645 },
                new Districts { Id = 478, Name = "ABANA", CityId = 37, PostCode = 1101 },
                new Districts { Id = 479, Name = "AĞLI", CityId = 37, PostCode = 1867 },
                new Districts { Id = 480, Name = "ARAÇ", CityId = 37, PostCode = 1140 },
                new Districts { Id = 481, Name = "AZDAVAY", CityId = 37, PostCode = 1162 },
                new Districts { Id = 482, Name = "BOZKURT", CityId = 37, PostCode = 1208 },
                new Districts { Id = 483, Name = "CİDE", CityId = 37, PostCode = 1221 },
                new Districts { Id = 484, Name = "ÇATALZEYTİN", CityId = 37, PostCode = 1238 },
                new Districts { Id = 485, Name = "DADAY", CityId = 37, PostCode = 1264 },
                new Districts { Id = 486, Name = "DEVREKANİ", CityId = 37, PostCode = 1277 },
                new Districts { Id = 487, Name = "DOĞANYURT", CityId = 37, PostCode = 1915 },
                new Districts { Id = 488, Name = "HANÖNÜ", CityId = 37, PostCode = 1940 },
                new Districts { Id = 489, Name = "İHSANGAZİ", CityId = 37, PostCode = 1805 },
                new Districts { Id = 490, Name = "İNEBOLU", CityId = 37, PostCode = 1410 },
                new Districts { Id = 491, Name = "KÜRE", CityId = 37, PostCode = 1499 },
                new Districts { Id = 492, Name = "PINARBAŞI", CityId = 37, PostCode = 1836 },
                new Districts { Id = 493, Name = "SEYDİLER", CityId = 37, PostCode = 1984 },
                new Districts { Id = 494, Name = "ŞENPAZAR", CityId = 37, PostCode = 1845 },
                new Districts { Id = 495, Name = "TAŞKÖPRÜ", CityId = 37, PostCode = 1666 },
                new Districts { Id = 496, Name = "TOSYA", CityId = 37, PostCode = 1685 },
                new Districts { Id = 497, Name = "AKKIŞLA", CityId = 38, PostCode = 1752 },
                new Districts { Id = 498, Name = "BÜNYAN", CityId = 38, PostCode = 1218 },
                new Districts { Id = 499, Name = "DEVELİ", CityId = 38, PostCode = 1275 },
                new Districts { Id = 500, Name = "FELAHİYE", CityId = 38, PostCode = 1330 },
                new Districts { Id = 501, Name = "HACILAR", CityId = 38, PostCode = 1936 },
                new Districts { Id = 502, Name = "İNCESU", CityId = 38, PostCode = 1409 },
                new Districts { Id = 503, Name = "KOCASİNAN", CityId = 38, PostCode = 1863 },
                new Districts { Id = 504, Name = "MELİKGAZİ", CityId = 38, PostCode = 1864 },
                new Districts { Id = 505, Name = "ÖZVATAN", CityId = 38, PostCode = 1978 },
                new Districts { Id = 506, Name = "PINARBAŞI", CityId = 38, PostCode = 1576 },
                new Districts { Id = 507, Name = "SARIOĞLAN", CityId = 38, PostCode = 1603 },
                new Districts { Id = 508, Name = "SARIZ", CityId = 38, PostCode = 1605 },
                new Districts { Id = 509, Name = "TALAS", CityId = 38, PostCode = 1846 },
                new Districts { Id = 510, Name = "TOMARZA", CityId = 38, PostCode = 1680 },
                new Districts { Id = 511, Name = "YAHYALI", CityId = 38, PostCode = 1715 },
                new Districts { Id = 512, Name = "YEŞİLHİSAR", CityId = 38, PostCode = 1727 },
                new Districts { Id = 513, Name = "BABAESKİ", CityId = 39, PostCode = 1163 },
                new Districts { Id = 514, Name = "DEMİRKÖY", CityId = 39, PostCode = 1270 },
                new Districts { Id = 515, Name = "KOFÇAZ", CityId = 39, PostCode = 1480 },
                new Districts { Id = 516, Name = "LÜLEBURGAZ", CityId = 39, PostCode = 1505 },
                new Districts { Id = 517, Name = "PEHLİVANKÖY", CityId = 39, PostCode = 1572 },
                new Districts { Id = 518, Name = "PINARHİSAR", CityId = 39, PostCode = 1577 },
                new Districts { Id = 519, Name = "VİZE", CityId = 39, PostCode = 1714 },
                new Districts { Id = 520, Name = "AKÇAKENT", CityId = 40, PostCode = 1869 },
                new Districts { Id = 521, Name = "AKPINAR", CityId = 40, PostCode = 1754 },
                new Districts { Id = 522, Name = "BOZTEPE", CityId = 40, PostCode = 1890 },
                new Districts { Id = 523, Name = "ÇİÇEKDAĞI", CityId = 40, PostCode = 1254 },
                new Districts { Id = 524, Name = "KAMAN", CityId = 40, PostCode = 1429 },
                new Districts { Id = 525, Name = "MUCUR", CityId = 40, PostCode = 1529 },
                new Districts { Id = 526, Name = "BAŞİSKELE", CityId = 41, PostCode = 2058 },
                new Districts { Id = 527, Name = "ÇAYIROVA", CityId = 41, PostCode = 2059 },
                new Districts { Id = 528, Name = "DARICA", CityId = 41, PostCode = 2060 },
                new Districts { Id = 529, Name = "DERİNCE", CityId = 41, PostCode = 2030 },
                new Districts { Id = 530, Name = "DİLOVASI", CityId = 41, PostCode = 2061 },
                new Districts { Id = 531, Name = "GEBZE", CityId = 41, PostCode = 1338 },
                new Districts { Id = 532, Name = "GÖLCÜK", CityId = 41, PostCode = 1355 },
                new Districts { Id = 533, Name = "İZMİT", CityId = 41, PostCode = 2062 },
                new Districts { Id = 534, Name = "KANDIRA", CityId = 41, PostCode = 1430 },
                new Districts { Id = 535, Name = "KARAMÜRSEL", CityId = 41, PostCode = 1440 },
                new Districts { Id = 536, Name = "KARTEPE", CityId = 41, PostCode = 2063 },
                new Districts { Id = 537, Name = "KÖRFEZ", CityId = 41, PostCode = 1821 },
                new Districts { Id = 538, Name = "AHIRLI", CityId = 42, PostCode = 1868 },
                new Districts { Id = 539, Name = "AKÖREN", CityId = 42, PostCode = 1753 },
                new Districts { Id = 540, Name = "AKŞEHİR", CityId = 42, PostCode = 1122 },
                new Districts { Id = 541, Name = "ALTINEKİN", CityId = 42, PostCode = 1760 },
                new Districts { Id = 542, Name = "BEYŞEHİR", CityId = 42, PostCode = 1188 },
                new Districts { Id = 543, Name = "BOZKIR", CityId = 42, PostCode = 1207 },
                new Districts { Id = 544, Name = "CİHANBEYLİ", CityId = 42, PostCode = 1222 },
                new Districts { Id = 545, Name = "ÇELTİK", CityId = 42, PostCode = 1902 },
                new Districts { Id = 546, Name = "ÇUMRA", CityId = 42, PostCode = 1262 },
                new Districts { Id = 547, Name = "DERBENT", CityId = 42, PostCode = 1907 },
                new Districts { Id = 548, Name = "DEREBUCAK", CityId = 42, PostCode = 1789 },
                new Districts { Id = 549, Name = "DOĞANHİSAR", CityId = 42, PostCode = 1285 },
                new Districts { Id = 550, Name = "EMİRGAZİ", CityId = 42, PostCode = 1920 },
                new Districts { Id = 551, Name = "EREĞLİ", CityId = 42, PostCode = 1312 },
                new Districts { Id = 552, Name = "GÜNEYSINIR", CityId = 42, PostCode = 1933 },
                new Districts { Id = 553, Name = "HADİM", CityId = 42, PostCode = 1375 },
                new Districts { Id = 554, Name = "HALKAPINAR", CityId = 42, PostCode = 1937 },
                new Districts { Id = 555, Name = "HÜYÜK", CityId = 42, PostCode = 1804 },
                new Districts { Id = 556, Name = "ILGIN", CityId = 42, PostCode = 1400 },
                new Districts { Id = 557, Name = "KADINHANI", CityId = 42, PostCode = 1422 },
                new Districts { Id = 558, Name = "KARAPINAR", CityId = 42, PostCode = 1441 },
                new Districts { Id = 559, Name = "KARATAY", CityId = 42, PostCode = 1814 },
                new Districts { Id = 560, Name = "KULU", CityId = 42, PostCode = 1491 },
                new Districts { Id = 561, Name = "MERAM", CityId = 42, PostCode = 1827 },
                new Districts { Id = 562, Name = "SARAYÖNÜ", CityId = 42, PostCode = 1598 },
                new Districts { Id = 563, Name = "SELÇUKLU", CityId = 42, PostCode = 1839 },
                new Districts { Id = 564, Name = "SEYDİŞEHİR", CityId = 42, PostCode = 1617 },
                new Districts { Id = 565, Name = "TAŞKENT", CityId = 42, PostCode = 1848 },
                new Districts { Id = 566, Name = "TUZLUKÇU", CityId = 42, PostCode = 1990 },
                new Districts { Id = 567, Name = "YALIHÜYÜK", CityId = 42, PostCode = 1994 },
                new Districts { Id = 568, Name = "YUNAK", CityId = 42, PostCode = 1735 },
                new Districts { Id = 569, Name = "ALTINTAŞ", CityId = 43, PostCode = 1132 },
                new Districts { Id = 570, Name = "ASLANAPA", CityId = 43, PostCode = 1764 },
                new Districts { Id = 571, Name = "ÇAVDARHİSAR", CityId = 43, PostCode = 1898 },
                new Districts { Id = 572, Name = "DOMANİÇ", CityId = 43, PostCode = 1288 },
                new Districts { Id = 573, Name = "DUMLUPINAR", CityId = 43, PostCode = 1790 },
                new Districts { Id = 574, Name = "EMET", CityId = 43, PostCode = 1304 },
                new Districts { Id = 575, Name = "GEDİZ", CityId = 43, PostCode = 1339 },
                new Districts { Id = 576, Name = "HİSARCIK", CityId = 43, PostCode = 1802 },
                new Districts { Id = 577, Name = "PAZARLAR", CityId = 43, PostCode = 1979 },
                new Districts { Id = 578, Name = "SİMAV", CityId = 43, PostCode = 1625 },
                new Districts { Id = 579, Name = "ŞAPHANE", CityId = 43, PostCode = 1843 },
                new Districts { Id = 580, Name = "TAVŞANLI", CityId = 43, PostCode = 1671 },
                new Districts { Id = 581, Name = "AKÇADAĞ", CityId = 44, PostCode = 1114 },
                new Districts { Id = 582, Name = "ARAPGİR", CityId = 44, PostCode = 1143 },
                new Districts { Id = 583, Name = "ARGUVAN", CityId = 44, PostCode = 1148 },
                new Districts { Id = 584, Name = "BATTALGAZİ", CityId = 44, PostCode = 1772 },
                new Districts { Id = 585, Name = "DARENDE", CityId = 44, PostCode = 1265 },
                new Districts { Id = 586, Name = "DOĞANŞEHİR", CityId = 44, PostCode = 1286 },
                new Districts { Id = 587, Name = "DOĞANYOL", CityId = 44, PostCode = 1914 },
                new Districts { Id = 588, Name = "HEKİMHAN", CityId = 44, PostCode = 1390 },
                new Districts { Id = 589, Name = "KALE", CityId = 44, PostCode = 1953 },
                new Districts { Id = 590, Name = "KULUNCAK", CityId = 44, PostCode = 1969 },
                new Districts { Id = 591, Name = "PÜTÜRGE", CityId = 44, PostCode = 1582 },
                new Districts { Id = 592, Name = "YAZIHAN", CityId = 44, PostCode = 1995 },
                new Districts { Id = 593, Name = "YEŞİLYURT", CityId = 44, PostCode = 1729 },
                new Districts { Id = 594, Name = "AHMETLİ", CityId = 45, PostCode = 1751 },
                new Districts { Id = 595, Name = "AKHİSAR", CityId = 45, PostCode = 1118 },
                new Districts { Id = 596, Name = "ALAŞEHİR", CityId = 45, PostCode = 1127 },
                new Districts { Id = 597, Name = "DEMİRCİ", CityId = 45, PostCode = 1269 },
                new Districts { Id = 598, Name = "GÖLMARMARA", CityId = 45, PostCode = 1793 },
                new Districts { Id = 599, Name = "GÖRDES", CityId = 45, PostCode = 1362 },
                new Districts { Id = 600, Name = "KIRKAĞAÇ", CityId = 45, PostCode = 1470 },
                new Districts { Id = 601, Name = "KÖPRÜBAŞI", CityId = 45, PostCode = 1965 },
                new Districts { Id = 602, Name = "KULA", CityId = 45, PostCode = 1489 },
                new Districts { Id = 603, Name = "SALİHLİ", CityId = 45, PostCode = 1590 },
                new Districts { Id = 604, Name = "SARIGÖL", CityId = 45, PostCode = 1600 },
                new Districts { Id = 605, Name = "SARUHANLI", CityId = 45, PostCode = 1606 },
                new Districts { Id = 606, Name = "SELENDİ", CityId = 45, PostCode = 1613 },
                new Districts { Id = 607, Name = "SOMA", CityId = 45, PostCode = 1634 },
                new Districts { Id = 608, Name = "ŞEHZADELER", CityId = 45, PostCode = 2086 },
                new Districts { Id = 609, Name = "TURGUTLU", CityId = 45, PostCode = 1689 },
                new Districts { Id = 610, Name = "YUNUSEMRE", CityId = 45, PostCode = 2087 },
                new Districts { Id = 611, Name = "AFŞİN", CityId = 46, PostCode = 1107 },
                new Districts { Id = 612, Name = "ANDIRIN", CityId = 46, PostCode = 1136 },
                new Districts { Id = 613, Name = "ÇAĞLAYANCERİT", CityId = 46, PostCode = 1785 },
                new Districts { Id = 614, Name = "DULKADİROĞLU", CityId = 46, PostCode = 2084 },
                new Districts { Id = 615, Name = "EKİNÖZÜ", CityId = 46, PostCode = 1919 },
                new Districts { Id = 616, Name = "ELBİSTAN", CityId = 46, PostCode = 1299 },
                new Districts { Id = 617, Name = "GÖKSUN", CityId = 46, PostCode = 1353 },
                new Districts { Id = 618, Name = "NURHAK", CityId = 46, PostCode = 1975 },
                new Districts { Id = 619, Name = "ONİKİŞUBAT", CityId = 46, PostCode = 2085 },
                new Districts { Id = 620, Name = "PAZARCIK", CityId = 46, PostCode = 1570 },
                new Districts { Id = 621, Name = "TÜRKOĞLU", CityId = 46, PostCode = 1694 },
                new Districts { Id = 622, Name = "ARTUKLU", CityId = 47, PostCode = 2088 },
                new Districts { Id = 623, Name = "DARGEÇİT", CityId = 47, PostCode = 1787 },
                new Districts { Id = 624, Name = "DERİK", CityId = 47, PostCode = 1273 },
                new Districts { Id = 625, Name = "KIZILTEPE", CityId = 47, PostCode = 1474 },
                new Districts { Id = 626, Name = "MAZIDAĞI", CityId = 47, PostCode = 1519 },
                new Districts { Id = 627, Name = "MİDYAT", CityId = 47, PostCode = 1526 },
                new Districts { Id = 628, Name = "NUSAYBİN", CityId = 47, PostCode = 1547 },
                new Districts { Id = 629, Name = "ÖMERLİ", CityId = 47, PostCode = 1564 },
                new Districts { Id = 630, Name = "SAVUR", CityId = 47, PostCode = 1609 },
                new Districts { Id = 631, Name = "YEŞİLLİ", CityId = 47, PostCode = 2002 },
                new Districts { Id = 632, Name = "BODRUM", CityId = 48, PostCode = 1197 },
                new Districts { Id = 633, Name = "DALAMAN", CityId = 48, PostCode = 1742 },
                new Districts { Id = 634, Name = "DATÇA", CityId = 48, PostCode = 1266 },
                new Districts { Id = 635, Name = "FETHİYE", CityId = 48, PostCode = 1331 },
                new Districts { Id = 636, Name = "KAVAKLIDERE", CityId = 48, PostCode = 1958 },
                new Districts { Id = 637, Name = "KÖYCEĞİZ", CityId = 48, PostCode = 1488 },
                new Districts { Id = 638, Name = "MARMARİS", CityId = 48, PostCode = 1517 },
                new Districts { Id = 639, Name = "MENTEŞE", CityId = 48, PostCode = 2089 },
                new Districts { Id = 640, Name = "MİLAS", CityId = 48, PostCode = 1528 },
                new Districts { Id = 641, Name = "ORTACA", CityId = 48, PostCode = 1831 },
                new Districts { Id = 642, Name = "SEYDİKEMER", CityId = 48, PostCode = 2090 },
                new Districts { Id = 643, Name = "ULA", CityId = 48, PostCode = 1695 },
                new Districts { Id = 644, Name = "YATAĞAN", CityId = 48, PostCode = 1719 },
                new Districts { Id = 645, Name = "BULANIK", CityId = 49, PostCode = 1213 },
                new Districts { Id = 646, Name = "HASKÖY", CityId = 49, PostCode = 1801 },
                new Districts { Id = 647, Name = "KORKUT", CityId = 49, PostCode = 1964 },
                new Districts { Id = 648, Name = "MALAZGİRT", CityId = 49, PostCode = 1510 },
                new Districts { Id = 649, Name = "VARTO", CityId = 49, PostCode = 1711 },
                new Districts { Id = 650, Name = "ACIGÖL", CityId = 50, PostCode = 1749 },
                new Districts { Id = 651, Name = "AVANOS", CityId = 50, PostCode = 1155 },
                new Districts { Id = 652, Name = "DERİNKUYU", CityId = 50, PostCode = 1274 },
                new Districts { Id = 653, Name = "GÜLŞEHİR", CityId = 50, PostCode = 1367 },
                new Districts { Id = 654, Name = "HACIBEKTAŞ", CityId = 50, PostCode = 1374 },
                new Districts { Id = 655, Name = "KOZAKLI", CityId = 50, PostCode = 1485 },
                new Districts { Id = 656, Name = "ÜRGÜP", CityId = 50, PostCode = 1707 },
                new Districts { Id = 657, Name = "ALTUNHİSAR", CityId = 51, PostCode = 1876 },
                new Districts { Id = 658, Name = "BOR", CityId = 51, PostCode = 1201 },
                new Districts { Id = 659, Name = "ÇAMARDI", CityId = 51, PostCode = 1225 },
                new Districts { Id = 660, Name = "ÇİFTLİK", CityId = 51, PostCode = 1904 },
                new Districts { Id = 661, Name = "ULUKIŞLA", CityId = 51, PostCode = 1700 },
                new Districts { Id = 662, Name = "AKKUŞ", CityId = 52, PostCode = 1119 },
                new Districts { Id = 663, Name = "ALTINORDU", CityId = 52, PostCode = 2103 },
                new Districts { Id = 664, Name = "AYBASTI", CityId = 52, PostCode = 1158 },
                new Districts { Id = 665, Name = "ÇAMAŞ", CityId = 52, PostCode = 1891 },
                new Districts { Id = 666, Name = "ÇATALPINAR", CityId = 52, PostCode = 1897 },
                new Districts { Id = 667, Name = "ÇAYBAŞI", CityId = 52, PostCode = 1900 },
                new Districts { Id = 668, Name = "FATSA", CityId = 52, PostCode = 1328 },
                new Districts { Id = 669, Name = "GÖLKÖY", CityId = 52, PostCode = 1358 },
                new Districts { Id = 670, Name = "GÜLYALI", CityId = 52, PostCode = 1795 },
                new Districts { Id = 671, Name = "GÜRGENTEPE", CityId = 52, PostCode = 1797 },
                new Districts { Id = 672, Name = "İKİZCE", CityId = 52, PostCode = 1947 },
                new Districts { Id = 673, Name = "KABADÜZ", CityId = 52, PostCode = 1950 },
                new Districts { Id = 674, Name = "KABATAŞ", CityId = 52, PostCode = 1951 },
                new Districts { Id = 675, Name = "KORGAN", CityId = 52, PostCode = 1482 },
                new Districts { Id = 676, Name = "KUMRU", CityId = 52, PostCode = 1493 },
                new Districts { Id = 677, Name = "MESUDİYE", CityId = 52, PostCode = 1525 },
                new Districts { Id = 678, Name = "PERŞEMBE", CityId = 52, PostCode = 1573 },
                new Districts { Id = 679, Name = "ULUBEY", CityId = 52, PostCode = 1696 },
                new Districts { Id = 680, Name = "ÜNYE", CityId = 52, PostCode = 1706 },
                new Districts { Id = 681, Name = "ARDEŞEN", CityId = 53, PostCode = 1146 },
                new Districts { Id = 682, Name = "ÇAMLIHEMŞİN", CityId = 53, PostCode = 1228 },
                new Districts { Id = 683, Name = "ÇAYELİ", CityId = 53, PostCode = 1241 },
                new Districts { Id = 684, Name = "DEREPAZARI", CityId = 53, PostCode = 1908 },
                new Districts { Id = 685, Name = "FINDIKLI", CityId = 53, PostCode = 1332 },
                new Districts { Id = 686, Name = "GÜNEYSU", CityId = 53, PostCode = 1796 },
                new Districts { Id = 687, Name = "HEMŞİN", CityId = 53, PostCode = 1943 },
                new Districts { Id = 688, Name = "İKİZDERE", CityId = 53, PostCode = 1405 },
                new Districts { Id = 689, Name = "İYİDERE", CityId = 53, PostCode = 1949 },
                new Districts { Id = 690, Name = "KALKANDERE", CityId = 53, PostCode = 1428 },
                new Districts { Id = 691, Name = "PAZAR", CityId = 53, PostCode = 1569 },
                new Districts { Id = 692, Name = "ADAPAZARI", CityId = 54, PostCode = 2068 },
                new Districts { Id = 693, Name = "AKYAZI", CityId = 54, PostCode = 1123 },
                new Districts { Id = 694, Name = "ARİFİYE", CityId = 54, PostCode = 2069 },
                new Districts { Id = 695, Name = "ERENLER", CityId = 54, PostCode = 2070 },
                new Districts { Id = 696, Name = "FERİZLİ", CityId = 54, PostCode = 1925 },
                new Districts { Id = 697, Name = "GEYVE", CityId = 54, PostCode = 1351 },
                new Districts { Id = 698, Name = "HENDEK", CityId = 54, PostCode = 1391 },
                new Districts { Id = 699, Name = "KARAPÜRÇEK", CityId = 54, PostCode = 1955 },
                new Districts { Id = 700, Name = "KARASU", CityId = 54, PostCode = 1442 },
                new Districts { Id = 701, Name = "KAYNARCA", CityId = 54, PostCode = 1453 },
                new Districts { Id = 702, Name = "KOCAALİ", CityId = 54, PostCode = 1818 },
                new Districts { Id = 703, Name = "PAMUKOVA", CityId = 54, PostCode = 1833 },
                new Districts { Id = 704, Name = "SAPANCA", CityId = 54, PostCode = 1595 },
                new Districts { Id = 705, Name = "SERDİVAN", CityId = 54, PostCode = 2071 },
                new Districts { Id = 706, Name = "SÖĞÜTLÜ", CityId = 54, PostCode = 1986 },
                new Districts { Id = 707, Name = "TARAKLI", CityId = 54, PostCode = 1847 },
                new Districts { Id = 708, Name = "19 MAYIS", CityId = 55, PostCode = 1830 },
                new Districts { Id = 709, Name = "ALAÇAM", CityId = 55, PostCode = 1125 },
                new Districts { Id = 710, Name = "ASARCIK", CityId = 55, PostCode = 1763 },
                new Districts { Id = 711, Name = "ATAKUM", CityId = 55, PostCode = 2072 },
                new Districts { Id = 712, Name = "AYVACIK", CityId = 55, PostCode = 1879 },
                new Districts { Id = 713, Name = "BAFRA", CityId = 55, PostCode = 1164 },
                new Districts { Id = 714, Name = "CANİK", CityId = 55, PostCode = 2073 },
                new Districts { Id = 715, Name = "ÇARŞAMBA", CityId = 55, PostCode = 1234 },
                new Districts { Id = 716, Name = "HAVZA", CityId = 55, PostCode = 1386 },
                new Districts { Id = 717, Name = "İLKADIM", CityId = 55, PostCode = 2074 },
                new Districts { Id = 718, Name = "KAVAK", CityId = 55, PostCode = 1452 },
                new Districts { Id = 719, Name = "LADİK", CityId = 55, PostCode = 1501 },
                new Districts { Id = 720, Name = "SALIPAZARI", CityId = 55, PostCode = 1838 },
                new Districts { Id = 721, Name = "TEKKEKÖY", CityId = 55, PostCode = 1849 },
                new Districts { Id = 722, Name = "TERME", CityId = 55, PostCode = 1676 },
                new Districts { Id = 723, Name = "VEZİRKÖPRÜ", CityId = 55, PostCode = 1712 },
                new Districts { Id = 724, Name = "YAKAKENT", CityId = 55, PostCode = 1993 },
                new Districts { Id = 725, Name = "BAYKAN", CityId = 56, PostCode = 1179 },
                new Districts { Id = 726, Name = "ERUH", CityId = 56, PostCode = 1317 },
                new Districts { Id = 727, Name = "KURTALAN", CityId = 56, PostCode = 1495 },
                new Districts { Id = 728, Name = "PERVARİ", CityId = 56, PostCode = 1575 },
                new Districts { Id = 729, Name = "ŞİRVAN", CityId = 56, PostCode = 1662 },
                new Districts { Id = 730, Name = "TİLLO", CityId = 56, PostCode = 1878 },
                new Districts { Id = 731, Name = "AYANCIK", CityId = 57, PostCode = 1156 },
                new Districts { Id = 732, Name = "BOYABAT", CityId = 57, PostCode = 1204 },
                new Districts { Id = 733, Name = "DİKMEN", CityId = 57, PostCode = 1910 },
                new Districts { Id = 734, Name = "DURAĞAN", CityId = 57, PostCode = 1290 },
                new Districts { Id = 735, Name = "ERFELEK", CityId = 57, PostCode = 1314 },
                new Districts { Id = 736, Name = "GERZE", CityId = 57, PostCode = 1349 },
                new Districts { Id = 737, Name = "SARAYDÜZÜ", CityId = 57, PostCode = 1981 },
                new Districts { Id = 738, Name = "TÜRKELİ", CityId = 57, PostCode = 1693 },
                new Districts { Id = 739, Name = "AKINCILAR", CityId = 58, PostCode = 1870 },
                new Districts { Id = 740, Name = "ALTINYAYLA", CityId = 58, PostCode = 1875 },
                new Districts { Id = 741, Name = "DİVRİĞİ", CityId = 58, PostCode = 1282 },
                new Districts { Id = 742, Name = "DOĞANŞAR", CityId = 58, PostCode = 1913 },
                new Districts { Id = 743, Name = "GEMEREK", CityId = 58, PostCode = 1342 },
                new Districts { Id = 744, Name = "GÖLOVA", CityId = 58, PostCode = 1927 },
                new Districts { Id = 745, Name = "GÜRÜN", CityId = 58, PostCode = 1373 },
                new Districts { Id = 746, Name = "HAFİK", CityId = 58, PostCode = 1376 },
                new Districts { Id = 747, Name = "İMRANLI", CityId = 58, PostCode = 1407 },
                new Districts { Id = 748, Name = "KANGAL", CityId = 58, PostCode = 1431 },
                new Districts { Id = 749, Name = "KOYULHİSAR", CityId = 58, PostCode = 1484 },
                new Districts { Id = 750, Name = "SUŞEHRİ", CityId = 58, PostCode = 1646 },
                new Districts { Id = 751, Name = "ŞARKIŞLA", CityId = 58, PostCode = 1650 },
                new Districts { Id = 752, Name = "ULAŞ", CityId = 58, PostCode = 1991 },
                new Districts { Id = 753, Name = "YILDIZELİ", CityId = 58, PostCode = 1731 },
                new Districts { Id = 754, Name = "ZARA", CityId = 58, PostCode = 1738 },
                new Districts { Id = 755, Name = "ÇERKEZKÖY", CityId = 59, PostCode = 1250 },
                new Districts { Id = 756, Name = "ÇORLU", CityId = 59, PostCode = 1258 },
                new Districts { Id = 757, Name = "ERGENE", CityId = 59, PostCode = 2094 },
                new Districts { Id = 758, Name = "HAYRABOLU", CityId = 59, PostCode = 1388 },
                new Districts { Id = 759, Name = "KAPAKLI", CityId = 59, PostCode = 2095 },
                new Districts { Id = 760, Name = "MALKARA", CityId = 59, PostCode = 1511 },
                new Districts { Id = 761, Name = "MARMARAEREĞLİSİ", CityId = 59, PostCode = 1825 },
                new Districts { Id = 762, Name = "MURATLI", CityId = 59, PostCode = 1538 },
                new Districts { Id = 763, Name = "SARAY", CityId = 59, PostCode = 1596 },
                new Districts { Id = 764, Name = "SÜLEYMANPAŞA", CityId = 59, PostCode = 2096 },
                new Districts { Id = 765, Name = "ŞARKÖY", CityId = 59, PostCode = 1652 },
                new Districts { Id = 766, Name = "ALMUS", CityId = 60, PostCode = 1129 },
                new Districts { Id = 767, Name = "ARTOVA", CityId = 60, PostCode = 1151 },
                new Districts { Id = 768, Name = "BAŞÇİFTLİK", CityId = 60, PostCode = 1883 },
                new Districts { Id = 769, Name = "ERBAA", CityId = 60, PostCode = 1308 },
                new Districts { Id = 770, Name = "NİKSAR", CityId = 60, PostCode = 1545 },
                new Districts { Id = 771, Name = "PAZAR", CityId = 60, PostCode = 1834 },
                new Districts { Id = 772, Name = "REŞADİYE", CityId = 60, PostCode = 1584 },
                new Districts { Id = 773, Name = "SULUSARAY", CityId = 60, PostCode = 1987 },
                new Districts { Id = 774, Name = "TURHAL", CityId = 60, PostCode = 1690 },
                new Districts { Id = 775, Name = "YEŞİLYURT", CityId = 60, PostCode = 1858 },
                new Districts { Id = 776, Name = "ZİLE", CityId = 60, PostCode = 1740 },
                new Districts { Id = 777, Name = "AKÇAABAT", CityId = 61, PostCode = 1113 },
                new Districts { Id = 778, Name = "ARAKLI", CityId = 61, PostCode = 1141 },
                new Districts { Id = 779, Name = "ARSİN", CityId = 61, PostCode = 1150 },
                new Districts { Id = 780, Name = "BEŞİKDÜZÜ", CityId = 61, PostCode = 1775 },
                new Districts { Id = 781, Name = "ÇARŞIBAŞI", CityId = 61, PostCode = 1896 },
                new Districts { Id = 782, Name = "ÇAYKARA", CityId = 61, PostCode = 1244 },
                new Districts { Id = 783, Name = "DERNEKPAZARI", CityId = 61, PostCode = 1909 },
                new Districts { Id = 784, Name = "DÜZKÖY", CityId = 61, PostCode = 1917 },
                new Districts { Id = 785, Name = "HAYRAT", CityId = 61, PostCode = 1942 },
                new Districts { Id = 786, Name = "KÖPRÜBAŞI", CityId = 61, PostCode = 1966 },
                new Districts { Id = 787, Name = "MAÇKA", CityId = 61, PostCode = 1507 },
                new Districts { Id = 788, Name = "OF", CityId = 61, PostCode = 1548 },
                new Districts { Id = 789, Name = "ORTAHİSAR", CityId = 61, PostCode = 2097 },
                new Districts { Id = 790, Name = "SÜRMENE", CityId = 61, PostCode = 1647 },
                new Districts { Id = 791, Name = "ŞALPAZARI", CityId = 61, PostCode = 1842 },
                new Districts { Id = 792, Name = "TONYA", CityId = 61, PostCode = 1681 },
                new Districts { Id = 793, Name = "VAKFIKEBİR", CityId = 61, PostCode = 1709 },
                new Districts { Id = 794, Name = "YOMRA", CityId = 61, PostCode = 1732 },
                new Districts { Id = 795, Name = "ÇEMİŞGEZEK", CityId = 62, PostCode = 1247 },
                new Districts { Id = 796, Name = "HOZAT", CityId = 62, PostCode = 1397 },
                new Districts { Id = 797, Name = "MAZGİRT", CityId = 62, PostCode = 1518 },
                new Districts { Id = 798, Name = "NAZIMİYE", CityId = 62, PostCode = 1541 },
                new Districts { Id = 799, Name = "OVACIK", CityId = 62, PostCode = 1562 },
                new Districts { Id = 800, Name = "PERTEK", CityId = 62, PostCode = 1574 },
                new Districts { Id = 801, Name = "PÜLÜMÜR", CityId = 62, PostCode = 1581 },
                new Districts { Id = 802, Name = "AKÇAKALE", CityId = 63, PostCode = 1115 },
                new Districts { Id = 803, Name = "BİRECİK", CityId = 63, PostCode = 1194 },
                new Districts { Id = 804, Name = "BOZOVA", CityId = 63, PostCode = 1209 },
                new Districts { Id = 805, Name = "CEYLANPINAR", CityId = 63, PostCode = 1220 },
                new Districts { Id = 806, Name = "EYYÜBİYE", CityId = 63, PostCode = 2091 },
                new Districts { Id = 807, Name = "HALFETİ", CityId = 63, PostCode = 1378 },
                new Districts { Id = 808, Name = "HALİLİYE", CityId = 63, PostCode = 2092 },
                new Districts { Id = 809, Name = "HARRAN", CityId = 63, PostCode = 1800 },
                new Districts { Id = 810, Name = "HİLVAN", CityId = 63, PostCode = 1393 },
                new Districts { Id = 811, Name = "KARAKÖPRÜ", CityId = 63, PostCode = 2093 },
                new Districts { Id = 812, Name = "SİVEREK", CityId = 63, PostCode = 1630 },
                new Districts { Id = 813, Name = "SURUÇ", CityId = 63, PostCode = 1643 },
                new Districts { Id = 814, Name = "VİRANŞEHİR", CityId = 63, PostCode = 1713 },
                new Districts { Id = 815, Name = "BANAZ", CityId = 64, PostCode = 1170 },
                new Districts { Id = 816, Name = "EŞME", CityId = 64, PostCode = 1323 },
                new Districts { Id = 817, Name = "KARAHALLI", CityId = 64, PostCode = 1436 },
                new Districts { Id = 818, Name = "SİVASLI", CityId = 64, PostCode = 1629 },
                new Districts { Id = 819, Name = "ULUBEY", CityId = 64, PostCode = 1697 },
                new Districts { Id = 820, Name = "BAHÇESARAY", CityId = 65, PostCode = 1770 },
                new Districts { Id = 821, Name = "BAŞKALE", CityId = 65, PostCode = 1175 },
                new Districts { Id = 822, Name = "ÇALDIRAN", CityId = 65, PostCode = 1786 },
                new Districts { Id = 823, Name = "ÇATAK", CityId = 65, PostCode = 1236 },
                new Districts { Id = 824, Name = "EDREMİT", CityId = 65, PostCode = 1918 },
                new Districts { Id = 825, Name = "ERCİŞ", CityId = 65, PostCode = 1309 },
                new Districts { Id = 826, Name = "GEVAŞ", CityId = 65, PostCode = 1350 },
                new Districts { Id = 827, Name = "GÜRPINAR", CityId = 65, PostCode = 1372 },
                new Districts { Id = 828, Name = "İPEKYOLU", CityId = 65, PostCode = 2098 },
                new Districts { Id = 829, Name = "MURADİYE", CityId = 65, PostCode = 1533 },
                new Districts { Id = 830, Name = "ÖZALP", CityId = 65, PostCode = 1565 },
                new Districts { Id = 831, Name = "SARAY", CityId = 65, PostCode = 1980 },
                new Districts { Id = 832, Name = "TUŞBA", CityId = 65, PostCode = 2099 },
                new Districts { Id = 833, Name = "AKDAĞMADENİ", CityId = 66, PostCode = 1117 },
                new Districts { Id = 834, Name = "AYDINCIK", CityId = 66, PostCode = 1877 },
                new Districts { Id = 835, Name = "BOĞAZLIYAN", CityId = 66, PostCode = 1198 },
                new Districts { Id = 836, Name = "ÇANDIR", CityId = 66, PostCode = 1895 },
                new Districts { Id = 837, Name = "ÇAYIRALAN", CityId = 66, PostCode = 1242 },
                new Districts { Id = 838, Name = "ÇEKEREK", CityId = 66, PostCode = 1245 },
                new Districts { Id = 839, Name = "KADIŞEHRİ", CityId = 66, PostCode = 1952 },
                new Districts { Id = 840, Name = "SARAYKENT", CityId = 66, PostCode = 1982 },
                new Districts { Id = 841, Name = "SARIKAYA", CityId = 66, PostCode = 1602 },
                new Districts { Id = 842, Name = "SORGUN", CityId = 66, PostCode = 1635 },
                new Districts { Id = 843, Name = "ŞEFAATLİ", CityId = 66, PostCode = 1655 },
                new Districts { Id = 844, Name = "YENİFAKILI", CityId = 66, PostCode = 1998 },
                new Districts { Id = 845, Name = "YERKÖY", CityId = 66, PostCode = 1726 },
                new Districts { Id = 846, Name = "ALAPLI", CityId = 67, PostCode = 1758 },
                new Districts { Id = 847, Name = "ÇAYCUMA", CityId = 67, PostCode = 1240 },
                new Districts { Id = 848, Name = "DEVREK", CityId = 67, PostCode = 1276 },
                new Districts { Id = 849, Name = "EREĞLİ", CityId = 67, PostCode = 1313 },
                new Districts { Id = 850, Name = "GÖKÇEBEY", CityId = 67, PostCode = 1926 },
                new Districts { Id = 851, Name = "KİLİMLİ", CityId = 67, PostCode = 2100 },
                new Districts { Id = 852, Name = "KOZLU", CityId = 67, PostCode = 2101 },
                new Districts { Id = 853, Name = "AĞAÇÖREN", CityId = 68, PostCode = 1860 },
                new Districts { Id = 854, Name = "ESKİL", CityId = 68, PostCode = 1921 },
                new Districts { Id = 855, Name = "GÜLAĞAÇ", CityId = 68, PostCode = 1932 },
                new Districts { Id = 856, Name = "GÜZELYURT", CityId = 68, PostCode = 1861 },
                new Districts { Id = 857, Name = "ORTAKÖY", CityId = 68, PostCode = 1557 },
                new Districts { Id = 858, Name = "SARIYAHŞİ", CityId = 68, PostCode = 1866 },
                new Districts { Id = 859, Name = "SULTANHANI", CityId = 68, PostCode = 2106 },
                new Districts { Id = 860, Name = "AYDINTEPE", CityId = 69, PostCode = 1767 },
                new Districts { Id = 861, Name = "DEMİRÖZÜ", CityId = 69, PostCode = 1788 },
                new Districts { Id = 862, Name = "AYRANCI", CityId = 70, PostCode = 1768 },
                new Districts { Id = 863, Name = "BAŞYAYLA", CityId = 70, PostCode = 1884 },
                new Districts { Id = 864, Name = "ERMENEK", CityId = 70, PostCode = 1316 },
                new Districts { Id = 865, Name = "KAZIMKARABEKİR", CityId = 70, PostCode = 1862 },
                new Districts { Id = 866, Name = "SARIVELİLER", CityId = 70, PostCode = 1983 },
                new Districts { Id = 867, Name = "BAHŞILI", CityId = 71, PostCode = 1880 },
                new Districts { Id = 868, Name = "BALIŞEYH", CityId = 71, PostCode = 1882 },
                new Districts { Id = 869, Name = "ÇELEBİ", CityId = 71, PostCode = 1901 },
                new Districts { Id = 870, Name = "DELİCE", CityId = 71, PostCode = 1268 },
                new Districts { Id = 871, Name = "KARAKEÇİLİ", CityId = 71, PostCode = 1954 },
                new Districts { Id = 872, Name = "KESKİN", CityId = 71, PostCode = 1463 },
                new Districts { Id = 873, Name = "SULAKYURT", CityId = 71, PostCode = 1638 },
                new Districts { Id = 874, Name = "YAHŞİHAN", CityId = 71, PostCode = 1992 },
                new Districts { Id = 875, Name = "BEŞİRİ", CityId = 72, PostCode = 1184 },
                new Districts { Id = 876, Name = "GERCÜŞ", CityId = 72, PostCode = 1345 },
                new Districts { Id = 877, Name = "HASANKEYF", CityId = 72, PostCode = 1941 },
                new Districts { Id = 878, Name = "KOZLUK", CityId = 72, PostCode = 1487 },
                new Districts { Id = 879, Name = "SASON", CityId = 72, PostCode = 1607 },
                new Districts { Id = 880, Name = "BEYTÜŞŞEBAP", CityId = 73, PostCode = 1189 },
                new Districts { Id = 881, Name = "CİZRE", CityId = 73, PostCode = 1223 },
                new Districts { Id = 882, Name = "GÜÇLÜKONAK", CityId = 73, PostCode = 1931 },
                new Districts { Id = 883, Name = "İDİL", CityId = 73, PostCode = 1403 },
                new Districts { Id = 884, Name = "SİLOPİ", CityId = 73, PostCode = 1623 },
                new Districts { Id = 885, Name = "ULUDERE", CityId = 73, PostCode = 1698 },
                new Districts { Id = 886, Name = "AMASRA", CityId = 74, PostCode = 1761 },
                new Districts { Id = 887, Name = "KURUCAŞİLE", CityId = 74, PostCode = 1496 },
                new Districts { Id = 888, Name = "ULUS", CityId = 74, PostCode = 1701 },
                new Districts { Id = 889, Name = "ÇILDIR", CityId = 75, PostCode = 1252 },
                new Districts { Id = 890, Name = "DAMAL", CityId = 75, PostCode = 2008 },
                new Districts { Id = 891, Name = "GÖLE", CityId = 75, PostCode = 1356 },
                new Districts { Id = 892, Name = "HANAK", CityId = 75, PostCode = 1380 },
                new Districts { Id = 893, Name = "POSOF", CityId = 75, PostCode = 1579 },
                new Districts { Id = 894, Name = "ARALIK", CityId = 76, PostCode = 1142 },
                new Districts { Id = 895, Name = "KARAKOYUNLU", CityId = 76, PostCode = 2011 },
                new Districts { Id = 896, Name = "TUZLUCA", CityId = 76, PostCode = 1692 },
                new Districts { Id = 897, Name = "ALTINOVA", CityId = 77, PostCode = 2019 },
                new Districts { Id = 898, Name = "ARMUTLU", CityId = 77, PostCode = 2020 },
                new Districts { Id = 899, Name = "ÇINARCIK", CityId = 77, PostCode = 2021 },
                new Districts { Id = 900, Name = "ÇİFTLİKKÖY", CityId = 77, PostCode = 2022 },
                new Districts { Id = 901, Name = "TERMAL", CityId = 77, PostCode = 2026 },
                new Districts { Id = 902, Name = "EFLANİ", CityId = 78, PostCode = 1296 },
                new Districts { Id = 903, Name = "ESKİPAZAR", CityId = 78, PostCode = 1321 },
                new Districts { Id = 904, Name = "OVACIK", CityId = 78, PostCode = 1561 },
                new Districts { Id = 905, Name = "SAFRANBOLU", CityId = 78, PostCode = 1587 },
                new Districts { Id = 906, Name = "YENİCE", CityId = 78, PostCode = 1856 },
                new Districts { Id = 907, Name = "ELBEYLİ", CityId = 79, PostCode = 2023 },
                new Districts { Id = 908, Name = "MUSABEYLİ", CityId = 79, PostCode = 2024 },
                new Districts { Id = 909, Name = "POLATELİ", CityId = 79, PostCode = 2025 },
                new Districts { Id = 910, Name = "BAHÇE", CityId = 80, PostCode = 1165 },
                new Districts { Id = 911, Name = "DÜZİÇİ", CityId = 80, PostCode = 1743 },
                new Districts { Id = 912, Name = "HASANBEYLİ", CityId = 80, PostCode = 2027 },
                new Districts { Id = 913, Name = "KADİRLİ", CityId = 80, PostCode = 1423 },
                new Districts { Id = 914, Name = "SUMBAS", CityId = 80, PostCode = 2028 },
                new Districts { Id = 915, Name = "TOPRAKKALE", CityId = 80, PostCode = 2029 },
                new Districts { Id = 916, Name = "AKÇAKOCA", CityId = 81, PostCode = 1116 },
                new Districts { Id = 917, Name = "CUMAYERİ", CityId = 81, PostCode = 1784 },
                new Districts { Id = 918, Name = "ÇİLİMLİ", CityId = 81, PostCode = 1905 },
                new Districts { Id = 919, Name = "GÖLYAKA", CityId = 81, PostCode = 1794 },
                new Districts { Id = 920, Name = "GÜMÜŞOVA", CityId = 81, PostCode = 2017 },
                new Districts { Id = 921, Name = "KAYNAŞLI", CityId = 81, PostCode = 2031 },
                new Districts { Id = 922, Name = "YIĞILCA", CityId = 81, PostCode = 1730 }
            );

            // INGREDIENT TYPES
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
