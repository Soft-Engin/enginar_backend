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
            var ingredients = await unitOfWork.Ingredients.GetAllAsync();

            var ingredientDTOs = ingredients.Select(i => new IngredientIdDTO
            {
                Id = i.Id,
                Name = i.Name,
                TypeId = i.TypeId
            });

            return ingredientDTOs;
        }

        public async Task<int?> CreateIngredientAsync(IngredientDTO model)
        {
            try
            {
                var ingredient = new Ingredients
                {
                    Name = model.Name,
                    TypeId = model.TypeId
                };

                await unitOfWork.Ingredients.AddAsync(ingredient);
                await unitOfWork.CompleteAsync();

                return ingredient.Id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool?> UpdateIngredientAsync(int ingredientId, IngredientDTO model)
        {
            try
            {
                var ingredient = await unitOfWork.Ingredients.GetByIdAsync(ingredientId);

                if (ingredient == null)
                {
                    return null;
                }

                ingredient.Name = model.Name;
                ingredient.TypeId = model.TypeId;

                unitOfWork.Ingredients.Update(ingredient);
                await unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool?> DeleteIngredientAsync(int ingredientId)
        {
            try
            {
                var ingredient = await unitOfWork.Ingredients.GetByIdAsync(ingredientId);

                if (ingredient == null)
                {
                    return null;
                }

                unitOfWork.Ingredients.Delete(ingredient);
                await unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
