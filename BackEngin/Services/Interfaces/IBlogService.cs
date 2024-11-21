using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogDTO>> GetBlogs();
        Task<BlogDTO> CreateBlog(CreateBlogDTO createBlogDTO);
        Task<BlogDTO> UpdateBlog(int blogId, UpdateBlogDTO updateBlogDTO);
        Task<bool> DeleteBlog(int blogId);
        Task<BlogDetailDTO> GetBlogById(int blogId);
        Task<RecipeDTO> GetRecipeOfBlog(int blogId);
    }
}
