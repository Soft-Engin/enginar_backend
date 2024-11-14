using BackEngin.Services.Interfaces;
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
        private readonly DbContext _context;
        private readonly UserManager<Users> _userManager;

        public UserService(DbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _userManager.Users.Include(u => u.Address).Include(u => u.Role).ToListAsync();
        }

        public async Task<Users?> GetUserByIdAsync(string id)
        {
            return await _userManager.Users.Include(u => u.Address).Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Users> CreateUserAsync(Users user)
        {
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                // Handle creation failure (e.g., validation errors)
                return null;
            }
            return user;
        }

        public async Task<Users> UpdateUserAsync(string id, Users user)
        {
            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
            {
                return null;
            }

            // Update properties
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.AddressId = user.AddressId;
            existingUser.RoleId = user.RoleId;

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                // Handle update failure (e.g., validation errors)
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
