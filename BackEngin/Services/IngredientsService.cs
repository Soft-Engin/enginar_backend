using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Models;
using Models.DTO;

namespace BackEngin.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly IUnitOfWork unitOfWork;

        public IngredientsService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<IngredientIdDTO>> GetAllIngredientsAsync()
        {
            var ingredients = await unitOfWork.Ingredients.GetAllAsync(
                includeProperties: "Ingredients_Preferences.Preference,Type");

            var ingredientDTOs = ingredients.Select(i => new IngredientIdDTO
            {
                Id = i.Id,
                Name = i.Name,

                // Map IngredientType information
                Type = new IngredientTypeIdDTO
                {
                    Id = i.Type.Id,
                    Name = i.Type.Name,
                    Description = i.Type.Description
                },

                // Map allergens
                Allergens = i.Ingredients_Preferences.Select(ip => new AllergenIdDTO
                {
                    Id = ip.Preference.Id,
                    Name = ip.Preference.Name,
                    Description = ip.Preference.Description
                }).ToList()
            });

            return ingredientDTOs;
        }

        public async Task<int?> CreateIngredientAsync(IngredientDTO model)
        {
            try
            {
                // Validate AllergenIds
                if (model.AllergenIds != null && model.AllergenIds.Any())
                {
                    var existingAllergenIds = (await unitOfWork.Preferences.GetAllAsync())
                        .Select(a => a.Id)
                        .ToHashSet();

                    var invalidAllergenIds = model.AllergenIds.Where(id => !existingAllergenIds.Contains(id)).ToList();

                    if (invalidAllergenIds.Any())
                    {
                        throw new Exception($"Invalid AllergenIds: {string.Join(", ", invalidAllergenIds)}");
                    }
                }

                // Create the ingredient
                var ingredient = new Ingredients
                {
                    Name = model.Name,
                    TypeId = model.TypeId
                };

                await unitOfWork.Ingredients.AddAsync(ingredient);
                await unitOfWork.CompleteAsync();

                // Associate allergens with the ingredient
                if (model.AllergenIds != null && model.AllergenIds.Any())
                {
                    foreach (var allergenId in model.AllergenIds)
                    {
                        var ingredientPreference = new Ingredients_Preferences
                        {
                            IngredientId = ingredient.Id,
                            PreferenceId = allergenId
                        };
                        await unitOfWork.Ingredients_Preferences.AddAsync(ingredientPreference);
                    }

                    await unitOfWork.CompleteAsync();
                }

                return ingredient.Id;
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }


        public async Task<bool?> UpdateIngredientAsync(int ingredientId, IngredientDTO model)
        {
            try
            {
                // Validate the existence of the ingredient
                var ingredient = await unitOfWork.Ingredients.GetByIdAsync(ingredientId);

                if (ingredient == null)
                {
                    return null; // Ingredient not found
                }

                // Validate AllergenIds
                if (model.AllergenIds != null && model.AllergenIds.Any())
                {
                    var existingAllergenIds = (await unitOfWork.Preferences.GetAllAsync())
                        .Select(a => a.Id)
                        .ToHashSet();

                    var invalidAllergenIds = model.AllergenIds.Where(id => !existingAllergenIds.Contains(id)).ToList();

                    if (invalidAllergenIds.Any())
                    {
                        throw new Exception($"Invalid AllergenIds: {string.Join(", ", invalidAllergenIds)}");
                    }
                }

                // Update the ingredient's properties
                ingredient.Name = model.Name;
                ingredient.TypeId = model.TypeId;

                unitOfWork.Ingredients.Update(ingredient);
                await unitOfWork.CompleteAsync();

                // Update allergens (remove existing and add new)
                var existingAllergens = await unitOfWork.Ingredients_Preferences
                    .FindAsync(ip => ip.IngredientId == ingredientId);

                unitOfWork.Ingredients_Preferences.DeleteRange(existingAllergens);

                if (model.AllergenIds != null && model.AllergenIds.Any())
                {
                    foreach (var allergenId in model.AllergenIds)
                    {
                        var ingredientPreference = new Ingredients_Preferences
                        {
                            IngredientId = ingredient.Id,
                            PreferenceId = allergenId
                        };
                        await unitOfWork.Ingredients_Preferences.AddAsync(ingredientPreference);
                    }
                }

                await unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
        public async Task<bool?> DeleteIngredientAsync(int ingredientId)
        {
            try
            {
                // Validate the existence of the ingredient
                var ingredient = await unitOfWork.Ingredients.GetByIdAsync(ingredientId);

                if (ingredient == null)
                {
                    return null; // Ingredient not found
                }

                // Remove associated allergens
                var ingredientPreferences = await unitOfWork.Ingredients_Preferences
                    .FindAsync(ip => ip.IngredientId == ingredientId);

                if (ingredientPreferences != null && ingredientPreferences.Any())
                {
                    unitOfWork.Ingredients_Preferences.DeleteRange(ingredientPreferences);
                }

                // Delete the ingredient
                unitOfWork.Ingredients.Delete(ingredient);
                await unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

    }
}
