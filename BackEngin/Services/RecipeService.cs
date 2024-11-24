using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace BackEngin.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecipeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponseDTO<RecipeDTO>> GetRecipes(int pageNumber, int pageSize)
        {
            var (recipes, totalCount) = await _unitOfWork.Recipes.GetPaginatedAsync(
                filter: null, // No specific filter for now
                pageNumber: pageNumber,
                pageSize: pageSize
            );

            var recipeDtos = recipes.Select(r => new RecipeDTO
            {
                Id = r.Id,
                Header = r.Header,
                BodyText = r.BodyText
            }).ToList();

            return new PaginatedResponseDTO<RecipeDTO>
            {
                Items = recipeDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<RecipeDetailsDTO> CreateRecipe(CreateRecipeDTO createRecipeDTO)
        {
            var newRecipe = new Recipes
            {
                Header = createRecipeDTO.Header,
                BodyText = createRecipeDTO.BodyText,
                UserId = createRecipeDTO.UserId
            };

            await _unitOfWork.Recipes.AddAsync(newRecipe);
            await _unitOfWork.CompleteAsync();

            // Add the recipe ingredients
            var ingredientDTOs = new List<RecipeIngredientDetailsDTO>();
            if (createRecipeDTO.Ingredients != null && createRecipeDTO.Ingredients.Any())
            {
                foreach (var ingredientDto in createRecipeDTO.Ingredients)
                {
                    var recipeIngredient = new Recipes_Ingredients
                    {
                        RecipeId = newRecipe.Id,
                        IngredientId = ingredientDto.IngredientId,
                        Quantity = ingredientDto.Quantity,
                        Unit = ingredientDto.Unit
                    };

                    await _unitOfWork.Recipes_Ingredients.AddAsync(recipeIngredient);

                    // Add to DTO for returning details
                    ingredientDTOs.Add(new RecipeIngredientDetailsDTO
                    {
                        IngredientId = ingredientDto.IngredientId,
                        Quantity = ingredientDto.Quantity,
                        Unit = ingredientDto.Unit
                    });
                }

                await _unitOfWork.CompleteAsync();
            }

            // Return the created recipe with ingredients
            return new RecipeDetailsDTO
            {
                Id = newRecipe.Id,
                Header = newRecipe.Header,
                BodyText = newRecipe.BodyText,
                Ingredients = ingredientDTOs
            };
        }

        public async Task<RecipeDetailsDTO> GetRecipeDetails(int recipeId)
        {
            // Fetch the recipe with its ingredients
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId);
            if (recipe == null) return null;

            // Fetch associated recipe ingredients
            var recipeIngredients = (await _unitOfWork.Recipes_Ingredients
                .FindAsync(ri => ri.RecipeId == recipeId)).ToList();

            // Fetch ingredient names for the associated ingredient IDs
            var ingredientIds = recipeIngredients.Select(ri => ri.IngredientId).ToList();
            var ingredients = await _unitOfWork.Ingredients
                .FindAsync(i => ingredientIds.Contains(i.Id));

            // Map the recipe ingredients with their names
            var ingredientDTOs = recipeIngredients.Select(ri =>
            {
                var ingredient = ingredients.FirstOrDefault(i => i.Id == ri.IngredientId);
                return new RecipeIngredientDetailsDTO
                {
                    IngredientId = ri.IngredientId,
                    IngredientName = ingredient?.Name,
                    Quantity = ri.Quantity,
                    Unit = ri.Unit
                };
            }).ToList();

            return new RecipeDetailsDTO
            {
                Id = recipe.Id,
                Header = recipe.Header,
                BodyText = recipe.BodyText,
                Ingredients = ingredientDTOs
            };
        }

        public async Task<RecipeDetailsDTO> UpdateRecipe(int recipeId, RecipeRequestDTO updateRecipeDTO)
        {
            // Fetch the existing recipe
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId);
            if (recipe == null) return null;

            // Update the recipe details
            recipe.Header = updateRecipeDTO.Header;
            recipe.BodyText = updateRecipeDTO.BodyText;

            // Fetch existing ingredients for the recipe
            var existingIngredients = (await _unitOfWork.Recipes_Ingredients
                .FindAsync(ri => ri.RecipeId == recipeId)).ToList();

            // Handle updated ingredients
            if (updateRecipeDTO.Ingredients != null)
            {
                // Remove ingredients that are no longer in the updated list
                var ingredientIdsToRemove = existingIngredients
                    .Where(ri => !updateRecipeDTO.Ingredients.Any(ui => ui.IngredientId == ri.IngredientId))
                    .ToList();

                foreach (var ingredientToRemove in ingredientIdsToRemove)
                {
                    _unitOfWork.Recipes_Ingredients.Delete(ingredientToRemove);
                }

                // Add or update ingredients from the updated list
                foreach (var updatedIngredient in updateRecipeDTO.Ingredients)
                {
                    var existingIngredient = existingIngredients
                        .FirstOrDefault(ri => ri.IngredientId == updatedIngredient.IngredientId);

                    if (existingIngredient != null)
                    {
                        // Update the existing ingredient
                        existingIngredient.Quantity = updatedIngredient.Quantity;
                        existingIngredient.Unit = updatedIngredient.Unit;

                        _unitOfWork.Recipes_Ingredients.Update(existingIngredient);
                    }
                    else
                    {
                        // Add new ingredient
                        var newIngredient = new Recipes_Ingredients
                        {
                            RecipeId = recipe.Id,
                            IngredientId = updatedIngredient.IngredientId,
                            Quantity = updatedIngredient.Quantity,
                            Unit = updatedIngredient.Unit
                        };

                        await _unitOfWork.Recipes_Ingredients.AddAsync(newIngredient);
                    }
                }
            }

            // Save all changes
            _unitOfWork.Recipes.Update(recipe);
            await _unitOfWork.CompleteAsync();

            // Return updated recipe details
            return new RecipeDetailsDTO
            {
                Id = recipe.Id,
                Header = recipe.Header,
                BodyText = recipe.BodyText,
                Ingredients = updateRecipeDTO.Ingredients
            };
        }



        public async Task<bool> DeleteRecipe(int recipeId)
        {
            // Fetch the recipe
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId);
            if (recipe == null) return false;

            // Fetch associated recipe ingredients
            var recipeIngredients = (await _unitOfWork.Recipes_Ingredients
                .FindAsync(ri => ri.RecipeId == recipeId)).ToList();

            // Delete the associated recipe ingredients
            foreach (var recipeIngredient in recipeIngredients)
            {
                _unitOfWork.Recipes_Ingredients.Delete(recipeIngredient);
            }

            // Delete the recipe itself
            _unitOfWork.Recipes.Delete(recipe);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<string?> GetOwner(int recipeId)
        {
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId);
            if (recipe == null)
                return null;
            return recipe.UserId;
        }
    }
}
