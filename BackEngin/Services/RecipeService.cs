using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            // finds user names of the recipe users
            var userIds = recipes.Select(b => b.UserId).Distinct().ToList();
            var users = await _unitOfWork.Users.FindAsync(u => userIds.Contains(u.Id));
            var userDictionary = users.ToDictionary(u => u.Id, u => u.UserName);

            var recipeDtos = recipes.Select(r => new RecipeDTO
            {
                Id = r.Id,
                Header = r.Header,
                BodyText = r.BodyText,
                UserId = r.UserId,
                UserName = userDictionary.ContainsKey(r.UserId) ? userDictionary[r.UserId] : "Unknown",
                //Images = r.Images,
                CreatedAt = r.CreatedAt,
                PreparationTime = r.PreparationTime,
                ServingSize = r.ServingSize,
                Steps = r.Steps
            }).ToList();

            return new PaginatedResponseDTO<RecipeDTO>
            {
                Items = recipeDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<RecipeDetailsDTO> CreateRecipe(string userId, CreateRecipeDTO createRecipeDTO)
        {
            // Check if ingredients are provided and valid
            await IngredientCheck(createRecipeDTO.Ingredients);

            var newRecipe = new Recipes
            {
                Header = createRecipeDTO.Header,
                BodyText = createRecipeDTO.BodyText,
                UserId = userId,
                BannerImage = createRecipeDTO.BannerImage,
                StepImages = createRecipeDTO.StepImages,
                Steps = createRecipeDTO.Steps,
                CreatedAt = DateTime.UtcNow,
                ServingSize = createRecipeDTO.ServingSize,
                PreparationTime = createRecipeDTO.PreparationTime,
            };

            await _unitOfWork.Recipes.AddAsync(newRecipe);
            await _unitOfWork.CompleteAsync();

            // Add the recipe ingredients
            var ingredientDTOs = new List<RecipeIngredientRequestDTO>();
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
                ingredientDTOs.Add(new RecipeIngredientRequestDTO
                {
                    IngredientId = ingredientDto.IngredientId,
                    Quantity = ingredientDto.Quantity,
                    Unit = ingredientDto.Unit
                });
            }

            // Fetch ingredient details (including names) for the response
            var ingredientIds = createRecipeDTO.Ingredients.Select(i => i.IngredientId).ToList();
            var ingredients = (await _unitOfWork.Ingredients
                .FindAsync(i => ingredientIds.Contains(i.Id))).ToList();

            // Map ingredients to details DTO
            var ingredientDetailsDTOs = createRecipeDTO.Ingredients.Select(inputIngredient =>
            {
                var ingredient = ingredients.FirstOrDefault(i => i.Id == inputIngredient.IngredientId);
                return new RecipeIngredientDetailsDTO
                {
                    IngredientId = inputIngredient.IngredientId,
                    IngredientName = ingredient?.Name, // Get the ingredient name
                    Quantity = inputIngredient.Quantity,
                    Unit = inputIngredient.Unit
                };
            }).ToList();

            var user = await _unitOfWork.Users.FindAsync(u => u.Id == userId);

            // Return the created recipe with ingredients
            return new RecipeDetailsDTO
            {
                Id = newRecipe.Id,
                Header = newRecipe.Header,
                BodyText = newRecipe.BodyText,
                UserId = userId,
                UserName = user.FirstOrDefault()?.UserName ?? "Unknown",
                Ingredients = ingredientDetailsDTOs,
                CreatedAt = newRecipe.CreatedAt,
                ServingSize = newRecipe.ServingSize,
                PreparationTime = newRecipe.PreparationTime,
                Steps = newRecipe.Steps
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

            var user = await _unitOfWork.Users.FindAsync(u => u.Id == recipe.UserId);

            return new RecipeDetailsDTO
            {
                Id = recipe.Id,
                Header = recipe.Header,
                BodyText = recipe.BodyText,
                UserId = recipe.UserId,
                UserName = user.FirstOrDefault()?.UserName ?? "Unknown",
                Ingredients = ingredientDTOs,
                CreatedAt = recipe.CreatedAt,
                ServingSize = recipe.ServingSize,
                PreparationTime = recipe.PreparationTime,
                Steps = recipe.Steps
            };
        }

        public async Task<RecipeDetailsDTO> UpdateRecipe(int recipeId, RecipeRequestDTO updateRecipeDTO)
        {
            // Fetch the existing recipe
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId);
            if (recipe == null) return null;

            // Check if ingredients are provided and valid
            await IngredientCheck(updateRecipeDTO.Ingredients);

            // Update the recipe details
            recipe.Header = updateRecipeDTO.Header;
            recipe.BodyText = updateRecipeDTO.BodyText;
            recipe.BannerImage = updateRecipeDTO.BannerImage;
            recipe.StepImages = updateRecipeDTO.StepImages;
            recipe.Steps = updateRecipeDTO.Steps;
            recipe.ServingSize = updateRecipeDTO.ServingSize;
            recipe.PreparationTime = updateRecipeDTO.PreparationTime;

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

            var user = await _unitOfWork.Users.FindAsync(u => u.Id == recipe.UserId);

            // Return updated recipe details without images.
            return new RecipeDetailsDTO
            {
                Id = recipe.Id,
                Header = recipe.Header,
                BodyText = recipe.BodyText,
                UserId = recipe.UserId,
                UserName = user.FirstOrDefault()?.UserName ?? "Unknown",
                Ingredients = ingredientDTOs,
                CreatedAt = recipe.CreatedAt,
                ServingSize = recipe.ServingSize,
                PreparationTime = recipe.PreparationTime,
                Steps = recipe.Steps,
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

        public async Task<byte[]?> GetRecipeBannerImage(int recipeId)
        {
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId);
            if (recipe == null) return null;

            return recipe.BannerImage;
        }

        public async Task<byte[]?> GetRecipeStepImage(int recipeId, int stepIndex)
        {
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId);
            if (recipe == null || recipe.StepImages == null) return null;

            // Check if stepIndex is within bounds
            if (stepIndex < 0 || stepIndex >= recipe.StepImages.Length) return null;

            return recipe.StepImages[stepIndex];
        }

        private async Task<bool> IngredientCheck(List<RecipeIngredientRequestDTO> Ingredients)
        {
            // Check if ingredients are provided
            if (Ingredients == null || !Ingredients.Any())
            {
                throw new ArgumentException("A recipe must have at least one ingredient.");
            }

            // Validate each ingredient ID
            var ingredientIds = Ingredients.Select(i => i.IngredientId).ToList();
            var existingIngredients = (await _unitOfWork.Ingredients.FindAsync(i => ingredientIds.Contains(i.Id))).ToList();

            if (existingIngredients.Count != ingredientIds.Count)
            {
                throw new ArgumentException("One or more ingredient IDs are invalid.");
            }
            return true; // Validation passed successfully
        }

        public async Task<PaginatedResponseDTO<RecipeDTO>> SearchRecipes(RecipeSearchParams searchParams, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Recipes.GetQueryable();

            // Filters with case-insensitive comparison
            if (!string.IsNullOrEmpty(searchParams.HeaderContains))
                query = query.Where(r => r.Header.ToLowerInvariant().Contains(searchParams.HeaderContains.ToLowerInvariant()));

            if (!string.IsNullOrEmpty(searchParams.BodyContains))
                query = query.Where(r => r.BodyText.ToLowerInvariant().Contains(searchParams.BodyContains.ToLowerInvariant()));

            if (!string.IsNullOrEmpty(searchParams.UserName))
                query = query.Where(r => r.User.UserName.ToLowerInvariant().Contains(searchParams.UserName.ToLowerInvariant()));

            if (searchParams.IngredientIds.Any() || searchParams.AllergenIds.Any())
            {
                query = query.Include(r => r.Recipes_Ingredients)
                             .ThenInclude(ri => ri.Ingredient)
                             .ThenInclude(i => i.Ingredients_Preferences)
                             .ThenInclude(ip => ip.Preference);

                if (searchParams.IngredientIds.Any())
                {
                    query = query.Where(r => r.Recipes_Ingredients.Any(ri => searchParams.IngredientIds.Contains(ri.IngredientId)));
                }

                if (searchParams.AllergenIds.Any())
                {
                    query = query.Where(r => r.Recipes_Ingredients.Any(ri =>
                        !ri.Ingredient.Ingredients_Preferences.Any(ip => searchParams.AllergenIds.Contains(ip.PreferenceId)) ||
                        !ri.Ingredient.Ingredients_Preferences.Any()
                    ));
                }

            }

            // Sorting
            bool ascending = (searchParams.SortOrder?.ToLowerInvariant() != "desc");
            query = searchParams.SortBy?.ToLowerInvariant() switch
            {
                "bodytext" => ascending ? query.OrderBy(r => r.BodyText) : query.OrderByDescending(r => r.BodyText),
                "userid" => ascending ? query.OrderBy(r => r.UserId) : query.OrderByDescending(r => r.UserId),
                _ => ascending ? query.OrderBy(r => r.Header) : query.OrderByDescending(r => r.Header),
            };

            var totalCount = await query.CountAsync();
            var recipes = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

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

    }
}
