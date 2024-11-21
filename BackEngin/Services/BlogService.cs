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

        public async Task<IEnumerable<BlogDTO>> GetBlogs()
        {
            var blogs = await _unitOfWork.Blogs.GetAllAsync();

            return blogs.Select(b => new BlogDTO
            {
                Id = b.Id,
                Header = b.Header,
                BodyText = b.BodyText,
                UserId = b.UserId,
                RecipeId = b.RecipeId
            }).ToList();
        }

        public async Task<BlogDTO> CreateBlog(CreateBlogDTO createBlogDTO)
        {
            int? recipeId = createBlogDTO.RecipeId;

            // If a recipe DTO is provided, create the recipe
            if (createBlogDTO.Recipe != null)
            {
                var createRecipeDTO = new CreateRecipeDTO
                {
                    Header = createBlogDTO.Recipe.Header,
                    BodyText = createBlogDTO.Recipe.BodyText,
                    UserId = createBlogDTO.UserId, // Pass the user ID
                    Ingredients = createBlogDTO.Recipe.Ingredients
                };

                var createdRecipe = await _recipeService.CreateRecipe(createRecipeDTO);
                recipeId = createdRecipe.Id; // Use the newly created recipe ID
            }

            // Create the new blog with or without a recipe
            var newBlog = new Blogs
            {
                Header = createBlogDTO.Header,
                BodyText = createBlogDTO.BodyText,
                UserId = createBlogDTO.UserId,
                RecipeId = recipeId // Use the recipe ID if available
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
                RecipeId = newBlog.RecipeId
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
                        UserId = blog.UserId,
                        Ingredients = updateBlogDTO.Recipe.Ingredients
                    };

                    var newRecipe = await _recipeService.CreateRecipe(createRecipeDTO);
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
            RecipeDTO recipeDetails = await GetRecipeOfBlog(blogId);

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

        public async Task<RecipeDTO> GetRecipeOfBlog(int blogId)
        {
            // Fetch the blog and ensure it has an associated recipe
            var blog = await _unitOfWork.Blogs.GetByIdAsync(blogId);
            if (blog?.RecipeId == null) return null;

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
    }
}
