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
    public class IngredientTypesService : IIngredientTypesService
    {
        private readonly IUnitOfWork unitOfWork;

        public IngredientTypesService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IngredientTypeIdDTO> GetIngredientTypeByIdAsync(int ingredientTypeId)
        {
            var ingredientType = await unitOfWork.IngredientTypes.GetByIdAsync(ingredientTypeId);

            if (ingredientType == null)
            {
                return null; // IngredientType not found
            }

            var ingredientTypeDTO = new IngredientTypeIdDTO
            {
                Id = ingredientType.Id,
                Name = ingredientType.Name,
                Description = ingredientType.Description
            };

            return ingredientTypeDTO;
        }

        public async Task<PaginatedResponseDTO<IngredientTypeIdDTO>> GetIngredientTypesPaginatedAsync(int pageNumber, int pageSize)
        {
            var (ingredientTypes, totalCount) = await unitOfWork.IngredientTypes.GetPaginatedAsync(
                pageNumber: pageNumber,
                pageSize: pageSize);

            var ingredientTypeDTOs = ingredientTypes.Select(it => new IngredientTypeIdDTO
            {
                Id = it.Id,
                Name = it.Name,
                Description = it.Description
            }).ToList();

            return new PaginatedResponseDTO<IngredientTypeIdDTO>
            {
                Items = ingredientTypeDTOs,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<int?> CreateIngredientTypeAsync(IngredientTypeDTO model)
        {
            try
            {
                var ingredientType = new IngredientTypes
                {
                    Name = model.Name,
                    Description = model.Description
                };

                await unitOfWork.IngredientTypes.AddAsync(ingredientType);
                await unitOfWork.CompleteAsync();

                return ingredientType.Id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool?> UpdateIngredientTypeAsync(int ingredientTypeId, IngredientTypeDTO model)
        {
            try
            {
                var ingredientType = await unitOfWork.IngredientTypes.GetByIdAsync(ingredientTypeId);

                if (ingredientType == null)
                {
                    return null;
                }

                ingredientType.Name = model.Name;
                ingredientType.Description = model.Description;

                unitOfWork.IngredientTypes.Update(ingredientType);
                await unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool?> DeleteIngredientTypeAsync(int ingredientTypeId)
        {
            try
            {
                var ingredientType = await unitOfWork.IngredientTypes.GetByIdAsync(ingredientTypeId);

                if (ingredientType == null)
                {
                    return null;
                }

                unitOfWork.IngredientTypes.Delete(ingredientType);
                await unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PaginatedResponseDTO<IngredientTypeIdDTO>> SearchIngredientTypes(IngredientTypeSearchParams searchParams, int pageNumber, int pageSize)
        {
            var query = unitOfWork.IngredientTypes.GetQueryable();

            if (!string.IsNullOrEmpty(searchParams.NameContains))
                query = query.Where(it => it.Name.ToLowerInvariant().Contains(searchParams.NameContains.ToLowerInvariant()));

            if (!string.IsNullOrEmpty(searchParams.DescriptionContains))
                query = query.Where(it => it.Description.ToLowerInvariant().Contains(searchParams.DescriptionContains.ToLowerInvariant()));

            bool ascending = (searchParams.SortOrder?.ToLowerInvariant() != "desc");
            query = searchParams.SortBy?.ToLowerInvariant() switch
            {
                "description" => ascending ? query.OrderBy(it => it.Description) : query.OrderByDescending(it => it.Description),
                _ => ascending ? query.OrderBy(it => it.Name) : query.OrderByDescending(it => it.Name),
            };

            var totalCount = await query.CountAsync();
            var ingredientTypes = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var ingredientTypeDTOs = ingredientTypes.Select(it => new IngredientTypeIdDTO
            {
                Id = it.Id,
                Name = it.Name,
                Description = it.Description
            }).ToList();

            return new PaginatedResponseDTO<IngredientTypeIdDTO>
            {
                Items = ingredientTypeDTOs,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

    }
}
