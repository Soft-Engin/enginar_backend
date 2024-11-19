using BackEngin.Services.Interfaces;
using DataAccess.Migrations;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BackEngin.Services
{
    public class AllergenService : IAllergenService
    {
        private readonly IUnitOfWork unitOfWork;

        public AllergenService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<IEnumerable<AllergenDTO>> GetAllAllergensAsync()
        {
            var allergens = await unitOfWork.Preferences.GetAllAsync();

            // Map each Allergen to an AllergenDTO
            var allergenDTOs = allergens.Select(a => new AllergenDTO
            {
                Name = a.Name,
                Description = a.Description
            });

            return allergenDTOs;
        }

        public async Task<IResult> CreateAllergenAsync(AllergenDTO model)
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
                return Results.Ok(new { Id = allergen.Id, message = "Allergen created successfully!" });
            }
            catch (Exception ex)
            {
                // Handle errors
                return Results.BadRequest(new { errors = ex.Message });
            }
        }


        public async Task<IResult> DeleteAllergenAsync(int allergenId)
        {
            try
            {
                // Find the allergen by its ID
                var allergen = await unitOfWork.Preferences.GetByIdAsync(allergenId);

                if (allergen == null)
                {
                    // Return 404 if allergen not found
                    return Results.NotFound(new { message = $"Allergen with ID {allergenId} not found." });
                }

                // Delete the allergen
                unitOfWork.Preferences.Delete(allergen);
                await unitOfWork.CompleteAsync();

                // Return success
                return Results.Ok(new { message = "Allergen deleted successfully." });
            }
            catch (Exception ex)
            {
                // Handle errors
                return Results.BadRequest(new { errors = ex.Message });
            }
        }


        public async Task<IResult> UpdateAllergenAsync(int allergenId, AllergenDTO model)
        {
            try
            {
                // Find the allergen by its ID
                var allergen = await unitOfWork.Preferences.GetByIdAsync(allergenId);

                if (allergen == null)
                {
                    // Return 404 if allergen not found
                    return Results.NotFound(new { message = $"Allergen with ID {allergenId} not found." });
                }

                // Update the allergen's properties
                allergen.Name = model.Name;
                allergen.Description = model.Description;

                // Save the changes
                unitOfWork.Preferences.Update(allergen);
                await unitOfWork.CompleteAsync();

                // Return success
                return Results.Ok(new { message = "Allergen updated successfully." });
            }
            catch (Exception ex)
            {
                // Handle errors
                return Results.BadRequest(new { errors = ex.Message });
            }
        }

    }
}
