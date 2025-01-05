using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace DataAccess.Data
{
    public class EventDefaultData
    {
        public void PopulateEventRequirementsData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Requirements>().HasData(
                new Requirements { Id = 1, Name = "RSVP Required", Description = "Guests must confirm attendance before the event." },
                new Requirements { Id = 2, Name = "Dress Code", Description = "Guests are required to follow the formal dress code." },
                new Requirements { Id = 3, Name = "Age Limit", Description = "Only guests aged 18 and above are allowed to attend." },
                new Requirements { Id = 4, Name = "Bring Your Own Beverage (BYOB)", Description = "Guests are encouraged to bring their own beverages to the event." },
                new Requirements { Id = 5, Name = "No Pets Allowed", Description = "Pets are not allowed at the event venue." },
                new Requirements { Id = 6, Name = "ID Verification", Description = "Guests must present valid identification upon entry." },
                new Requirements { Id = 7, Name = "Allergy Notification", Description = "Guests should inform the host of any food allergies in advance." },
                new Requirements { Id = 8, Name = "Photography Policy", Description = "Photography is allowed, but guests must respect the privacy of others." },
                new Requirements { Id = 9, Name = "Non-Smoking", Description = "The event venue is a non-smoking area." },
                new Requirements { Id = 10, Name = "Timing Restrictions", Description = "Guests must arrive on time; late arrivals may not be admitted." },
                new Requirements { Id = 11, Name = "Ticket Required", Description = "Guests must bring their tickets for entry." },
                new Requirements { Id = 12, Name = "Child-Friendly", Description = "Children are welcome but must be supervised at all times." },
                new Requirements { Id = 13, Name = "Accessible Venue", Description = "The venue is fully accessible for guests with disabilities." },
                new Requirements { Id = 14, Name = "Reusable Utensils", Description = "Guests are encouraged to bring their own reusable utensils to support sustainability." },
                new Requirements { Id = 15, Name = "Meal Preferences", Description = "Guests should specify their meal preferences (e.g., vegetarian, vegan) in advance." }
            );

        }

        public void PopulateAddressData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addresses>().HasData(
            new Addresses { Id = 1, Name = "1", DistrictId = 1, Street = "AKÖREN" },
            new Addresses { Id = 2, Name = "1", DistrictId = 1, Street = "MANSURLU" },
            new Addresses { Id = 3, Name = "1", DistrictId = 1, Street = "SİNANPAŞA" },
            new Addresses { Id = 4, Name = "1", DistrictId = 1, Street = "BAŞPINAR" },
            new Addresses { Id = 5, Name = "1", DistrictId = 1, Street = "AKPINAR" },
            new Addresses { Id = 6, Name = "1", DistrictId = 1, Street = "BOZTAHTA" },
            new Addresses { Id = 7, Name = "1", DistrictId = 1, Street = "BÜYÜKSOFULU" },
            new Addresses { Id = 8, Name = "1", DistrictId = 1, Street = "CERİTLER" },
            new Addresses { Id = 9, Name = "1", DistrictId = 1, Street = "DARILIK" },
            new Addresses { Id = 10, Name = "1", DistrictId = 1, Street = "DAYILAR" },
            new Addresses { Id = 11, Name = "1", DistrictId = 1, Street = "DÖLEKLİ" },
            new Addresses { Id = 12, Name = "1", DistrictId = 1, Street = "EĞNER" },
            new Addresses { Id = 13, Name = "1", DistrictId = 1, Street = "GERDİBİ" },
            new Addresses { Id = 14, Name = "1", DistrictId = 1, Street = "GÖKÇE" },
            new Addresses { Id = 15, Name = "1", DistrictId = 1, Street = "EBRİŞİM" },
            new Addresses { Id = 16, Name = "1", DistrictId = 1, Street = "KABASAKAL" },
            new Addresses { Id = 17, Name = "1", DistrictId = 1, Street = "KARAHAN" },
            new Addresses { Id = 18, Name = "1", DistrictId = 1, Street = "KICAK" },
            new Addresses { Id = 19, Name = "1", DistrictId = 1, Street = "KIŞLAK" },
            new Addresses { Id = 20, Name = "1", DistrictId = 1, Street = "KIZILDAM" },
            new Addresses { Id = 21, Name = "1", DistrictId = 1, Street = "KÖKEZ" },
            new Addresses { Id = 22, Name = "1", DistrictId = 1, Street = "KÖPRÜCÜK" },
            new Addresses { Id = 23, Name = "1", DistrictId = 1, Street = "KÜP" },
            new Addresses { Id = 24, Name = "1", DistrictId = 1, Street = "MADENLİ" },
            new Addresses { Id = 25, Name = "1", DistrictId = 1, Street = "MAZILIK" },
            new Addresses { Id = 26, Name = "1", DistrictId = 1, Street = "TOPALLI" },
            new Addresses { Id = 27, Name = "1", DistrictId = 1, Street = "UZUNKUYU" },
            new Addresses { Id = 28, Name = "1", DistrictId = 1, Street = "POSYAĞBASAN" },
            new Addresses { Id = 29, Name = "1", DistrictId = 1, Street = "GİREĞİYENİKÖY" },
            new Addresses { Id = 30, Name = "1", DistrictId = 1, Street = "YETİMLİ" },
            new Addresses { Id = 31, Name = "1", DistrictId = 1, Street = "YÜKSEKÖREN" },
            new Addresses { Id = 32, Name = "1", DistrictId = 2, Street = "AĞAÇPINAR" },
            new Addresses { Id = 33, Name = "1", DistrictId = 2, Street = "ALTIKARA" },
            new Addresses { Id = 34, Name = "1", DistrictId = 2, Street = "AZİZLİ" },
            new Addresses { Id = 35, Name = "1", DistrictId = 2, Street = "BÜYÜKBURHANİYE" },
            new Addresses { Id = 36, Name = "1", DistrictId = 2, Street = "ÇAKALDERE" },
            new Addresses { Id = 37, Name = "1", DistrictId = 2, Street = "ÇEVRETEPE" },
            new Addresses { Id = 38, Name = "1", DistrictId = 2, Street = "ÇİÇEKLİ" },
            new Addresses { Id = 39, Name = "1", DistrictId = 2, Street = "ÇİFTLİKLER" },
            new Addresses { Id = 40, Name = "1", DistrictId = 2, Street = "ÇOKÇAPINAR" },
            new Addresses { Id = 41, Name = "1", DistrictId = 2, Street = "DEĞİRMENDERE" },
            new Addresses { Id = 42, Name = "1", DistrictId = 2, Street = "DOKUZTEKNE" },
            new Addresses { Id = 43, Name = "1", DistrictId = 2, Street = "DURHASANDEDE" },
            new Addresses { Id = 44, Name = "1", DistrictId = 2, Street = "DUTLUPINAR" },
            new Addresses { Id = 45, Name = "1", DistrictId = 2, Street = "ERENLER" },
            new Addresses { Id = 46, Name = "1", DistrictId = 2, Street = "GÜNDOĞAN" },
            new Addresses { Id = 47, Name = "1", DistrictId = 2, Street = "VEYSİYE " },
            new Addresses { Id = 48, Name = "1", DistrictId = 2, Street = "HAMİDİYE" },
            new Addresses { Id = 49, Name = "1", DistrictId = 2, Street = "HAMİTBEY" },
            new Addresses { Id = 50, Name = "1", DistrictId = 2, Street = "HAMİTBEYBUCAĞI" },
            new Addresses { Id = 51, Name = "1", DistrictId = 2, Street = "İMRAN" },
            new Addresses { Id = 52, Name = "1", DistrictId = 2, Street = "MERCİN" },
            new Addresses { Id = 53, Name = "1", DistrictId = 2, Street = "İSALI" },
            new Addresses { Id = 54, Name = "1", DistrictId = 2, Street = "KILIÇKAYA" },
            new Addresses { Id = 55, Name = "1", DistrictId = 2, Street = "KIZILDERE" },
            new Addresses { Id = 56, Name = "1", DistrictId = 2, Street = "KÖPRÜLÜ" },
            new Addresses { Id = 57, Name = "1", DistrictId = 2, Street = "KÖRKUYU" },
            new Addresses { Id = 58, Name = "1", DistrictId = 2, Street = "KUZUCAK" },
            new Addresses { Id = 59, Name = "1", DistrictId = 2, Street = "KÜÇÜKBURHANİYE" },
            new Addresses { Id = 60, Name = "1", DistrictId = 2, Street = "KÜÇÜKMANGIT" },
            new Addresses { Id = 61, Name = "1", DistrictId = 2, Street = "NARLIK" },
            new Addresses { Id = 62, Name = "1", DistrictId = 2, Street = "SAĞIRLAR" },
            new Addresses { Id = 63, Name = "1", DistrictId = 2, Street = "SELİMİYE" },
            new Addresses { Id = 64, Name = "1", DistrictId = 2, Street = "SİRKELİ" },
            new Addresses { Id = 65, Name = "1", DistrictId = 2, Street = "SOĞUKPINAR" },
            new Addresses { Id = 66, Name = "1", DistrictId = 2, Street = "TOKTAMIŞ" },
            new Addresses { Id = 67, Name = "1", DistrictId = 2, Street = "YELLİBEL" },
            new Addresses { Id = 68, Name = "1", DistrictId = 2, Street = "NAZIMBEY YENİKÖY" },
            new Addresses { Id = 69, Name = "1", DistrictId = 2, Street = "YILANKALE" },
            new Addresses { Id = 70, Name = "1", DistrictId = 2, Street = "ADAPINAR" },
            new Addresses { Id = 71, Name = "1", DistrictId = 2, Street = "ALTIGÖZBEKİRLİ" },
            new Addresses { Id = 72, Name = "1", DistrictId = 2, Street = "BURHANLI" },
            new Addresses { Id = 73, Name = "1", DistrictId = 2, Street = "CEYHANBEKİRLİ" },
            new Addresses { Id = 74, Name = "1", DistrictId = 2, Street = "ÇATAKLI" },
            new Addresses { Id = 75, Name = "1", DistrictId = 2, Street = "DAĞISTAN" },
            new Addresses { Id = 76, Name = "1", DistrictId = 2, Street = "DEĞİRMENLİ" },
            new Addresses { Id = 77, Name = "1", DistrictId = 2, Street = "DİKİLİTAŞ" },
            new Addresses { Id = 78, Name = "1", DistrictId = 2, Street = "EKİNYAZI" },
            new Addresses { Id = 79, Name = "1", DistrictId = 2, Street = "ELMAGÖLÜ" },
            new Addresses { Id = 80, Name = "1", DistrictId = 2, Street = "GÜNLÜCE" },
            new Addresses { Id = 81, Name = "1", DistrictId = 2, Street = "IRMAKLI" },
            new Addresses { Id = 82, Name = "1", DistrictId = 2, Street = "KARAKAYALI" },
            new Addresses { Id = 83, Name = "1", DistrictId = 2, Street = "TATARLI" },
            new Addresses { Id = 84, Name = "1", DistrictId = 2, Street = "YALAK" },
            new Addresses { Id = 85, Name = "1", DistrictId = 2, Street = "AĞAÇLI" },
            new Addresses { Id = 86, Name = "1", DistrictId = 2, Street = "AKDAM" },
            new Addresses { Id = 87, Name = "1", DistrictId = 2, Street = "BAŞÖREN" },
            new Addresses { Id = 88, Name = "1", DistrictId = 2, Street = "CAMUZAĞILI" },
            new Addresses { Id = 89, Name = "1", DistrictId = 2, Street = "ÇATALHÜYÜK" },
            new Addresses { Id = 90, Name = "1", DistrictId = 2, Street = "TUMLU" },
            new Addresses { Id = 91, Name = "1", DistrictId = 2, Street = "GÜMÜRDÜLÜ" },
            new Addresses { Id = 92, Name = "1", DistrictId = 2, Street = "ISIRGANLI" },
            new Addresses { Id = 93, Name = "1", DistrictId = 2, Street = "İNCEYER" },
            new Addresses { Id = 94, Name = "1", DistrictId = 2, Street = "KIVRIKLI" },
            new Addresses { Id = 95, Name = "1", DistrictId = 2, Street = "SARIBAHÇE" },
            new Addresses { Id = 96, Name = "1", DistrictId = 2, Street = "SOYSALLI" },
            new Addresses { Id = 97, Name = "1", DistrictId = 2, Street = "TATLIKUYU" },
            new Addresses { Id = 98, Name = "1", DistrictId = 2, Street = "ÜÇDUTYEŞİLOVA" },
            new Addresses { Id = 99, Name = "1", DistrictId = 2, Street = "YEŞİLBAHÇE" },
            new Addresses { Id = 100, Name = "1", DistrictId = 2, Street = "YEŞİLDAM" }
            );
        }

        public void PopulateEventData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Events>().HasData(
                new Events
                {
                    Id = 1,
                    CreatorId = "10",
                    AddressId = 69,
                    Date = new DateTime(2025, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Dessert Delights",
                    BodyText = "Indulge in an evening of seductive desserts with a guided tasting crafted for every palate."
                },
                new Events
                {
                    Id = 2,
                    CreatorId = "3",
                    AddressId = 49,
                    Date = new DateTime(2025, 3, 21, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Grill Master ShowDown",
                    BodyText = "Join us for a sizzling BBQ battle, where culinary masters fire up their grills for mouthwatering cre"
                },
                new Events
                {
                    Id = 3,
                    CreatorId = "46",
                    AddressId = 30,
                    Date = new DateTime(2025, 4, 2, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Gastronomic Symphony",
                    BodyText = "Indulge in a culinary extravaganza where flavors dance on your palate."
                },
                new Events
                {
                    Id = 4,
                    CreatorId = "6",
                    AddressId = 93,
                    Date = new DateTime(2025, 8, 30, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Savor the Flavors: Street Food Market",
                    BodyText = "Indulge in a culinary extravaganza! Join us for a feast of tantalizing dishes, where street food art"
                },
                new Events
                {
                    Id = 5,
                    CreatorId = "49",
                    AddressId = 27,
                    Date = new DateTime(2025, 12, 3, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Culinary Clicks: Food Photography Workshop",
                    BodyText = "Capture the beauty of food with expert tips and techniques for stunning food photography."
                },
                new Events
                {
                    Id = 6,
                    CreatorId = "81",
                    AddressId = 67,
                    Date = new DateTime(2025, 8, 24, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Unveiling the Vine's Secrets",
                    BodyText = "Join us for a tantalizing wine tasting journey, where flavors dance with aromas"
                },
                new Events
                {
                    Id = 7,
                    CreatorId = "15",
                    AddressId = 2,
                    Date = new DateTime(2025, 9, 17, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Savor the Street",
                    BodyText = "Street food market with flavors from around the globe."
                },
                new Events
                {
                    Id = 8,
                    CreatorId = "66",
                    AddressId = 91,
                    Date = new DateTime(2025, 2, 18, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Pasta Perfection in the Kitchen",
                    BodyText = "Craft authentic pasta from scratch with our interactive workshop. Discover secrets and indulge in yo"
                },
                new Events
                {
                    Id = 9,
                    CreatorId = "72",
                    AddressId = 38,
                    Date = new DateTime(2025, 8, 30, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Taste the World",
                    BodyText = "Indulge in global flavors at our vibrant Street Food Market!"
                },
                new Events
                {
                    Id = 10,
                    CreatorId = "43",
                    AddressId = 57,
                    Date = new DateTime(2025, 3, 7, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Culinary Odyssey",
                    BodyText = "Explore a world of flavors at our International Food Fair!"
                },
                new Events
                {
                    Id = 11,
                    CreatorId = "20",
                    AddressId = 21,
                    Date = new DateTime(2025, 1, 29, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Sweet Indulgence: Dessert Extravaganza",
                    BodyText = "Indulge in a symphony of flavors as we present an array of delectable desserts, crafted with love an"
                },
                new Events
                {
                    Id = 12,
                    CreatorId = "54",
                    AddressId = 19,
                    Date = new DateTime(2025, 7, 6, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Sushi Sensations: A Culinary Journey",
                    BodyText = "Immerse yourself in the art of sushi making with expert guidance. Craft your own delicious creations"
                },
                new Events
                {
                    Id = 13,
                    CreatorId = "8",
                    AddressId = 34,
                    Date = new DateTime(2025, 6, 12, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Pasta Perfection",
                    BodyText = "Join us for a hands-on pasta making workshop. Discover the art of crafting delicious fresh pasta fro"
                },
                new Events
                {
                    Id = 14,
                    CreatorId = "49",
                    AddressId = 1,
                    Date = new DateTime(2025, 6, 22, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Taste the World on Every Corner",
                    BodyText = "Indulge in a culinary journey as street food vendors from around the globe gather to tantalize your "
                },
                new Events
                {
                    Id = 15,
                    CreatorId = "57",
                    AddressId = 48,
                    Date = new DateTime(2025, 2, 26, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Sizzling Showdown: BBQ Extravaganza",
                    BodyText = "Get ready for a feast of epic proportions! Join us for a grilling competition where BBQ masters show"
                },
                new Events
                {
                    Id = 16,
                    CreatorId = "47",
                    AddressId = 18,
                    Date = new DateTime(2025, 11, 20, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Culinary Crossroads: A Global Feast",
                    BodyText = "Savor flavors from every corner, as culinary artisans gather to showcase their finest creations."
                },
                new Events
                {
                    Id = 17,
                    CreatorId = "57",
                    AddressId = 56,
                    Date = new DateTime(2025, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Chef's Table Dinner Culinary Experience",
                    BodyText = "Join us for an unforgettable Chef's Table Dinner food experience!"
                },
                new Events
                {
                    Id = 18,
                    CreatorId = "91",
                    AddressId = 78,
                    Date = new DateTime(2025, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Feast of Flavors",
                    BodyText = "An exquisite culinary journey awaits, where master chefs unveil their most tantalizing creations."
                },
                new Events
                {
                    Id = 19,
                    CreatorId = "35",
                    AddressId = 38,
                    Date = new DateTime(2025, 12, 12, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Street Food Market Culinary Experience",
                    BodyText = "Join us for an unforgettable Street Food Market food experience!"
                },
                new Events
                {
                    Id = 20,
                    CreatorId = "19",
                    AddressId = 7,
                    Date = new DateTime(2025, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Culinary Expedition: Flavors from Afar",
                    BodyText = "Embark on a tantalizing culinary journey as local and international chefs showcase their masterful d"
                },
                new Events
                {
                    Id = 21,
                    CreatorId = "8",
                    AddressId = 74,
                    Date = new DateTime(2025, 5, 13, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Farm-to-Table Dinner Culinary Experience",
                    BodyText = "Join us for an unforgettable Farm-to-Table Dinner food experience!"
                },
                new Events
                {
                    Id = 22,
                    CreatorId = "85",
                    AddressId = 1,
                    Date = new DateTime(2025, 5, 2, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Sushi Making Class Culinary Experience",
                    BodyText = "Join us for an unforgettable Sushi Making Class food experience!"
                },
                new Events
                {
                    Id = 23,
                    CreatorId = "26",
                    AddressId = 5,
                    Date = new DateTime(2025, 1, 14, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Baking Masterclass Culinary Experience",
                    BodyText = "Join us for an unforgettable Baking Masterclass food experience!"
                },
                new Events
                {
                    Id = 24,
                    CreatorId = "77",
                    AddressId = 90,
                    Date = new DateTime(2025, 11, 26, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Pasta Making Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Pasta Making Workshop food experience!"
                },
                new Events
                {
                    Id = 25,
                    CreatorId = "43",
                    AddressId = 31,
                    Date = new DateTime(2025, 7, 30, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Pasta Making Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Pasta Making Workshop food experience!"
                },
                new Events
                {
                    Id = 26,
                    CreatorId = "33",
                    AddressId = 96,
                    Date = new DateTime(2025, 11, 9, 0, 0, 0, DateTimeKind.Utc),
                    Title = "International Food Fair Culinary Experience",
                    BodyText = "Join us for an unforgettable International Food Fair food experience!"
                },
                new Events
                {
                    Id = 27,
                    CreatorId = "38",
                    AddressId = 1,
                    Date = new DateTime(2025, 4, 24, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Food Photography Class Culinary Experience",
                    BodyText = "Join us for an unforgettable Food Photography Class food experience!"
                },
                new Events
                {
                    Id = 28,
                    CreatorId = "62",
                    AddressId = 18,
                    Date = new DateTime(2025, 3, 21, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Sushi Making Class Culinary Experience",
                    BodyText = "Join us for an unforgettable Sushi Making Class food experience!"
                },
                new Events
                {
                    Id = 29,
                    CreatorId = "38",
                    AddressId = 61,
                    Date = new DateTime(2025, 10, 9, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Food Photography Class Culinary Experience",
                    BodyText = "Join us for an unforgettable Food Photography Class food experience!"
                },
                new Events
                {
                    Id = 30,
                    CreatorId = "92",
                    AddressId = 98,
                    Date = new DateTime(2025, 10, 6, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Pasta Making Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Pasta Making Workshop food experience!"
                },
                new Events
                {
                    Id = 31,
                    CreatorId = "34",
                    AddressId = 45,
                    Date = new DateTime(2025, 12, 17, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Craft Beer Tasting Culinary Experience",
                    BodyText = "Join us for an unforgettable Craft Beer Tasting food experience!"
                },
                new Events
                {
                    Id = 32,
                    CreatorId = "27",
                    AddressId = 37,
                    Date = new DateTime(2025, 3, 20, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Street Food Market Culinary Experience",
                    BodyText = "Join us for an unforgettable Street Food Market food experience!"
                },
                new Events
                {
                    Id = 33,
                    CreatorId = "20",
                    AddressId = 85,
                    Date = new DateTime(2025, 7, 4, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Pasta Making Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Pasta Making Workshop food experience!"
                },
                new Events
                {
                    Id = 34,
                    CreatorId = "34",
                    AddressId = 80,
                    Date = new DateTime(2025, 12, 24, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Craft Beer Tasting Culinary Experience",
                    BodyText = "Join us for an unforgettable Craft Beer Tasting food experience!"
                },
                new Events
                {
                    Id = 35,
                    CreatorId = "71",
                    AddressId = 62,
                    Date = new DateTime(2025, 10, 9, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Sushi Making Class Culinary Experience",
                    BodyText = "Join us for an unforgettable Sushi Making Class food experience!"
                },
                new Events
                {
                    Id = 36,
                    CreatorId = "49",
                    AddressId = 31,
                    Date = new DateTime(2025, 8, 12, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Pasta Making Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Pasta Making Workshop food experience!"
                },
                new Events
                {
                    Id = 37,
                    CreatorId = "37",
                    AddressId = 55,
                    Date = new DateTime(2025, 4, 29, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Craft Beer Tasting Culinary Experience",
                    BodyText = "Join us for an unforgettable Craft Beer Tasting food experience!"
                },
                new Events
                {
                    Id = 38,
                    CreatorId = "58",
                    AddressId = 74,
                    Date = new DateTime(2025, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Food Festival Culinary Experience",
                    BodyText = "Join us for an unforgettable Food Festival food experience!"
                },
                new Events
                {
                    Id = 39,
                    CreatorId = "87",
                    AddressId = 91,
                    Date = new DateTime(2025, 9, 26, 0, 0, 0, DateTimeKind.Utc),
                    Title = "International Food Fair Culinary Experience",
                    BodyText = "Join us for an unforgettable International Food Fair food experience!"
                },
                new Events
                {
                    Id = 40,
                    CreatorId = "77",
                    AddressId = 21,
                    Date = new DateTime(2025, 2, 2, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Chocolate Making Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Chocolate Making Workshop food experience!"
                },
                new Events
                {
                    Id = 41,
                    CreatorId = "26",
                    AddressId = 54,
                    Date = new DateTime(2025, 6, 7, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Cooking Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Cooking Workshop food experience!"
                },
                new Events
                {
                    Id = 42,
                    CreatorId = "77",
                    AddressId = 12,
                    Date = new DateTime(2025, 9, 3, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Craft Beer Tasting Culinary Experience",
                    BodyText = "Join us for an unforgettable Craft Beer Tasting food experience!"
                },
                new Events
                {
                    Id = 43,
                    CreatorId = "67",
                    AddressId = 23,
                    Date = new DateTime(2025, 7, 29, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Craft Beer Tasting Culinary Experience",
                    BodyText = "Join us for an unforgettable Craft Beer Tasting food experience!"
                },
                new Events
                {
                    Id = 44,
                    CreatorId = "65",
                    AddressId = 89,
                    Date = new DateTime(2025, 9, 19, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Food Festival Culinary Experience",
                    BodyText = "Join us for an unforgettable Food Festival food experience!"
                },
                new Events
                {
                    Id = 45,
                    CreatorId = "78",
                    AddressId = 65,
                    Date = new DateTime(2025, 9, 19, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Food Festival Culinary Experience",
                    BodyText = "Join us for an unforgettable Food Festival food experience!"
                },
                new Events
                {
                    Id = 46,
                    CreatorId = "17",
                    AddressId = 20,
                    Date = new DateTime(2025, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Dessert Tasting Culinary Experience",
                    BodyText = "Join us for an unforgettable Dessert Tasting food experience!"
                },
                new Events
                {
                    Id = 47,
                    CreatorId = "82",
                    AddressId = 30,
                    Date = new DateTime(2025, 5, 12, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Farm-to-Table Dinner Culinary Experience",
                    BodyText = "Join us for an unforgettable Farm-to-Table Dinner food experience!"
                },
                new Events
                {
                    Id = 48,
                    CreatorId = "85",
                    AddressId = 40,
                    Date = new DateTime(2025, 6, 20, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Street Food Market Culinary Experience",
                    BodyText = "Join us for an unforgettable Street Food Market food experience!"
                },
                new Events
                {
                    Id = 49,
                    CreatorId = "93",
                    AddressId = 47,
                    Date = new DateTime(2025, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Craft Beer Tasting Culinary Experience",
                    BodyText = "Join us for an unforgettable Craft Beer Tasting food experience!"
                },
                new Events
                {
                    Id = 50,
                    CreatorId = "72",
                    AddressId = 85,
                    Date = new DateTime(2025, 4, 12, 0, 0, 0, DateTimeKind.Utc),
                    Title = "BBQ Competition Culinary Experience",
                    BodyText = "Join us for an unforgettable BBQ Competition food experience!"
                },
                new Events
                {
                    Id = 51,
                    CreatorId = "87",
                    AddressId = 86,
                    Date = new DateTime(2025, 2, 2, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Craft Beer Tasting Culinary Experience",
                    BodyText = "Join us for an unforgettable Craft Beer Tasting food experience!"
                },
                new Events
                {
                    Id = 52,
                    CreatorId = "92",
                    AddressId = 13,
                    Date = new DateTime(2025, 11, 11, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Cooking Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Cooking Workshop food experience!"
                },
                new Events
                {
                    Id = 53,
                    CreatorId = "34",
                    AddressId = 81,
                    Date = new DateTime(2025, 2, 7, 0, 0, 0, DateTimeKind.Utc),
                    Title = "BBQ Competition Culinary Experience",
                    BodyText = "Join us for an unforgettable BBQ Competition food experience!"
                },
                new Events
                {
                    Id = 54,
                    CreatorId = "98",
                    AddressId = 45,
                    Date = new DateTime(2025, 11, 3, 0, 0, 0, DateTimeKind.Utc),
                    Title = "BBQ Competition Culinary Experience",
                    BodyText = "Join us for an unforgettable BBQ Competition food experience!"
                },
                new Events
                {
                    Id = 55,
                    CreatorId = "45",
                    AddressId = 81,
                    Date = new DateTime(2025, 2, 17, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Sushi Making Class Culinary Experience",
                    BodyText = "Join us for an unforgettable Sushi Making Class food experience!"
                },
                new Events
                {
                    Id = 56,
                    CreatorId = "31",
                    AddressId = 90,
                    Date = new DateTime(2025, 3, 18, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Farm-to-Table Dinner Culinary Experience",
                    BodyText = "Join us for an unforgettable Farm-to-Table Dinner food experience!"
                },
                new Events
                {
                    Id = 57,
                    CreatorId = "6",
                    AddressId = 64,
                    Date = new DateTime(2025, 8, 2, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Chocolate Making Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Chocolate Making Workshop food experience!"
                },
                new Events
                {
                    Id = 58,
                    CreatorId = "89",
                    AddressId = 29,
                    Date = new DateTime(2025, 2, 10, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Dessert Tasting Culinary Experience",
                    BodyText = "Join us for an unforgettable Dessert Tasting food experience!"
                },
                new Events
                {
                    Id = 59,
                    CreatorId = "45",
                    AddressId = 33,
                    Date = new DateTime(2025, 3, 26, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Baking Masterclass Culinary Experience",
                    BodyText = "Join us for an unforgettable Baking Masterclass food experience!"
                },
                new Events
                {
                    Id = 60,
                    CreatorId = "16",
                    AddressId = 86,
                    Date = new DateTime(2025, 2, 17, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Wine Tasting Culinary Experience",
                    BodyText = "Join us for an unforgettable Wine Tasting food experience!"
                },
                new Events
                {
                    Id = 61,
                    CreatorId = "58",
                    AddressId = 73,
                    Date = new DateTime(2025, 9, 26, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Chef's Table Dinner Culinary Experience",
                    BodyText = "Join us for an unforgettable Chef's Table Dinner food experience!"
                },
                new Events
                {
                    Id = 62,
                    CreatorId = "49",
                    AddressId = 66,
                    Date = new DateTime(2025, 8, 14, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Chef's Table Dinner Culinary Experience",
                    BodyText = "Join us for an unforgettable Chef's Table Dinner food experience!"
                },
                new Events
                {
                    Id = 63,
                    CreatorId = "41",
                    AddressId = 60,
                    Date = new DateTime(2025, 7, 30, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Pasta Making Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Pasta Making Workshop food experience!"
                },
                new Events
                {
                    Id = 64,
                    CreatorId = "79",
                    AddressId = 65,
                    Date = new DateTime(2025, 8, 10, 0, 0, 0, DateTimeKind.Utc),
                    Title = "International Food Fair Culinary Experience",
                    BodyText = "Join us for an unforgettable International Food Fair food experience!"
                },
                new Events
                {
                    Id = 65,
                    CreatorId = "86",
                    AddressId = 35,
                    Date = new DateTime(2025, 11, 5, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Street Food Market Culinary Experience",
                    BodyText = "Join us for an unforgettable Street Food Market food experience!"
                },
                new Events
                {
                    Id = 66,
                    CreatorId = "65",
                    AddressId = 74,
                    Date = new DateTime(2025, 2, 6, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Pasta Making Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Pasta Making Workshop food experience!"
                },
                new Events
                {
                    Id = 67,
                    CreatorId = "81",
                    AddressId = 16,
                    Date = new DateTime(2025, 1, 11, 0, 0, 0, DateTimeKind.Utc),
                    Title = "BBQ Competition Culinary Experience",
                    BodyText = "Join us for an unforgettable BBQ Competition food experience!"
                },
                new Events
                {
                    Id = 68,
                    CreatorId = "42",
                    AddressId = 97,
                    Date = new DateTime(2025, 5, 25, 0, 0, 0, DateTimeKind.Utc),
                    Title = "International Food Fair Culinary Experience",
                    BodyText = "Join us for an unforgettable International Food Fair food experience!"
                },
                new Events
                {
                    Id = 69,
                    CreatorId = "88",
                    AddressId = 89,
                    Date = new DateTime(2025, 3, 29, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Farm-to-Table Dinner Culinary Experience",
                    BodyText = "Join us for an unforgettable Farm-to-Table Dinner food experience!"
                },
                new Events
                {
                    Id = 70,
                    CreatorId = "100",
                    AddressId = 16,
                    Date = new DateTime(2025, 11, 24, 0, 0, 0, DateTimeKind.Utc),
                    Title = "International Food Fair Culinary Experience",
                    BodyText = "Join us for an unforgettable International Food Fair food experience!"
                },
                new Events
                {
                    Id = 71,
                    CreatorId = "98",
                    AddressId = 25,
                    Date = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Culinary Canvas: Food Photography Masterclass",
                    BodyText = "Learn food photography secrets from a pro and elevate your culinary shots."
                },
                new Events
                {
                    Id = 72,
                    CreatorId = "70",
                    AddressId = 54,
                    Date = new DateTime(2025, 9, 18, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Chef's Table Dinner Culinary Experience",
                    BodyText = "Join us for an unforgettable Chef's Table Dinner food experience!"
                },
                new Events
                {
                    Id = 73,
                    CreatorId = "91",
                    AddressId = 9,
                    Date = new DateTime(2025, 2, 14, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Chocolate's Sweet Symphony",
                    BodyText = "Unleash your inner chocolatier and craft delectable treats!"
                },
                new Events
                {
                    Id = 74,
                    CreatorId = "62",
                    AddressId = 82,
                    Date = new DateTime(2025, 9, 28, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Unveiling the Flavors of Wine and Cuisine",
                    BodyText = "Join us for an exquisite evening of wine tasting, savoring the perfect pairings with delectable culi"
                },
                new Events
                {
                    Id = 75,
                    CreatorId = "60",
                    AddressId = 58,
                    Date = new DateTime(2025, 7, 9, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Street Food Extravaganza",
                    BodyText = "Savor global flavors & culinary delights from around the world!"
                },
                new Events
                {
                    Id = 76,
                    CreatorId = "85",
                    AddressId = 32,
                    Date = new DateTime(2025, 6, 24, 0, 0, 0, DateTimeKind.Utc),
                    Title = "An Evening at the Chef's Table",
                    BodyText = "Indulge in an intimate dining experience. Watch our chefs craft a masterpiece before your eyes."
                },
                new Events
                {
                    Id = 77,
                    CreatorId = "89",
                    AddressId = 12,
                    Date = new DateTime(2025, 11, 9, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Culinary Creations Festival",
                    BodyText = "Join renowned chefs for cooking demos and hands-on workshops exploring flavors from around the globe"
                },
                new Events
                {
                    Id = 78,
                    CreatorId = "1",
                    AddressId = 83,
                    Date = new DateTime(2025, 3, 30, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Culinary Extravaganza",
                    BodyText = "Embark on a culinary journey through diverse flavors, cooking demos, and hands-on experiences."
                },
                new Events
                {
                    Id = 79,
                    CreatorId = "71",
                    AddressId = 36,
                    Date = new DateTime(2025, 7, 10, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Feast from the Farm",
                    BodyText = "Join us for a culinary journey where farm-fresh flavors take center stage."
                },
                new Events
                {
                    Id = 80,
                    CreatorId = "58",
                    AddressId = 17,
                    Date = new DateTime(2025, 11, 7, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Savor the Street",
                    BodyText = "Indulge in culinary delights from around the globe at our vibrant Street Food Market."
                },
                new Events
                {
                    Id = 81,
                    CreatorId = "85",
                    AddressId = 92,
                    Date = new DateTime(2025, 8, 26, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Chocolate Crafting Extravaganza",
                    BodyText = "Indulge in a hands-on adventure, crafting exquisite chocolates from scratch!"
                },
                new Events
                {
                    Id = 82,
                    CreatorId = "92",
                    AddressId = 71,
                    Date = new DateTime(2025, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Culinary Odyssey: Farm-to-Fork Feast",
                    BodyText = "Savor the freshest flavors as local ingredients dance on your plate."
                },
                new Events
                {
                    Id = 83,
                    CreatorId = "70",
                    AddressId = 97,
                    Date = new DateTime(2025, 11, 7, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Pasta Making Mastery",
                    BodyText = "Join our hands-on workshop and learn the art of crafting authentic Italian pasta from scratch."
                },
                new Events
                {
                    Id = 84,
                    CreatorId = "69",
                    AddressId = 43,
                    Date = new DateTime(2025, 6, 16, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Pasta Proficiency Workshop",
                    BodyText = "Join us as we master the art of crafting fresh, homemade pasta from scratch."
                },
                new Events
                {
                    Id = 85,
                    CreatorId = "67",
                    AddressId = 100,
                    Date = new DateTime(2025, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Culinary Canvas: Food Photography Masterclass",
                    BodyText = "Capture the art of cooking through the lens. Learn techniques to elevate your food photography skill"
                },
                new Events
                {
                    Id = 86,
                    CreatorId = "30",
                    AddressId = 91,
                    Date = new DateTime(2025, 2, 18, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Culinary Delights",
                    BodyText = "Savor a weekend of exquisite cuisine, cooking demos, and culinary workshops."
                },
                new Events
                {
                    Id = 87,
                    CreatorId = "22",
                    AddressId = 48,
                    Date = new DateTime(2025, 9, 2, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Savor the World's Flavors",
                    BodyText = "Embark on a culinary adventure, exploring diverse cuisines and tantalizing tastes from every corner "
                },
                new Events
                {
                    Id = 88,
                    CreatorId = "46",
                    AddressId = 96,
                    Date = new DateTime(2025, 8, 23, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Culinary Canvas: An Immersive Dining Experience",
                    BodyText = "Witness culinary artistry unfold as our chef paints flavors on your plate."
                },
                new Events
                {
                    Id = 89,
                    CreatorId = "66",
                    AddressId = 71,
                    Date = new DateTime(2025, 7, 22, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Smoky 'Cue Showdown",
                    BodyText = "Grab your forks and gather 'round for a sizzling BBQ competition where pitmasters ignite taste buds."
                },
                new Events
                {
                    Id = 90,
                    CreatorId = "48",
                    AddressId = 87,
                    Date = new DateTime(2025, 5, 31, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Cooking Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Cooking Workshop food experience!"
                },
                new Events
                {
                    Id = 91,
                    CreatorId = "96",
                    AddressId = 28,
                    Date = new DateTime(2025, 8, 9, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Cooking Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Cooking Workshop food experience!"
                },
                new Events
                {
                    Id = 92,
                    CreatorId = "24",
                    AddressId = 75,
                    Date = new DateTime(2025, 7, 4, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Chef's Table Dinner Culinary Experience",
                    BodyText = "Join us for an unforgettable Chef's Table Dinner food experience!"
                },
                new Events
                {
                    Id = 93,
                    CreatorId = "46",
                    AddressId = 36,
                    Date = new DateTime(2025, 7, 21, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Food Festival Culinary Experience",
                    BodyText = "Join us for an unforgettable Food Festival food experience!"
                },
                new Events
                {
                    Id = 94,
                    CreatorId = "66",
                    AddressId = 89,
                    Date = new DateTime(2025, 11, 25, 0, 0, 0, DateTimeKind.Utc),
                    Title = "BBQ Competition Culinary Experience",
                    BodyText = "Join us for an unforgettable BBQ Competition food experience!"
                },
                new Events
                {
                    Id = 95,
                    CreatorId = "45",
                    AddressId = 98,
                    Date = new DateTime(2025, 10, 20, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Craft Beer Tasting Culinary Experience",
                    BodyText = "Join us for an unforgettable Craft Beer Tasting food experience!"
                },
                new Events
                {
                    Id = 96,
                    CreatorId = "58",
                    AddressId = 97,
                    Date = new DateTime(2025, 7, 23, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Cooking Workshop Culinary Experience",
                    BodyText = "Join us for an unforgettable Cooking Workshop food experience!"
                },
                new Events
                {
                    Id = 97,
                    CreatorId = "56",
                    AddressId = 31,
                    Date = new DateTime(2025, 6, 13, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Chef's Table Dinner Culinary Experience",
                    BodyText = "Join us for an unforgettable Chef's Table Dinner food experience!"
                },
                new Events
                {
                    Id = 98,
                    CreatorId = "52",
                    AddressId = 39,
                    Date = new DateTime(2025, 12, 21, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Baking Masterclass Culinary Experience",
                    BodyText = "Join us for an unforgettable Baking Masterclass food experience!"
                },
                new Events
                {
                    Id = 99,
                    CreatorId = "83",
                    AddressId = 77,
                    Date = new DateTime(2025, 10, 14, 0, 0, 0, DateTimeKind.Utc),
                    Title = "Food Photography Class Culinary Experience",
                    BodyText = "Join us for an unforgettable Food Photography Class food experience!"
                },
                new Events
                {
                    Id = 100,
                    CreatorId = "28",
                    AddressId = 60,
                    Date = new DateTime(2025, 4, 11, 0, 0, 0, DateTimeKind.Utc),
                    Title = "International Food Fair Culinary Experience",
                    BodyText = "Join us for an unforgettable International Food Fair food experience!"
                }
            );
        }
    }
}
