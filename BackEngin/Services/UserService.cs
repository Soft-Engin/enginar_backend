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

        public async Task<Users?> GetUserByIdAsync(string id)
        {
            return await _userManager.Users.Include(u => u.Address).Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
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
            existingUser.RoleId = userDTO.RoleId;
            existingUser.AddressId = userDTO.AddressId;
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
    }
}
