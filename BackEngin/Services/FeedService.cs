using BackEngin.Data;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;
using NuGet.Protocol;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace BackEngin.Services
{
    public class FeedService : IFeedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventService _eventService;


        public FeedService(IUnitOfWork unitOfWork, IEventService eventService)
        {
            _unitOfWork = unitOfWork;
            _eventService = eventService;
        }

        public async Task<PaginatedResponseDTO<EventDTO>> GetEventFeed(string seed, int page, int pageSize)
        {
            // Step 1: Calculate seed-based values
            var seedVal = GetSeedValue(seed);
            var mult = GetMultiplier(seedVal);
            var limiter = (uint)10000; // Cap weights for consistent ordering

            // Step 2: Fetch paginated recipes with calculated weights
            var (events, totalCount) = await _unitOfWork.Events.GetPaginatedBySeedAsync(
                multiplier: mult,
                seedValue: seedVal,
                limiter: limiter,
                pageNumber: page,
                pageSize: pageSize,
                includeProperties: "Creator,Address,Address.District,Address.District.City,Address.District.City.Country",
                predicate: e => e.Date > DateTime.Now
            );

            // Step 3: Map to DTO
            var eventDTOs = events.Select(_eventService.MapEventToDto).ToList();

            // Step 4: Construct and return the paginated response
            return new PaginatedResponseDTO<EventDTO>
            {
                Items = eventDTOs,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResponseDTO<BlogDTO>> GetBlogsFeed(string seed, int page, int pageSize)
        {
            // Step 1: Calculate seed-based values
            var seedVal = GetSeedValue(seed);
            var mult = GetMultiplier(seedVal);
            var limiter = (uint)10000; // Cap weights for consistent ordering

            // Step 2: Fetch paginated recipes with calculated weights
            var (blogs, totalCount) = await _unitOfWork.Blogs.GetPaginatedBySeedAsync(
                multiplier: mult,
                seedValue: seedVal,
                limiter: limiter,
                pageNumber: page,
                pageSize: pageSize,
                includeProperties: "User"
            );

            // Step 3: Map to DTO
            var postDTOs = blogs.Select(b => new BlogDTO
            {
                Id = b.Id,
                Header = b.Header,
                BodyText = b.BodyText,
                UserId = b.UserId,
                UserName = b.User.UserName,
                RecipeId = b.RecipeId,
                CreatedAt = b.CreatedAt
            }).ToList();

            // Step 4: Construct and return the paginated response
            return new PaginatedResponseDTO<BlogDTO>
            {
                Items = postDTOs,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResponseDTO<RecipeDTO>> GetRecipeFeed(string seed, int page, int pageSize, string? userId)
        {
            // Step 1: Calculate seed-based values
            var seedVal = GetSeedValue(seed);
            var mult = GetMultiplier(seedVal);
            var limiter = (uint)10000; // Cap weights for consistent ordering

            (IEnumerable<Recipes> recipes, int totalCount) = (null, 0);

            //if guest user
            if (userId == null)
            {
                // Step 2: Fetch paginated recipes with calculated weights
                (recipes, totalCount) = await _unitOfWork.Recipes.GetPaginatedBySeedAsync(
                    multiplier: mult,
                    seedValue: seedVal,
                    limiter: limiter,
                    pageNumber: page,
                    pageSize: pageSize,
                    includeProperties: "User"
                );

            }
            else
            {
                // 1. Fetch and materialize userAllergens
                var userAllergens = (await _unitOfWork.User_Allergens.FindAsync(r => r.UserId == userId)).ToList();

                // 2. Now that userAllergens is fully enumerated, we can safely use it in the second query
                var userAllergenIngredientIds = (await _unitOfWork.Ingredients_Preferences.FindAsync(
                    ip => userAllergens.Any(ua => ua.PreferenceId == ip.PreferenceId))).Select(i => i.IngredientId).ToList();

                //step 2: fetch paginated recipes with calculated weights and user allergens
                (recipes, totalCount) = await _unitOfWork.Recipes.GetPaginatedBySeedAsync(
                    multiplier: mult,
                    seedValue: seedVal,
                    limiter: limiter,
                    pageNumber: page,
                    pageSize: pageSize,
                    includeProperties: "User,Recipes_Ingredients",
                    predicate: r => !r.Recipes_Ingredients.Any(i => userAllergenIngredientIds.Contains(i.IngredientId))
                    );
            }

            // Step 3: Map to DTO
            var recipeDTOs = recipes.Select(r => new RecipeDTO
            {
                Id = r.Id,
                Header = r.Header,
                BodyText = r.BodyText,
                UserId = r.UserId,
                UserName = r.User.UserName,
                ServingSize = r.ServingSize,
                PreparationTime = r.PreparationTime,
                Steps = r.Steps,
                CreatedAt = r.CreatedAt
            }).ToList();

            // Step 4: Construct and return the paginated response
            return new PaginatedResponseDTO<RecipeDTO>
            {
                Items = recipeDTOs,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }


        private uint GetMultiplier(uint seedValue)
        {
            uint multiplier = (seedValue % 6131) + 5531; // Ensures multiplier is >= 31 and variable

            return multiplier;
        }

        private uint GetSeedValue(string seed)
        {
            uint seedValue = (uint)seed.Aggregate(0, (current, ch) => current * 1931 + ch);

            return seedValue;
        }

        public async Task<PaginatedResponseDTO<BlogDTO>> GetFollowedRecentBlogsFeed(int page, int pageSize, string userId)
        {
            //get followings from db
            var followings = await _unitOfWork.Users.GetAllFollowingAsync(userId);

            var followingIdList = followings.Select(user => user.UserId).ToList();

            //get feed from db and order by date
            var (blogs, totalCount) = await _unitOfWork.Blogs.GetPaginatedByFollowedAsync(r => followingIdList.Contains(r.UserId), page, pageSize, includeProperties: "User");


            //map to DTO
            var blogDTOs = blogs.Select(b => new BlogDTO
            {
                Id = b.Id,
                Header = b.Header,
                BodyText = b.BodyText,
                UserId = b.UserId,
                UserName = b.User.UserName,
                RecipeId = b.RecipeId,
                CreatedAt = b.CreatedAt
            }).ToList();

            //return paginated response
            return new PaginatedResponseDTO<BlogDTO>
            {
                Items = blogDTOs,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };

        }

        public async Task<PaginatedResponseDTO<RecipeDTO>> GetFollowedRecentRecipeFeed(int page, int pageSize, string userId)
        {
            //get followings from db
            var followings = await _unitOfWork.Users.GetAllFollowingAsync(userId);

            //get following id list
            var followingIdList = followings.Select(u => u.UserId).ToList();

            // 1. Fetch and materialize userAllergens
            var userAllergens = (await _unitOfWork.User_Allergens.FindAsync(r => r.UserId == userId)).ToList();

            // 2. Now that userAllergens is fully enumerated, we can safely use it in the second query
            var userAllergenIngredientIds = (await _unitOfWork.Ingredients_Preferences.FindAsync(
                ip => userAllergens.Any(ua => ua.PreferenceId == ip.PreferenceId))).Select(i => i.IngredientId).ToList();

            //get feed from db and order by date
            var (recipes, totalCount) = await _unitOfWork.Recipes.GetPaginatedByFollowedAsync(
                predicate: r => !r.Recipes_Ingredients.Any(i => userAllergenIngredientIds.Contains(i.IngredientId)) && followingIdList.Contains(r.UserId),
                page,
                pageSize,
                includeProperties: "User,Recipes_Ingredients");


            // Step 3: Map to DTO
            var recipeDTOs = recipes.Select(r => new RecipeDTO
            {
                Id = r.Id,
                Header = r.Header,
                BodyText = r.BodyText,
                UserId = r.UserId,
                UserName = r.User.UserName,
                ServingSize = r.ServingSize,
                PreparationTime = r.PreparationTime,
                Steps = r.Steps,
                CreatedAt = r.CreatedAt
            }).ToList();

            // Step 4: Construct and return the paginated response
            return new PaginatedResponseDTO<RecipeDTO>
            {
                Items = recipeDTOs,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };

        }

        public async Task<PaginatedResponseDTO<EventDTO>> GetFollowedRecentEventFeed(int page, int pageSize, string userId)
        {
            //get followings from db
            var followings = await _unitOfWork.Users.GetAllFollowingAsync(userId);

            var followingIdList = followings.Select(user => user.UserId).ToList();

            // Step 2: Fetch paginated recipes with calculated weights
            var (events, totalCount) = await _unitOfWork.Events.GetPaginatedByFollowedAsync(
                pageNumber: page,
                pageSize: pageSize,
                includeProperties: "Creator,Address,Address.District,Address.District.City,Address.District.City.Country",
                predicate: e => e.Date > DateTime.Now && followingIdList.Contains(e.CreatorId)
            );


            // Step 3: Map to DTO
            var eventDTOs = events.Select(_eventService.MapEventToDto).ToList();

            // Step 4: Construct and return the paginated response
            return new PaginatedResponseDTO<EventDTO>
            {
                Items = eventDTOs,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };

        }

        public async Task<PaginatedResponseDTO<EventDTO>> GetFollowedUpcomingEventFeed(int page, int pageSize, string userId)
        {
            //get followings from db
            var followings = await _unitOfWork.Users.GetAllFollowingAsync(userId);

            var followingIdList = followings.Select(user => user.UserId).ToList();

            // Step 2: Fetch paginated recipes with calculated weights
            var (events, totalCount) = await _unitOfWork.Events.GetPaginatedByFollowedAsync(
                pageNumber: page,
                pageSize: pageSize,
                includeProperties: "Creator,Address,Address.District,Address.District.City,Address.District.City.Country",
                predicate: e => e.Date > DateTime.Now && followingIdList.Contains(e.CreatorId),
                orderBy: e => e.Date
            );


            // Step 3: Map to DTO
            var eventDTOs = events.Select(_eventService.MapEventToDto).ToList();

            // Step 4: Construct and return the paginated response
            return new PaginatedResponseDTO<EventDTO>
            {
                Items = eventDTOs,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize
            };

        }

        public async Task<PaginatedResponseDTO<UserCompactDTO>> GetPopularUserFeed(int page, int pageSize, string userId)
        {
            //get the last 1000 interactions from db wher ethe initiator is not user
            var interactions = await _unitOfWork.Users_Interactions.GetLastAsync(1000, predicate: u => u.InitiatorUserId != userId && u.TargetUserId != userId, includeProperties: "TargetUser");

            var mostFollowedUsersList = interactions.GroupBy(u => u.TargetUserId)
                .OrderByDescending(g => g.Count())
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            //return paginated
            return new PaginatedResponseDTO<UserCompactDTO>
            {
                Items = mostFollowedUsersList.Select(g => new UserCompactDTO
                {
                    UserId = g.First().TargetUserId,
                    UserName = g.First().TargetUser.UserName
                }).ToList(),
                TotalCount = mostFollowedUsersList.Count,
                PageNumber = page,
                PageSize = pageSize
            };

        }
    }
}