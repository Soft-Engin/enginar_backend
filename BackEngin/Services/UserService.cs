using BackEngin.Data;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTO;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace BackEngin.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly IUnitOfWork _unitOfWork;


        public UserService(UserManager<Users> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _userManager.Users.Include(u => u.Address).Include(u => u.Role).ToListAsync();
        }

        public async Task<GetUserByIdDTO?> GetUserByIdAsync(string id)
        {
            // Retrieve the user with all necessary related entities included
            var user = await _userManager.Users
                .Include(u => u.Address)
                .ThenInclude(a => a.District)
                .ThenInclude(d => d.City)
                .ThenInclude(c => c.Country)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            // If no user is found, return null
            if (user == null)
                return null;

            // Map the retrieved user to the DTO
            return new GetUserByIdDTO
            {
                UserName = user.UserName ?? "User Name",
                Email = user.Email ?? "E-Mail",
                FirstName = user.FirstName,
                LastName = user.LastName,
                AddressName = user.Address?.Name ?? "Address Name",
                Street = user.Address?.Street ?? "Street",
                District = user.Address?.District?.Name ?? "District",
                City = user.Address?.District?.City?.Name ?? "City",
                Country = user.Address?.District?.City?.Country?.Name ?? "Country",
                PostCode = user.Address?.District?.PostCode ?? 0, // Default to 0 if null
                RoleName = user.Role?.Name ?? "Role Name"
            };
        }


        public async Task<Users> UpdateUserAsync(string id, UpdateUserDto userDTO)
        {
            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
            {
                Console.Write("User not found");
                return null;
            }

            // Update properties
            existingUser.FirstName = userDTO.FirstName;
            existingUser.LastName = userDTO.LastName;

            if (existingUser.Address != null) {
                existingUser.Address.Name = userDTO.AddressName;
                existingUser.Address.Street = userDTO.Street;
                existingUser.Address.District.Name = userDTO.District;
                existingUser.Address.District.City.Name = userDTO.City;
                existingUser.Address.District.City.Country.Name = userDTO.Country;
                existingUser.Address.District.PostCode = userDTO.PostCode;
            }
            

            existingUser.Email = userDTO.Email;
            existingUser.UserName = userDTO.UserName;

            var validationResult = await _userManager.UserValidators[0].ValidateAsync(_userManager, existingUser);
            if (!validationResult.Succeeded)
            {
                foreach (var error in validationResult.Errors)
                {
                    Console.WriteLine($"Validation Error: {error.Code} - {error.Description}");
                }
                return null;
            }

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                // Handle update failure (e.g., validation errors)
                Console.WriteLine("Update Failed");
                return null;
            }

            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<FollowersDTO> GetFollowersAsync(string userId)
        {
            return await _unitOfWork.Users.GetFollowersAsync(userId);
        }

        public async Task<FollowedUsersDTO> GetFollowingAsync(string userId)
        {
            return await _unitOfWork.Users.GetFollowingAsync(userId);
        }

        public async Task<bool> FollowUserAsync(string initiatorUserId, string targetUserId)
        {
            if (initiatorUserId == targetUserId)
                throw new InvalidOperationException("Users cannot follow themselves.");

            return await _unitOfWork.Users.FollowUserAsync(initiatorUserId, targetUserId);
        }

        public async Task<bool> UnfollowUserAsync(string initiatorUserId, string targetUserId)
        {
            return await _unitOfWork.Users.UnfollowUserAsync(initiatorUserId, targetUserId);
        }
    }
}
