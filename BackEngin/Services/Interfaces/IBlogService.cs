﻿using Models.DTO;

namespace BackEngin.Services.Interfaces
{
    public interface IBlogService
    {
        Task<PaginatedResponseDTO<BlogDTO>> GetBlogs(int pageNumber, int pageSize);
        Task<BlogDTO> CreateBlog(string userId, CreateBlogDTO createBlogDTO);
        Task<BlogDTO> UpdateBlog(int blogId, UpdateBlogDTO updateBlogDTO);
        Task<bool> DeleteBlog(int blogId);
        Task<BlogDetailDTO> GetBlogById(int blogId);
        Task<RecipeDetailsDTO> GetRecipeOfBlog(int blogId);
        Task<string?> GetOwner(int blogId);
        Task<PaginatedResponseDTO<BlogDTO>> SearchBlogs(BlogSearchParams searchParams, int pageNumber, int pageSize);
        Task<byte[]?> GetBlogBannerImage(int blogId);
    }
}
