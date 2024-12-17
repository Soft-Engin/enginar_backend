using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace BackEngin.Tests.Utils
{
    internal static class TestUtilities
    {
        public static List<Preferences> CreateAllergens(int count)
        {
            var allergens = new List<Preferences>();
            for (int i = 1; i <= count; i++)
            {
                allergens.Add(new Preferences
                {
                    Id = i,
                    Name = $"Allergen{i}",
                    Description = $"Allergen{i} desc"
                });
            }
            return allergens;
        }

        public static List<IngredientTypes> CreateIngredientTypes(int count)
        {
            var types = new List<IngredientTypes>();
            for (int i = 1; i <= count; i++)
            {
                types.Add(new IngredientTypes
                {
                    Id = i,
                    Name = $"Type{i}",
                    Description = $"Type{i} desc"
                });
            }
            return types;
        }

        public static List<Ingredients> CreateIngredients(
            int count,
            List<IngredientTypes> types,
            bool addAllergens = false,
            List<Preferences> allAllergens = null)
        {
            var ingredients = new List<Ingredients>();
            int typeCount = types.Count;
            int allergenCount = allAllergens?.Count ?? 0;

            for (int i = 1; i <= count; i++)
            {
                // Cycle through types deterministically
                var chosenType = types[(i - 1) % typeCount];

                var ingredient = new Ingredients
                {
                    Id = i,
                    Name = $"Ingredient{i}",
                    TypeId = chosenType.Id,
                    Type = chosenType,
                    Ingredients_Preferences = new List<Ingredients_Preferences>()
                };

                if (addAllergens && allergenCount > 0)
                {
                    // For a given ingredient i, let's assign a subset of allergens in a predictable manner.
                    // Example: Assign allergen with Id = (i % allergenCount) + 1 to this ingredient.
                    // Or assign the first allergen for odd ingredients, the second for even, etc.
                    // Let's just assign one allergen per ingredient for simplicity:
                    int allergenIndex = (i - 1) % allergenCount;
                    var allergen = allAllergens[allergenIndex];
                    ingredient.Ingredients_Preferences.Add(new Ingredients_Preferences
                    {
                        IngredientId = ingredient.Id,
                        PreferenceId = allergen.Id,
                        Preference = allergen
                    });
                }

                ingredients.Add(ingredient);
            }
            return ingredients;
        }

        public static List<Users> CreateUsers(int count)
        {
            var users = new List<Users>();
            for (int i = 1; i <= count; i++)
            {
                users.Add(new Users
                {
                    Id = i.ToString(),
                    FirstName = $"User{i}",
                    LastName = $"Last{i}",
                    UserName = $"user{i}",
                    Email = $"user{i}@example.com"
                });
            }
            return users;
        }

        public static List<Recipes> CreateRecipes(int count, List<Users> users, List<Ingredients> ingredients)
        {
            var recipes = new List<Recipes>();
            int userCount = users.Count;
            int ingredientCount = ingredients.Count;

            for (int i = 1; i <= count; i++)
            {
                // Assign user based on (i-1) % userCount
                var user = users[(i - 1) % userCount];

                var recipe = new Recipes
                {
                    Id = i,
                    Header = $"Recipe{i}",
                    BodyText = $"Recipe{i} body",
                    UserId = user.Id,
                    User = user,
                    Recipes_Ingredients = new List<Recipes_Ingredients>()
                };

                // Deterministically pick some ingredients.
                // For example, for recipe i, include ingredients with Id = 1, 2, ..., up to i (or limited by ingredientCount)
                int ingToAdd = Math.Min(i, ingredientCount);
                for (int ingId = 1; ingId <= ingToAdd; ingId++)
                {
                    var ing = ingredients[ingId - 1]; // since ingId starts from 1
                    recipe.Recipes_Ingredients.Add(new Recipes_Ingredients
                    {
                        RecipeId = recipe.Id,
                        IngredientId = ing.Id,
                        Ingredient = ing,
                        Quantity = ingId, // quantity = ingId for determinism
                        Unit = "units"
                    });
                }

                recipes.Add(recipe);
            }
            return recipes;
        }

        public static List<Blogs> CreateBlogs(int count, List<Users> users, List<Recipes> recipes)
        {
            var blogs = new List<Blogs>();
            int userCount = users.Count;
            int recipeCount = recipes.Count;

            for (int i = 1; i <= count; i++)
            {
                // Assign user based on (i-1) % userCount
                var user = users[(i - 1) % userCount];

                // For recipes, let's assign a recipe to every second blog if recipes are available.
                Recipes selectedRecipe = null;
                if (recipeCount > 0 && i % 2 == 0) // every even blog gets a recipe
                {
                    selectedRecipe = recipes[(i - 1) % recipeCount];
                }

                blogs.Add(new Blogs
                {
                    Id = i,
                    Header = $"Blog{i}",
                    BodyText = $"Body{i}",
                    UserId = user.Id,
                    User = user,
                    RecipeId = selectedRecipe?.Id,
                    Recipe = selectedRecipe
                });
            }
            return blogs;
        }
    }
}
