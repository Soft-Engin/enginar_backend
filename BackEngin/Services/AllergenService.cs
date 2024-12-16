using BackEngin.Services.Interfaces;
using DataAccess.Migrations;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace BackEngin.Services
{
    public class AllergenService : IAllergenService
    {
        private readonly IUnitOfWork unitOfWork;

        public AllergenService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<PaginatedResponseDTO<AllergenIdDTO>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var (allergens, totalCount) = await unitOfWork.Preferences.GetPaginatedAsync(
                pageNumber: pageNumber,
                pageSize: pageSize);

            // Map each Allergen to an AllergenDTO
            var allergenDTOs = allergens.Select(a => new AllergenIdDTO
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description
            });

            return new PaginatedResponseDTO<AllergenIdDTO>
            {
                Items = allergenDTOs,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<int?> CreateAllergenAsync(AllergenDTO model)
        {
            try
            {
                // Convert DTO to entity
                var allergen = new Preferences
                {
                    Name = model.Name,
                    Description = model.Description
                };

                // Add the new allergen to the database
                await unitOfWork.Preferences.AddAsync(allergen);

                // Save changes to ensure the allergen is stored, and the ID is assigned
                await unitOfWork.CompleteAsync();

                // Return success message with the new allergen's ID
                return allergen.Id;
            }
            catch (Exception ex)
            {
                // Handle errors
                return null;
            }
        }


        public async Task<bool?> DeleteAllergenAsync(int allergenId)
        {
            try
            {
                // Find the allergen by its ID
                var allergen = await unitOfWork.Preferences.GetByIdAsync(allergenId);

                if (allergen == null)
                {
                    // Return 404 if allergen not found
                    return null;
                }

                // Delete the allergen
                unitOfWork.Preferences.Delete(allergen);
                await unitOfWork.CompleteAsync();

                // Return success
                return true;
            }
            catch (Exception ex)
            {
                // Handle errors
                return false;
            }
        }


        public async Task<bool?> UpdateAllergenAsync(int allergenId, AllergenDTO model)
        {
            try
            {
                // Find the allergen by its ID
                var allergen = await unitOfWork.Preferences.GetByIdAsync(allergenId);

                if (allergen == null)
                {
                    // Return 404 if allergen not found
                    return null;
                }

                // Update the allergen's properties
                allergen.Name = model.Name;
                allergen.Description = model.Description;

                // Save the changes
                unitOfWork.Preferences.Update(allergen);
                await unitOfWork.CompleteAsync();

                // Return success
                return true;
            }
            catch (Exception ex)
            {
                // Handle errors
                return false;
            }
        }

    }
}
