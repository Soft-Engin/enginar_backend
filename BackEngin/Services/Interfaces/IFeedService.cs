using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IFeedService
    {
        Task<PaginatedResponseDTO<BlogDTO>> GetBlogsFeed(string seed, int page, int pageSize);
        Task<PaginatedResponseDTO<RecipeDTO>> GetRecipeFeed(string seed, int page, int pageSize);
        Task<PaginatedResponseDTO<EventDTO>> GetEventFeed(string seed, int page, int pageSize);
        Task<PaginatedResponseDTO<BlogDTO>> GetFollowedRecentBlogsFeed( int page, int pageSize, string userId);
        Task<PaginatedResponseDTO<RecipeDTO>> GetFollowedRecentRecipeFeed(int page, int pageSize, string userId);
        Task<PaginatedResponseDTO<EventDTO>> GetFollowedRecentEventFeed(int page, int pageSize, string userId);
        Task<PaginatedResponseDTO<EventDTO>> GetFollowedUpcomingEventFeed(int page, int pageSize, string userId);

    }
}
