using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Models;
using Models.DTO;

namespace BackEngin.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipeService _recipeService;

        public BlogService(IUnitOfWork unitOfWork, IRecipeService recipeService)
        {
            _unitOfWork = unitOfWork;
            _recipeService = recipeService;
        }

        public async Task<PaginatedResponseDTO<BlogDTO>> GetBlogs(int pageNumber, int pageSize)
        {
            var (blogs, totalCount) = await _unitOfWork.Blogs.GetPaginatedAsync(
                filter: null, // No specific filter for now
                pageNumber: pageNumber,
                pageSize: pageSize
            );

            var blogDtos = blogs.Select(b => new BlogDTO
            {
                Id = b.Id,
                Header = b.Header,
                BodyText = b.BodyText,
                UserId = b.UserId,
                RecipeId = b.RecipeId
            }).ToList();

            return new PaginatedResponseDTO<BlogDTO>
            {
                Items = blogDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }


        public async Task<BlogDTO> CreateBlog(string userId, CreateBlogDTO createBlogDTO)
        {
            int? recipeId = createBlogDTO.RecipeId;

            // Check if a valid RecipeId is provided
            if (recipeId.HasValue)
            {
                // Validate if the recipe exists
                var existingRecipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId.Value);
                if (existingRecipe == null)
                {
                    throw new ArgumentException("The provided RecipeId does not exist.");
                }
            }
            else
            {
                // Check if a new recipe needs to be created
                if (createBlogDTO.Recipe != null)
                {
                    // Create a new recipe
                    var createRecipeDTO = new CreateRecipeDTO
                    {
                        Header = createBlogDTO.Recipe.Header,
                        BodyText = createBlogDTO.Recipe.BodyText,
                        Ingredients = createBlogDTO.Recipe.Ingredients
                    };

                    var createdRecipe = await _recipeService.CreateRecipe(userId, createRecipeDTO);
                    recipeId = createdRecipe.Id; // Link the new recipe to the blog
                }
            }            

            // Create the new blog
            var newBlog = new Blogs
            {
                Header = createBlogDTO.Header,
                BodyText = createBlogDTO.BodyText,
                UserId = userId,
                RecipeId = recipeId // This will be null if no recipe is provided
            };

            await _unitOfWork.Blogs.AddAsync(newBlog);
            await _unitOfWork.CompleteAsync();

            // Return the created blog DTO
            return new BlogDTO
            {
                Id = newBlog.Id,
                Header = newBlog.Header,
                BodyText = newBlog.BodyText,
                UserId = newBlog.UserId,
                RecipeId = newBlog.RecipeId // This will be null if no recipe was linked
            };
        }

        public async Task<BlogDTO> UpdateBlog(int blogId, UpdateBlogDTO updateBlogDTO)
        {
            // Fetch the blog by ID
            var blog = await _unitOfWork.Blogs.GetByIdAsync(blogId);
            if (blog == null) return null;

            // Update the blog fields
            blog.Header = updateBlogDTO.Header;
            blog.BodyText = updateBlogDTO.BodyText;
            blog.UserId = blog.UserId; // its stupid but the owner does not change hehe

            // Handle recipe updates
            if (updateBlogDTO.Recipe != null)
            {
                if (blog.RecipeId.HasValue)
                {
                    // If a recipe is already associated, update it
                    await _recipeService.UpdateRecipe(blog.RecipeId.Value, updateBlogDTO.Recipe);
                }
                else
                {
                    // If no recipe is associated, create a new one
                    var createRecipeDTO = new CreateRecipeDTO
                    {
                        Header = updateBlogDTO.Recipe.Header,
                        BodyText = updateBlogDTO.Recipe.BodyText,
                        Ingredients = updateBlogDTO.Recipe.Ingredients
                    };

                    var newRecipe = await _recipeService.CreateRecipe(blog.UserId, createRecipeDTO);
                    blog.RecipeId = newRecipe.Id; // Associate the newly created recipe with the blog
                }
            }
            else if (updateBlogDTO.RecipeId == null && blog.RecipeId.HasValue)
            {
                // If RecipeId is null, remove the existing recipe and its ingredients
                await _recipeService.DeleteRecipe(blog.RecipeId.Value);
                blog.RecipeId = null;
            }

            // Update the blog
            _unitOfWork.Blogs.Update(blog);
            await _unitOfWork.CompleteAsync();

            // Return the updated blog details
            return new BlogDTO
            {
                Id = blog.Id,
                Header = blog.Header,
                BodyText = blog.BodyText,
                UserId = blog.UserId,
                RecipeId = blog.RecipeId
            };
        }

        public async Task<BlogDetailDTO> GetBlogById(int blogId)
        {
            // Fetch the blog by ID
            var blog = await _unitOfWork.Blogs.GetByIdAsync(blogId);
            if (blog == null) return null;

            // Fetch recipe details if associated
            RecipeDetailsDTO recipeDetails = await GetRecipeOfBlog(blogId);

            // Map and return the blog details along with recipe details
            return new BlogDetailDTO
            {
                Id = blog.Id,
                Header = blog.Header,
                BodyText = blog.BodyText,
                UserId = blog.UserId,
                RecipeId = blog.RecipeId,
                RecipeHeader = recipeDetails?.Header,
                Recipe = recipeDetails // Include the full recipe details
            };
        }

        public async Task<RecipeDetailsDTO> GetRecipeOfBlog(int blogId)
        {
            // Fetch the blog and ensure it has an associated recipe
            var blog = await _unitOfWork.Blogs.GetByIdAsync(blogId);
            if (blog == null) return null;
            if (blog.RecipeId == null) return null;

            // Use RecipeService to fetch recipe details
            return await _recipeService.GetRecipeDetails(blog.RecipeId.Value);
        }

        public async Task<bool> DeleteBlog(int blogId)
        {
            var blog = await _unitOfWork.Blogs.GetByIdAsync(blogId);
            if (blog == null) return false;

            // If the blog has an associated recipe, delete the recipe
            if (blog.RecipeId.HasValue)
            {
                var recipeDeleted = await _recipeService.DeleteRecipe(blog.RecipeId.Value);
                if (!recipeDeleted) return false;
            }

            _unitOfWork.Blogs.Delete(blog);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<string?> GetOwner(int blogId)
        {
            var blog = await _unitOfWork.Blogs.GetByIdAsync(blogId);
            if (blog == null)
                return null;
            return blog.UserId;
        }
    }
}
