using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<PaginatedResponseDTO<IngredientIdDTO>> GetIngredientsPaginatedAsync(int pageNumber, int pageSize)
        {
            var (ingredients, totalCount) = await unitOfWork.Ingredients.GetPaginatedAsync(
                pageNumber: pageNumber,
                pageSize: pageSize,
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
            }).ToList();

            return new PaginatedResponseDTO<IngredientIdDTO>
            {
                Items = ingredientDTOs,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<IngredientIdDTO> GetIngredientByIdAsync(int ingredientId)
        {
            // Fetch the ingredient with related entities
            var ingredient = (await unitOfWork.Ingredients.GetAllAsync(
                includeProperties: "Ingredients_Preferences.Preference,Type"))
                .FirstOrDefault(i => i.Id == ingredientId);

            if (ingredient == null)
            {
                return null; // Ingredient not found
            }

            // Map to DTO
            var ingredientDTO = new IngredientIdDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name,

                // Map IngredientType information
                Type = new IngredientTypeIdDTO
                {
                    Id = ingredient.Type.Id,
                    Name = ingredient.Type.Name,
                    Description = ingredient.Type.Description
                },

                // Map allergens
                Allergens = ingredient.Ingredients_Preferences.Select(ip => new AllergenIdDTO
                {
                    Id = ip.Preference.Id,
                    Name = ip.Preference.Name,
                    Description = ip.Preference.Description
                }).ToList()
            };

            return ingredientDTO;
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

        public async Task<PaginatedResponseDTO<IngredientIdDTO>> SearchIngredients(IngredientSearchParams searchParams, int pageNumber, int pageSize)
        {
            var query = unitOfWork.Ingredients.GetQueryable()
                .Include(i => i.Type)
                .Include(i => i.Ingredients_Preferences)
                .ThenInclude(ip => ip.Preference)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchParams.NameContains))
                query = query.Where(i => i.Name.Contains(searchParams.NameContains));

            if (searchParams.IngredientTypeIds.Any())
            {
                query = query.Where(i => searchParams.IngredientTypeIds.Contains(i.TypeId));
            }

            if (searchParams.AllergenIds.Any())
            {
                query = query.Where(i => !i.Ingredients_Preferences.Any(ip => searchParams.AllergenIds.Contains(ip.PreferenceId)) || !i.Ingredients_Preferences.Any());
            }

            bool ascending = (searchParams.SortOrder?.ToLower() != "desc");
            query = searchParams.SortBy?.ToLower() switch
            {
                "type" => ascending ? query.OrderBy(i => i.Type.Name) : query.OrderByDescending(i => i.Type.Name),
                _ => ascending ? query.OrderBy(i => i.Name) : query.OrderByDescending(i => i.Name),
            };

            var totalCount = await query.CountAsync();
            var ingredients = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var ingredientDTOs = ingredients.Select(i => new IngredientIdDTO
            {
                Id = i.Id,
                Name = i.Name,
                Type = new IngredientTypeIdDTO
                {
                    Id = i.Type.Id,
                    Name = i.Type.Name,
                    Description = i.Type.Description
                },
                Allergens = i.Ingredients_Preferences.Select(ip => new AllergenIdDTO
                {
                    Id = ip.Preference.Id,
                    Name = ip.Preference.Name,
                    Description = ip.Preference.Description
                }).ToList()
            }).ToList();

            return new PaginatedResponseDTO<IngredientIdDTO>
            {
                Items = ingredientDTOs,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

    }
}
