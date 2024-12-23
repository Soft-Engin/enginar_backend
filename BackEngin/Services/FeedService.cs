using BackEngin.Data;
using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Models;
using Models.DTO;
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

        public async Task<PaginatedResponseDTO<RecipeDTO>> GetRecipeFeed(string seed, int page, int pageSize)
        {
            // Step 1: Calculate seed-based values
            var seedVal = GetSeedValue(seed);
            var mult = GetMultiplier(seedVal);
            var limiter = (uint)10000; // Cap weights for consistent ordering


            // Step 2: Fetch paginated recipes with calculated weights
            var (recipes, totalCount) = await _unitOfWork.Recipes.GetPaginatedBySeedAsync(
                multiplier: mult,
                seedValue: seedVal,
                limiter: limiter,
                pageNumber: page,
                pageSize: pageSize,
                includeProperties: "User"
            );

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

    }
}