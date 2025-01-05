using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Data
{
    public class RecipeDefaultData
    {
        public void PopulateRecipeData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipes>().HasData(
                // Recipe 1: Almond Butter Banana Smoothie
                new Recipes
                {
                    Id = 1,
                    UserId = "1",
                    Header = "Almond Butter Banana Smoothie",
                    BodyText = "A quick and easy breakfast smoothie.",
                    BannerImage = null,
                    Steps = ["Combine all ingredients in a blender.", "Blend until smooth.", "Pour into a glass and enjoy!"],
                    StepImages = null,
                    ServingSize = 1,
                    PreparationTime = 5,
                    CreatedAt = DateTime.UtcNow
                },
                // Recipe 2: Chicken Stir Fry
                new Recipes
                {
                    Id = 2,
                    UserId = "3",
                    Header = "Chicken Stir Fry",
                    BodyText = "A healthy and flavorful stir-fry.",
                    BannerImage = null,
                    Steps = ["Cut the chicken and vegetables into bite-sized pieces.", "Heat sesame oil in a wok or large skillet.", "Add chicken and stir-fry until cooked through.", "Add vegetables and stir-fry until tender-crisp.", "Add soy sauce and stir-fry for 1 more minute.", "Serve hot."],
                    StepImages = null,
                    ServingSize = 2,
                    PreparationTime = 20,
                    CreatedAt = DateTime.UtcNow
                },
                 // Recipe 3: Lentil Soup
                 new Recipes
                 {
                     Id = 3,
                     UserId = "56",
                     Header = "Hearty Lentil Soup",
                     BodyText = "A comforting and nutritious soup.",
                     BannerImage = null,
                     Steps = ["Sauté onions, carrots, and garlic in olive oil until softened.", "Add lentils, vegetable stock, and bay leaves.", "Bring to a boil, then reduce heat and simmer until lentils are tender.", "Season with salt and pepper.", "Remove bay leaves and serve hot."],
                     StepImages = null,
                     ServingSize = 4,
                     PreparationTime = 40,
                     CreatedAt = DateTime.UtcNow
                 },
                 // Recipe 4: Baked Salmon with Lemon
                 new Recipes
                 {
                     Id = 4,
                     UserId = "32",
                     Header = "Baked Salmon with Lemon",
                     BodyText = "Simple and delicious baked salmon.",
                     BannerImage = null,
                     Steps = ["Place salmon fillets on a baking sheet.", "Drizzle with olive oil and lemon juice.", "Season with salt, pepper, and dill.", "Bake until cooked through."],
                     StepImages = null,
                     ServingSize = 2,
                     PreparationTime = 15,
                     CreatedAt = DateTime.UtcNow
                 },
                // Recipe 5:  Caprese Salad
                new Recipes
                {
                    Id = 5,
                    UserId = "99",
                    Header = "Classic Caprese Salad",
                    BodyText = "A simple and refreshing salad.",
                    BannerImage = null,
                    Steps = ["Slice tomatoes and fresh mozzarella.", "Arrange tomato and mozzarella slices on a plate.", "Top with fresh basil leaves.", "Drizzle with balsamic glaze.", "Season with salt and pepper."],
                    StepImages = null,
                    ServingSize = 2,
                    PreparationTime = 10,
                    CreatedAt = DateTime.UtcNow
                },
                  // Recipe 6:  Oatmeal with Berries
                  new Recipes
                  {
                      Id = 6,
                      UserId = "95",
                      Header = "Oatmeal with Mixed Berries",
                      BodyText = "A healthy and warm breakfast option.",
                      BannerImage = null,
                      Steps = ["Cook oats with water or milk according to package directions.", "Top with mixed berries.", "Add a drizzle of honey or maple syrup if desired."],
                      StepImages = null,
                      ServingSize = 1,
                      PreparationTime = 10,
                      CreatedAt = DateTime.UtcNow
                  },
                 // Recipe 7:  Spicy Black Bean Burgers
                 new Recipes
                 {
                     Id = 7,
                     UserId = "100",
                     Header = "Spicy Black Bean Burgers",
                     BodyText = "Vegetarian burgers with a kick.",
                     BannerImage = null,
                     Steps = ["Mash black beans in a bowl.", "Add breadcrumbs, chopped onions, spices and mix thoroughly.", "Form mixture into patties.", "Cook the burgers in a pan with oil until each side is browned."],
                     StepImages = null,
                     ServingSize = 4,
                     PreparationTime = 25,
                     CreatedAt = DateTime.UtcNow
                 },
                 // Recipe 8:  Guacamole
                 new Recipes
                 {
                     Id = 8,
                     UserId = "98",
                     Header = "Fresh Guacamole",
                     BodyText = "Perfect appetizer for any occasion.",
                     BannerImage = null,
                     Steps = ["Mash avocados in a bowl.", "Add diced onions, cilantro, diced tomatoes, lime juice and mix thoroughly.", "Season with salt and pepper to taste.", "Serve with chips or vegetables."],
                     StepImages = null,
                     ServingSize = 4,
                     PreparationTime = 15,
                     CreatedAt = DateTime.UtcNow
                 },
                // Recipe 9:  Avocado Toast
                new Recipes
                {
                    Id = 9,
                    UserId = "98",
                    Header = "Avocado Toast with Eggs",
                    BodyText = "Simple and nutritious breakfast.",
                    BannerImage = null,
                    Steps = ["Toast the bread.", "Mash avocado and spread on toast.", "Cook eggs to your liking.", "Top toast with eggs and season with salt and pepper."],
                    StepImages = null,
                    ServingSize = 2,
                    PreparationTime = 10,
                    CreatedAt = DateTime.UtcNow
                },
                // Recipe 10:  Chicken Curry
                new Recipes
                {
                    Id = 10,
                    UserId = "95",
                    Header = "Chicken Curry",
                    BodyText = "A flavorful and aromatic curry.",
                    BannerImage = null,
                    Steps = ["Sauté onions, garlic and ginger in coconut oil until fragrant.", "Add chicken pieces and brown them.", "Add curry powder, cumin, turmeric and stir well.", "Pour in vegetable stock, coconut milk and simmer until chicken is cooked thoroughly."],
                    StepImages = null,
                    ServingSize = 4,
                    PreparationTime = 40,
                    CreatedAt = DateTime.UtcNow
                },
                    // Recipe 11:  Mushroom Risotto
                    new Recipes
                    {
                        Id = 11,
                        UserId = "96",
                        Header = "Mushroom Risotto",
                        BodyText = "Creamy and comforting risotto.",
                        BannerImage = null,
                        Steps = ["Sauté mushrooms and onions in olive oil.", "Add rice and toast for a minute.", "Add warm vegetable stock gradually, stirring continuously until absorbed.", "Stir in parmesan cheese and butter at the end."],
                        StepImages = null,
                        ServingSize = 4,
                        PreparationTime = 45,
                        CreatedAt = DateTime.UtcNow
                    },
                      // Recipe 12:  Shrimp Scampi
                      new Recipes
                      {
                          Id = 12,
                          UserId = "7",
                          Header = "Shrimp Scampi with Pasta",
                          BodyText = "Quick and flavorful shrimp pasta.",
                          BannerImage = null,
                          Steps = ["Cook pasta according to package directions.", "Sauté garlic in olive oil.", "Add shrimp and cook until pink.", "Toss with cooked pasta, lemon juice, parsley and serve."],
                          StepImages = null,
                          ServingSize = 2,
                          PreparationTime = 20,
                          CreatedAt = DateTime.UtcNow
                      },
                    // Recipe 13:  Beef Tacos
                    new Recipes
                    {
                        Id = 13,
                        UserId = "41",
                        Header = "Spicy Beef Tacos",
                        BodyText = "Flavorful beef tacos with your favorite toppings.",
                        BannerImage = null,
                        Steps = ["Cook beef with taco seasoning.", "Warm tortillas.", "Fill tortillas with beef, salsa and toppings of choice."],
                        StepImages = null,
                        ServingSize = 4,
                        PreparationTime = 25,
                        CreatedAt = DateTime.UtcNow
                    },
                   // Recipe 14:  Spinach and Feta Stuffed Chicken
                   new Recipes
                   {
                       Id = 14,
                       UserId = "96",
                       Header = "Spinach and Feta Stuffed Chicken Breast",
                       BodyText = "Delicious and healthy stuffed chicken.",
                       BannerImage = null,
                       Steps = ["Mix spinach, feta cheese, garlic.", "Cut a slit into each chicken breast.", "Stuff chicken breasts with spinach-feta mixture.", "Bake until chicken is cooked through."],
                       StepImages = null,
                       ServingSize = 2,
                       PreparationTime = 30,
                       CreatedAt = DateTime.UtcNow
                   },
                  // Recipe 15:  Pancakes
                  new Recipes
                  {
                      Id = 15,
                      UserId = "25",
                      Header = "Fluffy Pancakes",
                      BodyText = "Classic breakfast pancakes.",
                      BannerImage = null,
                      Steps = ["Whisk together dry ingredients.", "Combine wet ingredients.", "Gently mix wet and dry ingredients.", "Cook on a preheated griddle until golden brown."],
                      StepImages = null,
                      ServingSize = 4,
                      PreparationTime = 20,
                      CreatedAt = DateTime.UtcNow
                  },
                 // Recipe 16:  Eggplant Parmesan
                 new Recipes
                 {
                     Id = 16,
                     UserId = "76",
                     Header = "Eggplant Parmesan",
                     BodyText = "Italian classic eggplant dish.",
                     BannerImage = null,
                     Steps = ["Slice and salt the eggplant.", "Bread eggplant slices.", "Fry eggplant slices until golden.", "Layer eggplant, sauce and mozzarella in a baking dish and bake."],
                     StepImages = null,
                     ServingSize = 4,
                     PreparationTime = 60,
                     CreatedAt = DateTime.UtcNow
                 },
                // Recipe 17:  Caprese Pasta Salad
                new Recipes
                {
                    Id = 17,
                    UserId = "65",
                    Header = "Caprese Pasta Salad",
                    BodyText = "Refreshing pasta salad with caprese flavors.",
                    BannerImage = null,
                    Steps = ["Cook pasta according to package.", "Combine cooked pasta with diced tomatoes, mozzarella, basil leaves.", "Drizzle with olive oil, balsamic glaze and serve."],
                    StepImages = null,
                    ServingSize = 4,
                    PreparationTime = 15,
                    CreatedAt = DateTime.UtcNow
                },
                  // Recipe 18:  Tuna Salad Sandwich
                  new Recipes
                  {
                      Id = 18,
                      UserId = "55",
                      Header = "Classic Tuna Salad Sandwich",
                      BodyText = "Simple tuna salad sandwich.",
                      BannerImage = null,
                      Steps = ["Mix tuna with mayonnaise, diced celery and onions.", "Spread on bread slices.", "Add lettuce and other toppings to liking."],
                      StepImages = null,
                      ServingSize = 2,
                      PreparationTime = 10,
                      CreatedAt = DateTime.UtcNow
                  },
                  // Recipe 19:  Chicken and Veggie Skewers
                  new Recipes
                  {
                      Id = 19,
                      UserId = "32",
                      Header = "Chicken and Vegetable Skewers",
                      BodyText = "Easy to grill or bake skewers.",
                      BannerImage = null,
                      Steps = ["Cut chicken and vegetables into bite-sized pieces.", "Thread onto skewers.", "Grill or bake until chicken is cooked through."],
                      StepImages = null,
                      ServingSize = 4,
                      PreparationTime = 25,
                      CreatedAt = DateTime.UtcNow
                  },
                 // Recipe 20:  Sweet Potato Fries
                 new Recipes
                 {
                     Id = 20,
                     UserId = "1",
                     Header = "Baked Sweet Potato Fries",
                     BodyText = "Healthy baked sweet potato fries.",
                     BannerImage = null,
                     Steps = ["Cut sweet potatoes into strips.", "Toss with olive oil, salt, pepper, paprika.", "Bake until tender and lightly brown."],
                     StepImages = null,
                     ServingSize = 4,
                     PreparationTime = 40,
                     CreatedAt = DateTime.UtcNow
                 }
            );
        }
        public void PopulateRecipeIngredientsData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipes_Ingredients>().HasData(
                // Recipe 1 Ingredients
                new Recipes_Ingredients { Id = 1, IngredientId = 2, RecipeId = 1, Quantity = 2, Unit = "tablespoons" },
                new Recipes_Ingredients { Id = 2, IngredientId = 11, RecipeId = 1, Quantity = 1, Unit = "medium" },
                new Recipes_Ingredients { Id = 3, IngredientId = 83, RecipeId = 1, Quantity = 1, Unit = "cup" },


                // Recipe 2 Ingredients
                new Recipes_Ingredients { Id = 4, IngredientId = 35, RecipeId = 2, Quantity = 1, Unit = "pound" },
                new Recipes_Ingredients { Id = 5, IngredientId = 17, RecipeId = 2, Quantity = 1, Unit = "large" },
                new Recipes_Ingredients { Id = 6, IngredientId = 23, RecipeId = 2, Quantity = 1, Unit = "cup" },
                new Recipes_Ingredients { Id = 7, IngredientId = 62, RecipeId = 2, Quantity = 2, Unit = "cloves" },
                new Recipes_Ingredients { Id = 8, IngredientId = 122, RecipeId = 2, Quantity = 1, Unit = "tablespoon" },
                new Recipes_Ingredients { Id = 9, IngredientId = 126, RecipeId = 2, Quantity = 2, Unit = "tablespoons" },

               // Recipe 3 Ingredients
               new Recipes_Ingredients { Id = 10, IngredientId = 77, RecipeId = 3, Quantity = 1, Unit = "cup" },
               new Recipes_Ingredients { Id = 11, IngredientId = 93, RecipeId = 3, Quantity = 1, Unit = "medium" },
               new Recipes_Ingredients { Id = 12, IngredientId = 28, RecipeId = 3, Quantity = 2, Unit = "medium" },
               new Recipes_Ingredients { Id = 13, IngredientId = 62, RecipeId = 3, Quantity = 2, Unit = "cloves" },
                new Recipes_Ingredients { Id = 14, IngredientId = 92, RecipeId = 3, Quantity = 2, Unit = "tablespoons" },
               new Recipes_Ingredients { Id = 15, IngredientId = 142, RecipeId = 3, Quantity = 4, Unit = "cups" },
               new Recipes_Ingredients { Id = 16, IngredientId = 14, RecipeId = 3, Quantity = 2, Unit = "leaves" },


                 // Recipe 4 Ingredients
                 new Recipes_Ingredients { Id = 17, IngredientId = 119, RecipeId = 4, Quantity = 2, Unit = "fillets" },
                 new Recipes_Ingredients { Id = 18, IngredientId = 92, RecipeId = 4, Quantity = 1, Unit = "tablespoon" },
                new Recipes_Ingredients { Id = 19, IngredientId = 76, RecipeId = 4, Quantity = 1, Unit = "tablespoon" },
                new Recipes_Ingredients { Id = 20, IngredientId = 55, RecipeId = 4, Quantity = 1, Unit = "teaspoon" },
                 new Recipes_Ingredients { Id = 21, IngredientId = 20, RecipeId = 4, Quantity = 1, Unit = "pinch" },


                 // Recipe 5 Ingredients
                 new Recipes_Ingredients { Id = 22, IngredientId = 137, RecipeId = 5, Quantity = 2, Unit = "medium" },
                new Recipes_Ingredients { Id = 23, IngredientId = 32, RecipeId = 5, Quantity = 4, Unit = "ounces" },
                 new Recipes_Ingredients { Id = 24, IngredientId = 13, RecipeId = 5, Quantity = 4, Unit = "leaves" },
                new Recipes_Ingredients { Id = 25, IngredientId = 10, RecipeId = 5, Quantity = 1, Unit = "tablespoon" },
                 new Recipes_Ingredients { Id = 26, IngredientId = 20, RecipeId = 5, Quantity = 1, Unit = "pinch" },


                // Recipe 6 Ingredients
                new Recipes_Ingredients { Id = 27, IngredientId = 91, RecipeId = 6, Quantity = 0.5, Unit = "cup" },
                new Recipes_Ingredients { Id = 28, IngredientId = 129, RecipeId = 6, Quantity = 0.5, Unit = "cup" },
                new Recipes_Ingredients { Id = 29, IngredientId = 21, RecipeId = 6, Quantity = 0.5, Unit = "cup" },
                 new Recipes_Ingredients { Id = 30, IngredientId = 69, RecipeId = 6, Quantity = 1, Unit = "tablespoon" },

                // Recipe 7 Ingredients
                new Recipes_Ingredients { Id = 31, IngredientId = 19, RecipeId = 7, Quantity = 1, Unit = "can" },
                new Recipes_Ingredients { Id = 32, IngredientId = 22, RecipeId = 7, Quantity = 0.5, Unit = "cup" },
                new Recipes_Ingredients { Id = 33, IngredientId = 93, RecipeId = 7, Quantity = 0.25, Unit = "cup" },
                 new Recipes_Ingredients { Id = 34, IngredientId = 53, RecipeId = 7, Quantity = 1, Unit = "teaspoon" },
                  new Recipes_Ingredients { Id = 35, IngredientId = 31, RecipeId = 7, Quantity = 0.25, Unit = "teaspoon" },

               // Recipe 8 Ingredients
               new Recipes_Ingredients { Id = 36, IngredientId = 7, RecipeId = 8, Quantity = 2, Unit = "medium" },
               new Recipes_Ingredients { Id = 37, IngredientId = 93, RecipeId = 8, Quantity = 0.25, Unit = "cup" },
               new Recipes_Ingredients { Id = 38, IngredientId = 38, RecipeId = 8, Quantity = 0.25, Unit = "cup" },
               new Recipes_Ingredients { Id = 39, IngredientId = 137, RecipeId = 8, Quantity = 0.5, Unit = "medium" },
               new Recipes_Ingredients { Id = 40, IngredientId = 78, RecipeId = 8, Quantity = 1, Unit = "tablespoon" },
               new Recipes_Ingredients { Id = 41, IngredientId = 20, RecipeId = 8, Quantity = 1, Unit = "pinch" },


               // Recipe 9 Ingredients
               new Recipes_Ingredients { Id = 42, IngredientId = 22, RecipeId = 9, Quantity = 2, Unit = "slices" },
               new Recipes_Ingredients { Id = 43, IngredientId = 7, RecipeId = 9, Quantity = 1, Unit = "medium" },
               new Recipes_Ingredients { Id = 44, IngredientId = 58, RecipeId = 9, Quantity = 2, Unit = "large" },
               new Recipes_Ingredients { Id = 45, IngredientId = 20, RecipeId = 9, Quantity = 1, Unit = "pinch" },


              // Recipe 10 Ingredients
              new Recipes_Ingredients { Id = 46, IngredientId = 93, RecipeId = 10, Quantity = 1, Unit = "medium" },
              new Recipes_Ingredients { Id = 47, IngredientId = 62, RecipeId = 10, Quantity = 2, Unit = "cloves" },
              new Recipes_Ingredients { Id = 48, IngredientId = 64, RecipeId = 10, Quantity = 1, Unit = "tablespoon" },
              new Recipes_Ingredients { Id = 49, IngredientId = 42, RecipeId = 10, Quantity = 2, Unit = "tablespoons" },
              new Recipes_Ingredients { Id = 50, IngredientId = 35, RecipeId = 10, Quantity = 1, Unit = "pound" },
              new Recipes_Ingredients { Id = 51, IngredientId = 53, RecipeId = 10, Quantity = 1, Unit = "tablespoon" },
              new Recipes_Ingredients { Id = 52, IngredientId = 140, RecipeId = 10, Quantity = 0.5, Unit = "teaspoon" },
              new Recipes_Ingredients { Id = 53, IngredientId = 39, RecipeId = 10, Quantity = 0.5, Unit = "teaspoon" },
               new Recipes_Ingredients { Id = 54, IngredientId = 142, RecipeId = 10, Quantity = 2, Unit = "cups" },
               new Recipes_Ingredients { Id = 55, IngredientId = 83, RecipeId = 10, Quantity = 1, Unit = "cup" },

                 // Recipe 11 Ingredients
                 new Recipes_Ingredients { Id = 56, IngredientId = 88, RecipeId = 11, Quantity = 1, Unit = "cup" },
                new Recipes_Ingredients { Id = 57, IngredientId = 93, RecipeId = 11, Quantity = 0.5, Unit = "medium" },
                new Recipes_Ingredients { Id = 58, IngredientId = 92, RecipeId = 11, Quantity = 1, Unit = "tablespoon" },
                new Recipes_Ingredients { Id = 59, IngredientId = 115, RecipeId = 11, Quantity = 1.5, Unit = "cups" },
                 new Recipes_Ingredients { Id = 60, IngredientId = 142, RecipeId = 11, Quantity = 4, Unit = "cups" },
                 new Recipes_Ingredients { Id = 61, IngredientId = 32, RecipeId = 11, Quantity = 0.5, Unit = "cup" },
                  new Recipes_Ingredients { Id = 62, IngredientId = 24, RecipeId = 11, Quantity = 1, Unit = "tablespoon" },


                 // Recipe 12 Ingredients
                 new Recipes_Ingredients { Id = 63, IngredientId = 98, RecipeId = 12, Quantity = 4, Unit = "ounces" },
                 new Recipes_Ingredients { Id = 64, IngredientId = 62, RecipeId = 12, Quantity = 2, Unit = "cloves" },
                new Recipes_Ingredients { Id = 65, IngredientId = 92, RecipeId = 12, Quantity = 2, Unit = "tablespoons" },
                new Recipes_Ingredients { Id = 66, IngredientId = 124, RecipeId = 12, Quantity = 1, Unit = "pound" },
                new Recipes_Ingredients { Id = 67, IngredientId = 76, RecipeId = 12, Quantity = 1, Unit = "tablespoon" },
                 new Recipes_Ingredients { Id = 68, IngredientId = 97, RecipeId = 12, Quantity = 1, Unit = "tablespoon" },


                // Recipe 13 Ingredients
                new Recipes_Ingredients { Id = 69, IngredientId = 16, RecipeId = 13, Quantity = 1, Unit = "pound" },
                new Recipes_Ingredients { Id = 70, IngredientId = 96, RecipeId = 13, Quantity = 1, Unit = "tablespoon" },
                new Recipes_Ingredients { Id = 71, IngredientId = 138, RecipeId = 13, Quantity = 4, Unit = "pieces" },
                 new Recipes_Ingredients { Id = 72, IngredientId = 137, RecipeId = 13, Quantity = 0.5, Unit = "cup" },


                // Recipe 14 Ingredients
                new Recipes_Ingredients { Id = 73, IngredientId = 127, RecipeId = 14, Quantity = 1, Unit = "cup" },
                 new Recipes_Ingredients { Id = 74, IngredientId = 59, RecipeId = 14, Quantity = 0.5, Unit = "cup" },
                 new Recipes_Ingredients { Id = 75, IngredientId = 62, RecipeId = 14, Quantity = 1, Unit = "clove" },
                new Recipes_Ingredients { Id = 76, IngredientId = 35, RecipeId = 14, Quantity = 2, Unit = "breasts" },

               // Recipe 15 Ingredients
               new Recipes_Ingredients { Id = 77, IngredientId = 22, RecipeId = 15, Quantity = 2, Unit = "cups" },
                new Recipes_Ingredients { Id = 78, IngredientId = 8, RecipeId = 15, Quantity = 1, Unit = "teaspoon" },
                new Recipes_Ingredients { Id = 79, IngredientId = 130, RecipeId = 15, Quantity = 2, Unit = "tablespoons" },
                 new Recipes_Ingredients { Id = 80, IngredientId = 58, RecipeId = 15, Quantity = 2, Unit = "large" },
                 new Recipes_Ingredients { Id = 81, IngredientId = 83, RecipeId = 15, Quantity = 1, Unit = "cup" },

                // Recipe 16 Ingredients
                new Recipes_Ingredients { Id = 82, IngredientId = 57, RecipeId = 16, Quantity = 1, Unit = "large" },
                new Recipes_Ingredients { Id = 83, IngredientId = 58, RecipeId = 16, Quantity = 2, Unit = "large" },
                 new Recipes_Ingredients { Id = 84, IngredientId = 22, RecipeId = 16, Quantity = 1, Unit = "cup" },
                 new Recipes_Ingredients { Id = 85, IngredientId = 137, RecipeId = 16, Quantity = 2, Unit = "cups" },
                 new Recipes_Ingredients { Id = 86, IngredientId = 32, RecipeId = 16, Quantity = 4, Unit = "ounces" },


                // Recipe 17 Ingredients
                new Recipes_Ingredients { Id = 87, IngredientId = 98, RecipeId = 17, Quantity = 8, Unit = "ounces" },
                 new Recipes_Ingredients { Id = 88, IngredientId = 137, RecipeId = 17, Quantity = 1, Unit = "cup" },
                 new Recipes_Ingredients { Id = 89, IngredientId = 32, RecipeId = 17, Quantity = 1, Unit = "cup" },
                new Recipes_Ingredients { Id = 90, IngredientId = 13, RecipeId = 17, Quantity = 0.5, Unit = "cup" },
                new Recipes_Ingredients { Id = 91, IngredientId = 92, RecipeId = 17, Quantity = 2, Unit = "tablespoons" },
                new Recipes_Ingredients { Id = 92, IngredientId = 10, RecipeId = 17, Quantity = 1, Unit = "tablespoon" },



                 // Recipe 18 Ingredients
                 new Recipes_Ingredients { Id = 93, IngredientId = 139, RecipeId = 18, Quantity = 1, Unit = "can" },
                new Recipes_Ingredients { Id = 94, IngredientId = 82, RecipeId = 18, Quantity = 2, Unit = "tablespoons" },
                 new Recipes_Ingredients { Id = 95, IngredientId = 140, RecipeId = 18, Quantity = 0.25, Unit = "cup" },
                 new Recipes_Ingredients { Id = 96, IngredientId = 93, RecipeId = 18, Quantity = 0.25, Unit = "cup" },
                 new Recipes_Ingredients { Id = 97, IngredientId = 22, RecipeId = 18, Quantity = 2, Unit = "slices" },

                // Recipe 19 Ingredients
                new Recipes_Ingredients { Id = 98, IngredientId = 35, RecipeId = 19, Quantity = 1, Unit = "pound" },
                new Recipes_Ingredients { Id = 99, IngredientId = 17, RecipeId = 19, Quantity = 1, Unit = "large" },
                 new Recipes_Ingredients { Id = 100, IngredientId = 150, RecipeId = 19, Quantity = 1, Unit = "medium" },
                new Recipes_Ingredients { Id = 101, IngredientId = 93, RecipeId = 19, Quantity = 0.5, Unit = "medium" },

                 // Recipe 20 Ingredients
                 new Recipes_Ingredients { Id = 102, IngredientId = 132, RecipeId = 20, Quantity = 2, Unit = "medium" },
                new Recipes_Ingredients { Id = 103, IngredientId = 92, RecipeId = 20, Quantity = 2, Unit = "tablespoons" },
                new Recipes_Ingredients { Id = 104, IngredientId = 96, RecipeId = 20, Quantity = 1, Unit = "teaspoon" },
               new Recipes_Ingredients { Id = 105, IngredientId = 20, RecipeId = 20, Quantity = 1, Unit = "pinch" }

           );
        }
    }
}
