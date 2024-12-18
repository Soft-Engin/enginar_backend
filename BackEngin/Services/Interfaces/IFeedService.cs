using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IFeedService
    {
        Task<PaginatedResponseDTO<BlogDTO>> GetBlogsFeed(string seed, int page, int pageSize);
        Task<PaginatedResponseDTO<RecipeDTO>> GetRecipeFeed(string seed, int page, int pageSize);
        Task<PaginatedResponseDTO<UserDTO>> GetEventFeed(string seed, int page, int pageSize);
    }
}
