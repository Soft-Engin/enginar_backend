using BackEngin.Services.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
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

            // finds user names of the blogs users
            var userIds = blogs.Select(b => b.UserId).Distinct().ToList();
            var users = await _unitOfWork.Users.FindAsync(u => userIds.Contains(u.Id));
            var userDictionary = users.ToDictionary(u => u.Id, u => u.UserName);

            var blogDtos = blogs.Select(b => new BlogDTO
            {
                Id = b.Id,
                Header = b.Header,
                BodyText = b.BodyText,
                UserId = b.UserId,
                UserName = userDictionary.ContainsKey(b.UserId) ? userDictionary[b.UserId] : "Unknown",
                RecipeId = b.RecipeId,
                CreatedAt = b.CreatedAt,
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
                        Ingredients = createBlogDTO.Recipe.Ingredients,
                        BannerImage = createBlogDTO.Recipe.BannerImage,
                        StepImages = createBlogDTO.Recipe.StepImages
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
                RecipeId = recipeId, // This will be null if no recipe is provided
                BannerImage = createBlogDTO.BannerImage,
                Images = createBlogDTO.Images,
                CreatedAt = DateTime.UtcNow,
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
                RecipeId = newBlog.RecipeId, // This will be null if no recipe was linked
                CreatedAt = newBlog.CreatedAt,
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
            blog.BannerImage = updateBlogDTO.BannerImage;
            blog.Images = updateBlogDTO.Images;

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
                        BannerImage = updateBlogDTO.Recipe.BannerImage,
                        StepImages = updateBlogDTO.Recipe.StepImages,
                        Ingredients = updateBlogDTO.Recipe.Ingredients
                    };

                    var newRecipe = await _recipeService.CreateRecipe(blog.UserId, createRecipeDTO);
                    blog.RecipeId = newRecipe.Id; // Associate the newly created recipe with the blog
                }
            }
            else if (updateBlogDTO.RecipeId == null && blog.RecipeId.HasValue)
            {
                // If RecipeId is null, remove the existing recipe and its ingredients
                var otherBlogswihtSameReciipe = _unitOfWork.Blogs.FindAsync(b => b.RecipeId == blog.RecipeId);
                if(otherBlogswihtSameReciipe == null)
                {
                    await _recipeService.DeleteRecipe(blog.RecipeId.Value);
                    blog.RecipeId = null;
                }                
            }

            // Update the blog
            _unitOfWork.Blogs.Update(blog);
            await _unitOfWork.CompleteAsync();

            var user = await _unitOfWork.Users.FindAsync(u => u.Id == blog.UserId);

            // Return the updated blog details
            return new BlogDTO
            {
                Id = blog.Id,
                Header = blog.Header,
                BodyText = blog.BodyText,
                UserId = blog.UserId,
                UserName = user.FirstOrDefault()?.UserName ?? "Unknown",
                RecipeId = blog.RecipeId,
                CreatedAt = blog.CreatedAt,
            };
        }

        public async Task<BlogDetailDTO> GetBlogById(int blogId)
        {
            // Fetch the blog by ID
            var blog = await _unitOfWork.Blogs.GetByIdAsync(blogId);
            if (blog == null) return null;

            // Fetch recipe details if associated
            RecipeDetailsDTO recipeDetails = await GetRecipeOfBlog(blogId);

            var user = await _unitOfWork.Users.FindAsync(u => u.Id == blog.UserId);

            // Map and return the blog details along with recipe details
            return new BlogDetailDTO
            {
                Id = blog.Id,
                Header = blog.Header,
                BodyText = blog.BodyText,
                UserId = blog.UserId,
                UserName = user.FirstOrDefault()?.UserName ?? "Unknown",
                RecipeId = blog.RecipeId,
                RecipeHeader = recipeDetails?.Header,
                Recipe = recipeDetails ,// Include the full recipe details
                CreatedAt = blog.CreatedAt,
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

        public async Task<PaginatedResponseDTO<BlogDTO>> SearchBlogs(BlogSearchParams searchParams, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Blogs.GetQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(searchParams.HeaderContains))
                query = query.Where(b => b.Header.Contains(searchParams.HeaderContains));

            if (!string.IsNullOrEmpty(searchParams.BodyContains))
                query = query.Where(b => b.BodyText.Contains(searchParams.BodyContains));

            if (!string.IsNullOrEmpty(searchParams.UserName))
                query = query.Where(b => b.User.UserName == searchParams.UserName);

            if (searchParams.RecipeId.HasValue)
                query = query.Where(b => b.RecipeId == searchParams.RecipeId);

            // For Ingredient and Allergen filtering, we need to include related data:
            if (searchParams.IngredientIds.Any() || searchParams.AllergenIds.Any())
            {
                query = query.Include(b => b.Recipe)
                             .ThenInclude(r => r.Recipes_Ingredients)
                             .ThenInclude(ri => ri.Ingredient)
                             .ThenInclude(i => i.Ingredients_Preferences)
                             .ThenInclude(ip => ip.Preference);

                if (searchParams.IngredientIds.Any())
                {
                    query = query.Where(b => b.Recipe != null &&
                        b.Recipe.Recipes_Ingredients.Any(ri => searchParams.IngredientIds.Contains(ri.IngredientId)));
                }

                if (searchParams.AllergenIds.Any())
                {
                    query = query.Where(b => b.Recipe != null &&
                        (b.Recipe.Recipes_Ingredients.Any(ri =>
                            !ri.Ingredient.Ingredients_Preferences.Any(ip => searchParams.AllergenIds.Contains(ip.PreferenceId)) ||
                            !ri.Ingredient.Ingredients_Preferences.Any()
                        ))
                    );
                }
            }

            // Sorting
            bool ascending = (searchParams.SortOrder?.ToLower() != "desc");
            query = searchParams.SortBy?.ToLower() switch
            {
                "bodytext" => ascending ? query.OrderBy(b => b.BodyText) : query.OrderByDescending(b => b.BodyText),
                "username" => ascending ? query.OrderBy(b => b.User.UserName) : query.OrderByDescending(b => b.User.UserName),
                _ => ascending ? query.OrderBy(b => b.Header) : query.OrderByDescending(b => b.Header),
            };

            var totalCount = await query.CountAsync();
            var blogs = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

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

    }
}
