using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class PreferencesDefaultData
    {
        public void PopulatePreferencesData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Preferences>().HasData(
               new Preferences { Id = 1, Name = "Milk", Description = "Includes all milk products, such as milk, cheese, yogurt, butter, and whey." },
               new Preferences { Id = 2, Name = "Eggs", Description = "Includes chicken eggs and any products containing eggs, such as baked goods and mayonnaise." },
               new Preferences { Id = 3, Name = "Fish", Description = "Includes all finned fish such as bass, cod, salmon, tuna, and anchovies." },
               new Preferences { Id = 4, Name = "Crustacean Shellfish", Description = "Includes shrimp, crab, lobster, prawns, and crayfish." },
               new Preferences { Id = 5, Name = "Tree Nuts", Description = "Includes almonds, walnuts, pecans, cashews, macadamia nuts, and hazelnuts. Excludes peanuts." },
               new Preferences { Id = 6, Name = "Peanuts", Description = "Includes peanuts and peanut-containing products, such as peanut butter and peanut oil." },
               new Preferences { Id = 7, Name = "Wheat", Description = "Includes foods containing wheat gluten, such as bread, pasta, and cereals." },
               new Preferences { Id = 8, Name = "Soybeans", Description = "Includes soy and soy-containing products, such as tofu, soy sauce, and edamame." },
               new Preferences { Id = 9, Name = "Sesame", Description = "Includes sesame seeds, sesame oil, and products containing sesame, such as tahini." }
           );
        }

        public void PopulateIngredientPreferencesData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredients_Preferences>().HasData(
                // Milk
                new Ingredients_Preferences { Id = 1, IngredientId = 32, PreferenceId = 1 }, // Cheese
                new Ingredients_Preferences { Id = 2, IngredientId = 24, PreferenceId = 1 }, // Butter
                new Ingredients_Preferences { Id = 3, IngredientId = 49, PreferenceId = 1 }, // Cottage Cheese
                 new Ingredients_Preferences { Id = 4, IngredientId = 51, PreferenceId = 1 }, // Cream cheese
                new Ingredients_Preferences { Id = 5, IngredientId = 52, PreferenceId = 1 }, // Cream
                new Ingredients_Preferences { Id = 6, IngredientId = 83, PreferenceId = 1 }, // Milk
                new Ingredients_Preferences { Id = 7, IngredientId = 117, PreferenceId = 1 }, // Ricotta
                new Ingredients_Preferences { Id = 8, IngredientId = 125, PreferenceId = 1 }, // Sour cream
                new Ingredients_Preferences { Id = 9, IngredientId = 149, PreferenceId = 1 }, // Yogurt


                // Eggs
                new Ingredients_Preferences { Id = 10, IngredientId = 58, PreferenceId = 2 }, // Eggs
                new Ingredients_Preferences { Id = 11, IngredientId = 82, PreferenceId = 2 },// Mayonnaise


                // Fish
                new Ingredients_Preferences { Id = 12, IngredientId = 68, PreferenceId = 3 },// Halibut
                new Ingredients_Preferences { Id = 13, IngredientId = 119, PreferenceId = 3 }, // Salmon
                new Ingredients_Preferences { Id = 14, IngredientId = 120, PreferenceId = 3 }, // Sardines
                new Ingredients_Preferences { Id = 15, IngredientId = 139, PreferenceId = 3 }, // Tuna


               // Crustacean Shellfish
               new Ingredients_Preferences { Id = 16, IngredientId = 79, PreferenceId = 4 },// Lobster
               new Ingredients_Preferences { Id = 17, IngredientId = 121, PreferenceId = 4 },// Scallops
                new Ingredients_Preferences { Id = 18, IngredientId = 124, PreferenceId = 4 }, // Shrimp


                 // Tree Nuts
                 new Ingredients_Preferences { Id = 19, IngredientId = 3, PreferenceId = 5 }, // Almonds
                 new Ingredients_Preferences { Id = 20, IngredientId = 29, PreferenceId = 5 },// Cashews
                 new Ingredients_Preferences { Id = 21, IngredientId = 104, PreferenceId = 5 },// Pecans
                new Ingredients_Preferences { Id = 22, IngredientId = 144, PreferenceId = 5 }, // Walnuts


               // Peanuts
               new Ingredients_Preferences { Id = 23, IngredientId = 100, PreferenceId = 6 },// Peanuts
                new Ingredients_Preferences { Id = 24, IngredientId = 101, PreferenceId = 6 },// Peanut butter


               // Wheat
               new Ingredients_Preferences { Id = 25, IngredientId = 12, PreferenceId = 7 },// Barley
                new Ingredients_Preferences { Id = 26, IngredientId = 22, PreferenceId = 7 },// Bread
                new Ingredients_Preferences { Id = 27, IngredientId = 50, PreferenceId = 7 },// Couscous
                 new Ingredients_Preferences { Id = 28, IngredientId = 98, PreferenceId = 7 },// Pasta

               // Soybeans
               new Ingredients_Preferences { Id = 29, IngredientId = 126, PreferenceId = 8 },// Soy sauce
                new Ingredients_Preferences { Id = 30, IngredientId = 134, PreferenceId = 8 },// Tempeh
               new Ingredients_Preferences { Id = 31, IngredientId = 136, PreferenceId = 8 },// Tofu

               // Sesame
               new Ingredients_Preferences { Id = 32, IngredientId = 122, PreferenceId = 9 },// Sesame oil
               new Ingredients_Preferences { Id = 33, IngredientId = 123, PreferenceId = 9 }, // Sesame seeds
                 new Ingredients_Preferences { Id = 34, IngredientId = 133, PreferenceId = 9 }// Tahini
            );

        }
    }
}
