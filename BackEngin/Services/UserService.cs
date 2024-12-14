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

        public async Task<PaginatedResponseDTO<UserDTO>> GetAllUsersAsync(int page, int pageSize)
        {
            var totalCount = await _userManager.Users.CountAsync();

            var users = await _userManager.Users
                .Include(u => u.Address)
                .Include(u => u.Role)
                .Select(u => new UserDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Address = u.Address,
                    Role = u.Role,
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                })
                .Skip((page - 1) * pageSize)  // Skip the records of previous pages
                .Take(pageSize)               // Take the records for the current page
                .ToListAsync();

            return new PaginatedResponseDTO<UserDTO>
            {
                Items = users,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
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


        public async Task<UpdateUserDto> UpdateUserAsync(string id, UpdateUserDto userDTO)
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

            if (existingUser.Address != null)
            {
                existingUser.Address.Name = userDTO.AddressName;
                existingUser.Address.Street = userDTO.Street;
                existingUser.Address.District.Name = userDTO.District;
                existingUser.Address.District.City.Name = userDTO.City;
                existingUser.Address.District.City.Country.Name = userDTO.Country;
                existingUser.Address.District.PostCode = userDTO.PostCode;
            }
            else
            {
                existingUser.Address = new Addresses
                {
                    Name = userDTO.AddressName != null ? userDTO.AddressName : "AddressName",
                    Street = userDTO.Street != null ? userDTO.Street : "Street",
                    District = new Districts
                    {
                        Name = userDTO.District != null ? userDTO.District : "District",
                        City = new Cities
                        {
                            Name = userDTO.City != null ? userDTO.City : "City",
                            Country = new Countries { Name = userDTO.Country != null ? userDTO.Country : "Country" }
                        },
                        PostCode = userDTO.PostCode
                    }
                };
            }


            existingUser.Email = userDTO.Email;
            existingUser.UserName = userDTO.UserName;
            existingUser.PhoneNumber = userDTO.PhoneNumber;

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

            return new UpdateUserDto
            {
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                Email = existingUser.Email,
                UserName = existingUser.UserName,
                PhoneNumber = existingUser.PhoneNumber,
                AddressName = existingUser.Address != null ? existingUser.Address.Name : "AddressName",
                Street = existingUser.Address != null ? existingUser.Address.Street : "Street",
                District = existingUser.Address != null ? existingUser.Address.District.Name : "District",
                City = existingUser.Address != null ? existingUser.Address.District.City.Name : "City",
                Country = existingUser.Address != null ? existingUser.Address.District.City.Country.Name : "Country",
                PostCode = existingUser.Address != null ? existingUser.Address.District.PostCode : 15
            };
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

        public async Task<PaginatedResponseDTO<string>> GetFollowersAsync(string userId, int page, int pageSize)
        {
            var User = await _userManager.FindByIdAsync(userId);

            if (User == null)
            {
                throw new InvalidOperationException("User does not exist.");
            }

            return await _unitOfWork.Users.GetFollowersAsync(userId, page, pageSize);
        }

        public async Task<PaginatedResponseDTO<string>> GetFollowingAsync(string userId, int page, int pageSize)
        {
            var User = await _userManager.FindByIdAsync(userId);

            if (User == null)
            {
                throw new InvalidOperationException("User does not exist.");
            }

            return await _unitOfWork.Users.GetFollowingAsync(userId, page, pageSize);
        }


        public async Task<bool> FollowUserAsync(string initiatorUserId, string targetUserId)
        {
            if (initiatorUserId == targetUserId)
                throw new InvalidOperationException("Users cannot follow themselves.");

            // Probably not most efficient way to validate targetUserId
            // but i dont care
            var targetUser = await _userManager.FindByIdAsync(targetUserId);

            if (targetUser == null)
            {
                throw new InvalidOperationException("User does not exist.");
            }

            return await _unitOfWork.Users.FollowUserAsync(initiatorUserId, targetUserId);
        }

        public async Task<bool> UnfollowUserAsync(string initiatorUserId, string targetUserId)
        {
            return await _unitOfWork.Users.UnfollowUserAsync(initiatorUserId, targetUserId);
        }
        public async Task<PaginatedResponseDTO<BookmarkRecipesItemDTO>> GetBookmarkedRecipesAsync(string userId, int page, int pageSize)
        {
            // Probably not most efficient way to validate targetUserId
            // but i dont care
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return await _unitOfWork.Users_Recipes_Interactions.GetBookmarkedRecipesAsync(userId, page, pageSize);
        }


        public async Task<PaginatedResponseDTO<BookmarkBlogsItemDTO>> GetBookmarkedBlogsAsync(string userId, int page, int pageSize)
        {
            // Probably not most efficient way to validate targetUserId
            // but i dont care
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return await _unitOfWork.Users_Blogs_Interactions.GetBookmarkedBlogsAsync(userId, page, pageSize);
        }

        public async Task<PaginatedResponseDTO<UserDTO>> SearchUsersAsync(UserSearchParams searchParams, int pageNumber, int pageSize)
        {
            // Base query
            IQueryable<Users> query = _unitOfWork.Users.GetQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(searchParams.UserNameContains))
            {
                query = query.Where(u => u.UserName.Contains(searchParams.UserNameContains));
            }

            if (!string.IsNullOrEmpty(searchParams.First_LastNameContains))
            {
                query = query.Where(u => u.FirstName.Contains(searchParams.First_LastNameContains));
                query = query.Where(u => u.LastName.Contains(searchParams.First_LastNameContains));
            }

            if (!string.IsNullOrEmpty(searchParams.EmailContains))
            {
                query = query.Where(u => u.Email.Contains(searchParams.EmailContains));
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(searchParams.SortBy))
            {
                query = searchParams.SortBy switch
                {
                    "UserName" => searchParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(u => u.UserName) : query.OrderBy(u => u.UserName),
                    "Name" => searchParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(u => u.FirstName + u.LastName) : query.OrderBy(u => u.FirstName + u.LastName),
                    "Email" => searchParams.SortOrder.ToLower() == "desc" ? query.OrderByDescending(u => u.Email) : query.OrderBy(u => u.Email),
                    _ => query.OrderBy(u => u.UserName) // Default sorting by Name
                };
            }

            // Fetch paginated results
            var totalCount = await query.CountAsync();
            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Map to DTOs
            var userDTOs = users.Select(u => new UserDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address = u.Address,
                Role = u.Role,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).ToList();

            return new PaginatedResponseDTO<UserDTO>
            {
                Items = userDTOs,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
