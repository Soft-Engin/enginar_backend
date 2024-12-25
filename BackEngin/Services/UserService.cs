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
                .Include(b => b.Bio)
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
                RoleName = user.Role?.Name ?? "Role Name",
                Bio = user.Bio ?? "Bio"
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

            // Update images if provided
            if (userDTO.BannerImage != null)
                existingUser.BannerImage = userDTO.BannerImage;
            if (userDTO.ProfileImage != null)
                existingUser.ProfileImage = userDTO.ProfileImage;

            if(userDTO.Email != null) 
                existingUser.Email = userDTO.Email;

            if(userDTO.UserName != null) 
                existingUser.UserName = userDTO.UserName;
            if (userDTO.PhoneNumber != null)
                existingUser.PhoneNumber = userDTO.PhoneNumber;

            if (userDTO.Bio != null)
                existingUser.Bio = userDTO.Bio;

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
                PostCode = existingUser.Address != null ? existingUser.Address.District.PostCode : 15,
                // Not returning image here, you need to fetch it separately. This field stays because it will get messy to create another DTO for this purpose. This endpoint is already a big mess...
                BannerImage = null,
                ProfileImage = null,
                Bio = existingUser.Bio
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
            // Step 1: Fetch the bookmarked recipe IDs for the given user from the Recipe_Bookmarks table
            var bookmarkedRecipeIds = (await _unitOfWork.Recipe_Bookmarks.FindAsync(rb => rb.UserId == userId))
                .Select(rb => rb.RecipeId)
                .ToList();

            if (!bookmarkedRecipeIds.Any())
            {
                return new PaginatedResponseDTO<BookmarkRecipesItemDTO>
                {
                    Items = new List<BookmarkRecipesItemDTO>(),
                    TotalCount = 0,
                    PageNumber = page,
                    PageSize = pageSize
                };
            }

            // Step 2: Fetch the recipes with the matching IDs from the Recipes table
            var recipesQuery = (await _unitOfWork.Recipes.FindAsync(r => bookmarkedRecipeIds.Contains(r.Id))).AsQueryable();

            // Calculate total count and apply pagination
            var totalCount = recipesQuery.Count();
            var paginatedRecipes = recipesQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Step 3: Map the data to BookmarkRecipesItemDTO
            var recipeDtos = paginatedRecipes.Select(r => new BookmarkRecipesItemDTO
            {
                UserName = r.User?.UserName ?? "Unknown",
                Header = r.Header,
                BodyText = r.BodyText
            }).ToList();

            // Step 4: Return the paginated response
            return new PaginatedResponseDTO<BookmarkRecipesItemDTO>
            {
                Items = recipeDtos,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }


        public async Task<PaginatedResponseDTO<LikedRecipesItemDTO>> GetLikedRecipesAsync(string userId, int page, int pageSize)
        {
            // Step 1: Fetch the liked recipe IDs for the given user from the Recipe_Likes table
            var likedRecipeIds = (await _unitOfWork.Recipe_Likes.FindAsync(bl => bl.UserId == userId))
                .Select(bl => bl.RecipeId)
                .ToList();

            if (!likedRecipeIds.Any())
            {
                return new PaginatedResponseDTO<LikedRecipesItemDTO>
                {
                    Items = new List<LikedRecipesItemDTO>(),
                    TotalCount = 0,
                    PageNumber = page,
                    PageSize = pageSize
                };
            }

            // Step 2: Fetch the recipes with the matching IDs from the Recipes table
            var recipesQuery = (await _unitOfWork.Recipes.FindAsync(b => likedRecipeIds.Contains(b.Id))).AsQueryable();

            // Calculate total count and apply pagination
            var totalCount = recipesQuery.Count();
            var paginatedRecipes = recipesQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Step 3: Map the data to LikedRecipesItemDTO
            var recipeDtos = paginatedRecipes.Select(b => new LikedRecipesItemDTO
            {
                UserName = b.User?.UserName ?? "Unknown",
                Header = b.Header,
                BodyText = b.BodyText
            }).ToList();

            // Step 4: Return the paginated response
            return new PaginatedResponseDTO<LikedRecipesItemDTO>
            {
                Items = recipeDtos,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }



        public async Task<PaginatedResponseDTO<BookmarkBlogsItemDTO>> GetBookmarkedBlogsAsync(string userId, int page, int pageSize)
        {
            // Step 1: Fetch the bookmarked blog IDs for the given user from the Blog_Bookmarks table
            var bookmarkedBlogIds = (await _unitOfWork.Blog_Bookmarks.FindAsync(bb => bb.UserId == userId))
                .Select(bb => bb.BlogId)
                .ToList();

            if (!bookmarkedBlogIds.Any())
            {
                return new PaginatedResponseDTO<BookmarkBlogsItemDTO>
                {
                    Items = new List<BookmarkBlogsItemDTO>(),
                    TotalCount = 0,
                    PageNumber = page,
                    PageSize = pageSize
                };
            }

            // Step 2: Fetch the blogs with the matching IDs from the Blogs table
            var blogsQuery = (await _unitOfWork.Blogs.FindAsync(b => bookmarkedBlogIds.Contains(b.Id))).AsQueryable();

            // Calculate total count and apply pagination
            var totalCount = blogsQuery.Count();
            var paginatedBlogs = blogsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Step 3: Map the data to BookmarkBlogsItemDTO
            var blogDtos = paginatedBlogs.Select(b => new BookmarkBlogsItemDTO
            {
                UserName = b.User?.UserName ?? "Unknown",
                Header = b.Header,
                BodyText = b.BodyText
            }).ToList();

            // Step 4: Return the paginated response
            return new PaginatedResponseDTO<BookmarkBlogsItemDTO>
            {
                Items = blogDtos,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResponseDTO<LikedBlogsItemDTO>> GetLikedBlogsAsync(string userId, int page, int pageSize)
        {
            // Step 1: Fetch the liked blog IDs for the given user from the Blog_Likes table
            var likedBlogIds = (await _unitOfWork.Blog_Likes.FindAsync(bl => bl.UserId == userId))
                .Select(bl => bl.BlogId)
                .ToList();

            if (!likedBlogIds.Any())
            {
                return new PaginatedResponseDTO<LikedBlogsItemDTO>
                {
                    Items = new List<LikedBlogsItemDTO>(),
                    TotalCount = 0,
                    PageNumber = page,
                    PageSize = pageSize
                };
            }

            // Step 2: Fetch the blogs with the matching IDs from the Blogs table
            var blogsQuery = (await _unitOfWork.Blogs.FindAsync(b => likedBlogIds.Contains(b.Id))).AsQueryable();

            // Calculate total count and apply pagination
            var totalCount = blogsQuery.Count();
            var paginatedBlogs = blogsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Step 3: Map the data to LikedBlogsItemDTO
            var blogDtos = paginatedBlogs.Select(b => new LikedBlogsItemDTO
            {
                UserName = b.User?.UserName ?? "Unknown",
                Header = b.Header,
                BodyText = b.BodyText
            }).ToList();

            // Step 4: Return the paginated response
            return new PaginatedResponseDTO<LikedBlogsItemDTO>
            {
                Items = blogDtos,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResponseDTO<UserDTO>> SearchUsersAsync(UserSearchParams searchParams, int pageNumber, int pageSize)
        {
            // Base query
            IQueryable<Users> query = _unitOfWork.Users.GetQueryable();

            // Apply filtering with case-insensitive comparisons
            if (!string.IsNullOrEmpty(searchParams.UserNameContains))
            {
                query = query.Where(u => u.UserName.ToLower().Contains(searchParams.UserNameContains.ToLower()));
            }

            if (!string.IsNullOrEmpty(searchParams.First_LastNameContains))
            {
                var searchTerm = searchParams.First_LastNameContains.ToLower();
                query = query.Where(u => u.FirstName.ToLower().Contains(searchTerm) 
                                        || u.LastName.ToLower().Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(searchParams.EmailContains))
            {
                query = query.Where(u => u.Email.ToLower().Contains(searchParams.EmailContains.ToLower()));
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

        public async Task<PaginatedResponseDTO<RecipeDTO>> GetUserRecipesAsync(string userId, int pageNumber, int pageSize)
        {
            var (recipes, totalCount) = await _unitOfWork.Recipes.GetPaginatedAsync(
                filter: r => r.UserId == userId,
                pageNumber: pageNumber,
                pageSize: pageSize
            );

            if (!recipes.Any())
            {
                return new PaginatedResponseDTO<RecipeDTO>
                {
                    Items = new List<RecipeDTO>(),
                    TotalCount = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }

            var relatedUser = await GetUserByIdAsync(userId);

            if (relatedUser == null)
            {
                throw new ArgumentException();
            }

            var recipeDtos = recipes.Select(r => new RecipeDTO
            {
                Id = r.Id,
                Header = r.Header,
                BodyText = r.BodyText,
                UserId = r.UserId,
                UserName = relatedUser.UserName,
                CreatedAt = r.CreatedAt,
                PreparationTime = r.PreparationTime,
                ServingSize = r.ServingSize,
                Steps = r.Steps
            }).ToList();

            return new PaginatedResponseDTO<RecipeDTO>
            {
                Items = recipeDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResponseDTO<BlogDTO>> GetUserBlogsAsync(string userId, int pageNumber, int pageSize)
        {
            var (blogs, totalCount) = await _unitOfWork.Blogs.GetPaginatedAsync(
                filter: b => b.UserId == userId,
                includeProperties: "User,Recipe",
                pageNumber: pageNumber,
                pageSize: pageSize
            );

            if (!blogs.Any())
            {
                return new PaginatedResponseDTO<BlogDTO>
                {
                    Items = new List<BlogDTO>(),
                    TotalCount = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }

            var relatedUser = await GetUserByIdAsync(userId);

            if (relatedUser == null)
            {
                throw new ArgumentException();
            }

            var blogDtos = blogs.Select(b => new BlogDTO
            {
                Id = b.Id,
                Header = b.Header,
                BodyText = b.BodyText,
                UserId = b.UserId,
                UserName = relatedUser.UserName,
                RecipeId = b.RecipeId,
                CreatedAt = b.CreatedAt
            }).ToList();

            return new PaginatedResponseDTO<BlogDTO>
            {
                Items = blogDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResponseDTO<EventDTO>> GetUserEventsAsync(string userId, int page, int pageSize)
        {
            // Step 1: Fetch the events created by the user
            var (items, totalCount) = await _unitOfWork.Events.GetPaginatedAsync(
                filter: e => e.CreatorId == userId,
                includeProperties: "Creator,Address,Address.District,Address.District.City,Address.District.City.Country",
                pageNumber: page,
                pageSize: pageSize
            );

            // Step 2: Map events to EventDTO directly inside the method
            var events = items.Select(e =>
            {
                // Ensure related entities are populated
                if (e.Creator == null)
                {
                    e.Creator = _unitOfWork.Users.FindAsync(u => u.Id == e.CreatorId).Result.First();
                }
                if (e.Address == null)
                {
                    e.Address = _unitOfWork.Addresses.FindAsync(u => u.Id == e.AddressId).Result.First();
                }
                if (e.Address.District == null)
                {
                    e.Address.District = _unitOfWork.Districts.FindAsync(u => u.Id == e.Address.DistrictId).Result.First();
                }
                if (e.Address.District.City == null)
                {
                    e.Address.District.City = _unitOfWork.Cities.FindAsync(u => u.Id == e.Address.District.CityId).Result.First();
                }
                if (e.Address.District.City.Country == null)
                {
                    e.Address.District.City.Country = _unitOfWork.Countries.FindAsync(u => u.Id == e.Address.District.City.CountryId).Result.First();
                }

                return new EventDTO
                {
                    Title = e.Title,
                    Date = e.Date,
                    EventId = e.Id,
                    BodyText = e.BodyText,
                    CreatorUserName = e.Creator?.UserName ?? "Unknown",
                    CreatorId = e.CreatorId,
                    Address = e.Address,
                    CreatedAt = e.CreatedAt,
                    Requirements = _unitOfWork.Events_Requirements
                        .FindAsync(r => r.EventId == e.Id, includeProperties: "Requirement")
                        .Result
                        .Select(er => new RequirementDTO
                        {
                            Id = er.Requirement.Id,
                            Name = er.Requirement.Name,
                            Description = er.Requirement.Description
                        })
                        .ToList(),
                    TotalParticipantsCount = _unitOfWork.User_Event_Participations.CountAsync(r => r.EventId == e.Id).Result
                };
            }).ToList();

            // Step 3: Return paginated response
            return new PaginatedResponseDTO<EventDTO>
            {
                Items = events,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public async Task<byte[]?> GetUserBannerImageAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            return user.BannerImage;
        }

        public async Task<byte[]?> GetUserProfileImageAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            return user.ProfileImage;
        }
    }
}
