using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class IngredientsDefaultData
    {
        public void PopulateIngredientTypesData(ModelBuilder modelBuilder)
        {
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

        public void PopulateIngredientsData(ModelBuilder modelBuilder)
        {
           
        }


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
                new Ingredients_Preferences { Id = 1, IngredientId = 61, PreferenceId = 1 },
                new Ingredients_Preferences { Id = 2, IngredientId = 62, PreferenceId = 1 },
                new Ingredients_Preferences { Id = 3, IngredientId = 63, PreferenceId = 1 },
                new Ingredients_Preferences { Id = 4, IngredientId = 64, PreferenceId = 1 },
                new Ingredients_Preferences { Id = 5, IngredientId = 65, PreferenceId = 1 },
                new Ingredients_Preferences { Id = 6, IngredientId = 66, PreferenceId = 1 },
                new Ingredients_Preferences { Id = 7, IngredientId = 67, PreferenceId = 1 },
                new Ingredients_Preferences { Id = 8, IngredientId = 68, PreferenceId = 1 },
                new Ingredients_Preferences { Id = 9, IngredientId = 69, PreferenceId = 1 },
                new Ingredients_Preferences { Id = 10, IngredientId = 70, PreferenceId = 1 },
                new Ingredients_Preferences { Id = 11, IngredientId = 48, PreferenceId = 2 },
                new Ingredients_Preferences { Id = 12, IngredientId = 136, PreferenceId = 2 },
                new Ingredients_Preferences { Id = 13, IngredientId = 45, PreferenceId = 3 },
                new Ingredients_Preferences { Id = 14, IngredientId = 47, PreferenceId = 3 },
                new Ingredients_Preferences { Id = 15, IngredientId = 56, PreferenceId = 3 },
                new Ingredients_Preferences { Id = 16, IngredientId = 58, PreferenceId = 3 },
                new Ingredients_Preferences { Id = 17, IngredientId = 46, PreferenceId = 4 },
                new Ingredients_Preferences { Id = 18, IngredientId = 57, PreferenceId = 4 },
                new Ingredients_Preferences { Id = 19, IngredientId = 60, PreferenceId = 4 },
                new Ingredients_Preferences { Id = 20, IngredientId = 81, PreferenceId = 5 },
                new Ingredients_Preferences { Id = 21, IngredientId = 82, PreferenceId = 5 },
                new Ingredients_Preferences { Id = 22, IngredientId = 83, PreferenceId = 5 },
                new Ingredients_Preferences { Id = 23, IngredientId = 84, PreferenceId = 5 },
                new Ingredients_Preferences { Id = 24, IngredientId = 141, PreferenceId = 5 },
                new Ingredients_Preferences { Id = 25, IngredientId = 85, PreferenceId = 6 },
                new Ingredients_Preferences { Id = 26, IngredientId = 140, PreferenceId = 6 },
                new Ingredients_Preferences { Id = 27, IngredientId = 72, PreferenceId = 7 },
                new Ingredients_Preferences { Id = 28, IngredientId = 75, PreferenceId = 7 },
                new Ingredients_Preferences { Id = 29, IngredientId = 49, PreferenceId = 8 },
                new Ingredients_Preferences { Id = 30, IngredientId = 50, PreferenceId = 8 },
                new Ingredients_Preferences { Id = 31, IngredientId = 131, PreferenceId = 8 },
                new Ingredients_Preferences { Id = 32, IngredientId = 137, PreferenceId = 8 },
                new Ingredients_Preferences { Id = 33, IngredientId = 86, PreferenceId = 9 },
                new Ingredients_Preferences { Id = 34, IngredientId = 114, PreferenceId = 9 },
                new Ingredients_Preferences { Id = 35, IngredientId = 138, PreferenceId = 9 }
            );

        }


    }
}
