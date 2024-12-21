using BackEngin.Services;
using BackEngin.Services.Interfaces;
using BackEngin.Tests.Utils;
using DataAccess.Repositories;
using FluentAssertions;
using Models;
using Models.DTO;
using Moq;
using MockQueryable.Moq;

namespace BackEngin.Tests.Services
{
    public class BlogServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRecipeService> _mockRecipeService;
        private readonly BlogService _blogService;

        public BlogServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockRecipeService = new Mock<IRecipeService>();
            _blogService = new BlogService(_mockUnitOfWork.Object, _mockRecipeService.Object);
        }

        [Fact]
        public async Task GetBlogs_ShouldReturnPaginatedBlogs()
        {
            // Arrange
            var blogs = new List<Blogs>
            {
                new Blogs { Id = 1, Header = "Blog 1", BodyText = "Content 1", UserId = "1" },
                new Blogs { Id = 2, Header = "Blog 2", BodyText = "Content 2", UserId = "2" }
            };

            var users = new List<Users>
            {
                new Users { Id = "1", UserName = "User1" },
                new Users { Id = "2", UserName = "User2" }
            };

            _mockUnitOfWork.Setup(u => u.Blogs.GetPaginatedAsync(null, 1, 2)).ReturnsAsync((blogs, blogs.Count));
            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>())).ReturnsAsync(users);

            // Act
            var result = await _blogService.GetBlogs(1, 2);

            // Assert
            result.Should().NotBeNull();
            result.Items.Count().Should().Be(2);
            result.Items.First().Header.Should().Be("Blog 1");
            result.Items.First().UserName.Should().Be("User1");
            result.TotalCount.Should().Be(2);
            result.PageNumber.Should().Be(1);
            result.PageSize.Should().Be(2);
        }


        [Fact]
        public async Task CreateBlog_ShouldCreateBlogWithNewRecipe()
        {
            // Arrange
            var userId = "123";
            var createBlogDTO = new CreateBlogDTO
            {
                Header = "New Blog",
                BodyText = "Blog Content",
                Recipe = new CreateRecipeDTO
                {
                    Header = "New Recipe",
                    BodyText = "Recipe Content",
                    Ingredients = new List<RecipeIngredientRequestDTO>
                    {
                        new RecipeIngredientRequestDTO { IngredientId = 1, Quantity = 2, Unit = "cups" }
                    }
                }
            };

            var createdRecipe = new RecipeDetailsDTO
            {
                Id = 1,
                Header = "New Recipe",
                BodyText = "Recipe Content",
                Ingredients = createBlogDTO.Recipe.Ingredients
                    .Select(i => new RecipeIngredientDetailsDTO
                    {
                        IngredientId = i.IngredientId,
                        Quantity = i.Quantity,
                        Unit = i.Unit,
                        IngredientName = "Flour" // Mocked ingredient name
                    }).ToList()
            };

            // Mock RecipeService to return the created recipe
            _mockRecipeService.Setup(r => r.CreateRecipe(userId, It.IsAny<CreateRecipeDTO>())).ReturnsAsync(createdRecipe);

            // Mock Blog creation
            _mockUnitOfWork.Setup(u => u.Blogs.AddAsync(It.IsAny<Blogs>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _blogService.CreateBlog(userId, createBlogDTO);

            // Assert
            result.Should().NotBeNull();
            result.RecipeId.Should().Be(1);
            result.Header.Should().Be("New Blog");
            result.BodyText.Should().Be("Blog Content");

            // Verify Blog and Recipe creation
            _mockUnitOfWork.Verify(u => u.Blogs.AddAsync(It.IsAny<Blogs>()), Times.Once);
            _mockRecipeService.Verify(r => r.CreateRecipe(userId, It.IsAny<CreateRecipeDTO>()), Times.Once);
        }


        [Fact]
        public async Task CreateBlog_ShouldThrowError_WhenInvalidIngredient()
        {
            // Arrange
            var userId = "123";
            var createBlogDTO = new CreateBlogDTO
            {
                Header = "New Blog",
                BodyText = "Blog Content",
                Recipe = new CreateRecipeDTO
                {
                    Header = "New Recipe",
                    BodyText = "Recipe Content",
                    Ingredients = new List<RecipeIngredientRequestDTO>
            {
                new RecipeIngredientRequestDTO { IngredientId = 99, Quantity = 2, Unit = "cups" }
            }
                }
            };

            // Mock the CreateRecipe method in IRecipeService to throw an ArgumentException
            _mockRecipeService.Setup(r => r.CreateRecipe(userId, It.IsAny<CreateRecipeDTO>()))
                              .ThrowsAsync(new ArgumentException("One or more ingredient IDs are invalid."));

            // Act
            Func<Task> act = async () => await _blogService.CreateBlog(userId, createBlogDTO);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("One or more ingredient IDs are invalid.");
        }

        [Fact]
        public async Task CreateBlog_ShouldCreateBlogWithoutRecipe()
        {
            // Arrange
            var userId = "123";
            var createBlogDTO = new CreateBlogDTO
            {
                Header = "New Blog Without Recipe",
                BodyText = "This blog does not have an associated recipe.",
                Recipe = null, // No recipe provided
                RecipeId = null
            };

            _mockUnitOfWork.Setup(u => u.Blogs.AddAsync(It.IsAny<Blogs>())).Callback<Blogs>(b => b.Id = 1);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _blogService.CreateBlog(userId, createBlogDTO);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Header.Should().Be("New Blog Without Recipe");
            result.BodyText.Should().Be("This blog does not have an associated recipe.");
            result.RecipeId.Should().BeNull();

            _mockUnitOfWork.Verify(u => u.Blogs.AddAsync(It.IsAny<Blogs>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.AtLeastOnce);
        }



        [Fact]
        public async Task UpdateBlog_ShouldUpdateBlogAndAssociatedRecipe()
        {
            // Arrange
            var blogId = 1;
            var userId = "123";
            var userName = "TestUser";
            var updateBlogDTO = new UpdateBlogDTO
            {
                Header = "Updated Blog",
                BodyText = "Updated Content",
                Recipe = new RecipeRequestDTO
                {
                    Header = "Updated Recipe",
                    BodyText = "Updated Recipe Content",
                    Ingredients = new List<RecipeIngredientRequestDTO>
            {
                new RecipeIngredientRequestDTO { IngredientId = 1, Quantity = 2, Unit = "cups" }
            }
                }
            };

            var blog = new Blogs
            {
                Id = blogId,
                Header = "Old Blog",
                BodyText = "Old Content",
                RecipeId = 1,
                UserId = userId
            };

            _mockUnitOfWork.Setup(u => u.Blogs.GetByIdAsync(blogId)).ReturnsAsync(blog);
            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                           .ReturnsAsync(new List<Users> { new Users { Id = userId, UserName = userName } });
            _mockRecipeService.Setup(r => r.UpdateRecipe(1, updateBlogDTO.Recipe))
                              .ReturnsAsync(new RecipeDetailsDTO { Id = 1, Header = "Updated Recipe" });

            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _blogService.UpdateBlog(blogId, updateBlogDTO);

            // Assert
            result.Should().NotBeNull();
            result.Header.Should().Be("Updated Blog");
            result.BodyText.Should().Be("Updated Content");
            result.RecipeId.Should().Be(1);
            result.UserId.Should().Be(userId);
            result.UserName.Should().Be(userName);
            _mockRecipeService.Verify(r => r.UpdateRecipe(1, updateBlogDTO.Recipe), Times.Once);
        }


        [Fact]
        public async Task GetBlogById_ShouldReturnBlogWithRecipeDetails()
        {
            // Arrange
            var blogId = 1;
            var userId = "123";
            var userName = "TestUser";
            var blog = new Blogs
            {
                Id = blogId,
                Header = "Blog",
                BodyText = "Content",
                RecipeId = 1,
                UserId = userId
            };

            var recipe = new RecipeDetailsDTO
            {
                Id = 1,
                Header = "Recipe",
                BodyText = "Recipe Content",
                Ingredients = new List<RecipeIngredientDetailsDTO>
                {
                    new RecipeIngredientDetailsDTO { IngredientId = 1, IngredientName = "Flour", Quantity = 2, Unit = "cups" }
                }
            };

            var user = new Users
            {
                Id = userId,
                UserName = userName
            };

            _mockUnitOfWork.Setup(u => u.Blogs.GetByIdAsync(blogId)).ReturnsAsync(blog);
            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                           .ReturnsAsync(new List<Users> { user });
            _mockRecipeService.Setup(r => r.GetRecipeDetails(1)).ReturnsAsync(recipe);

            // Act
            var result = await _blogService.GetBlogById(blogId);

            // Assert
            result.Should().NotBeNull();
            result.Header.Should().Be("Blog");
            result.UserId.Should().Be(userId);
            result.UserName.Should().Be(userName);
            result.Recipe.Should().NotBeNull();
            result.Recipe.Ingredients.First().IngredientName.Should().Be("Flour");
        }

        [Fact]
        public async Task DeleteBlog_ShouldDeleteBlogAndAssociatedRecipe()
        {
            // Arrange
            var blogId = 1;
            var blog = new Blogs { Id = blogId, Header = "Blog", BodyText = "Content", RecipeId = 1 };

            _mockUnitOfWork.Setup(u => u.Blogs.GetByIdAsync(blogId)).ReturnsAsync(blog);
            _mockRecipeService.Setup(r => r.DeleteRecipe(1)).ReturnsAsync(true);

            _mockUnitOfWork.Setup(u => u.Blogs.Delete(blog)).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _blogService.DeleteBlog(blogId);

            // Assert
            result.Should().BeTrue();
            _mockRecipeService.Verify(r => r.DeleteRecipe(1), Times.Once);
            _mockUnitOfWork.Verify(u => u.Blogs.Delete(blog), Times.Once);
        }

        [Fact]
        public async Task DeleteBlog_ShouldReturnFalse_WhenRecipeDeletionFails()
        {
            // Arrange
            var blogId = 1;
            var blog = new Blogs { Id = blogId, Header = "Blog", BodyText = "Content", RecipeId = 1 };

            _mockUnitOfWork.Setup(u => u.Blogs.GetByIdAsync(blogId)).ReturnsAsync(blog);
            _mockRecipeService.Setup(r => r.DeleteRecipe(1)).ReturnsAsync(false);

            // Act
            var result = await _blogService.DeleteBlog(blogId);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SearchBlogs_ShouldReturnAll_WhenNoFilterApplied()
        {
            var users = TestUtilities.CreateUsers(3);
            var ingredients = TestUtilities.CreateIngredients(5, TestUtilities.CreateIngredientTypes(2), addAllergens: true, allAllergens: TestUtilities.CreateAllergens(3));
            var recipes = TestUtilities.CreateRecipes(5, users, ingredients);
            var blogs = TestUtilities.CreateBlogs(10, users, recipes).AsQueryable().BuildMockDbSet();

            _mockUnitOfWork.Setup(u => u.Blogs.GetQueryable()).Returns(blogs.Object);

            var searchParams = new BlogSearchParams();

            var result = await _blogService.SearchBlogs(searchParams, 1, 20);

            result.TotalCount.Should().Be(10);
            result.Items.Should().HaveCount(10);
        }

        [Fact]
        public async Task SearchBlogs_ShouldFilterByHeaderAndBody()
        {
            var users = TestUtilities.CreateUsers(2);
            var ingredients = TestUtilities.CreateIngredients(5, TestUtilities.CreateIngredientTypes(1), true, TestUtilities.CreateAllergens(2));
            var recipes = TestUtilities.CreateRecipes(3, users, ingredients);
            var blogsList = TestUtilities.CreateBlogs(5, users, recipes);

            blogsList[0].Header = "SpecialHeader";
            blogsList[0].BodyText = "TastyBodyText";
            blogsList[1].Header = "SpecialHeader";
            blogsList[1].BodyText = "Not matching body";
            blogsList[2].Header = "Irrelevant";
            blogsList[2].BodyText = "TastyBodyText";

            var blogs = blogsList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Blogs.GetQueryable()).Returns(blogs.Object);

            var searchParams = new BlogSearchParams
            {
                HeaderContains = "Special",
                BodyContains = "Tasty"
            };

            var result = await _blogService.SearchBlogs(searchParams, 1, 10);

            result.TotalCount.Should().Be(1);
            result.Items.Should().HaveCount(1);
            result.Items.First().Header.Should().Be("SpecialHeader");
        }

        [Fact]
        public async Task SearchBlogs_ShouldFilterByUserName()
        {
            var users = TestUtilities.CreateUsers(2);
            users[0].UserName = "matchingUser";
            users[1].UserName = "otherUser";

            var ingredients = TestUtilities.CreateIngredients(5, TestUtilities.CreateIngredientTypes(1), true, TestUtilities.CreateAllergens(2));
            var recipes = TestUtilities.CreateRecipes(3, users, ingredients);
            var blogsList = TestUtilities.CreateBlogs(5, users, recipes);

            blogsList[0].User = users[0];
            blogsList[0].UserId = users[0].Id;
            blogsList[1].User = users[1];
            blogsList[1].UserId = users[1].Id;

            var blogs = blogsList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Blogs.GetQueryable()).Returns(blogs.Object);

            var searchParams = new BlogSearchParams
            {
                UserName = "matchingUser"
            };

            var result = await _blogService.SearchBlogs(searchParams, 1, 10);
            result.TotalCount.Should().Be(blogsList.Count(b => b.User.UserName == "matchingUser"));
            result.Items.Should().AllSatisfy(b => b.UserId.Should().Be(users[0].Id));
        }

        [Fact]
        public async Task SearchBlogs_ShouldFilterByRecipeId()
        {
            var users = TestUtilities.CreateUsers(2);
            var ingredients = TestUtilities.CreateIngredients(5, TestUtilities.CreateIngredientTypes(2), true, TestUtilities.CreateAllergens(3));
            var recipes = TestUtilities.CreateRecipes(5, users, ingredients);
            var blogsList = TestUtilities.CreateBlogs(10, users, recipes);

            blogsList[0].RecipeId = recipes[0].Id;
            blogsList[1].RecipeId = recipes[0].Id;
            blogsList[2].RecipeId = recipes[1].Id;

            var blogs = blogsList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Blogs.GetQueryable()).Returns(blogs.Object);

            var searchParams = new BlogSearchParams
            {
                RecipeId = recipes[0].Id
            };

            var result = await _blogService.SearchBlogs(searchParams, 1, 10);

            result.TotalCount.Should().Be(3);
            result.Items.Should().HaveCount(3);
        }

        [Fact]
        public async Task SearchBlogs_ShouldFilterByIngredientAndAllergen()
        {
            // Complex scenario: Only those blogs whose associated recipe ingredients do not contain given allergen and do contain given ingredients.
            var users = TestUtilities.CreateUsers(2);
            var ingredientTypes = TestUtilities.CreateIngredientTypes(2);
            var allAllergens = TestUtilities.CreateAllergens(2);
            var ingredients = TestUtilities.CreateIngredients(5, ingredientTypes, true, allAllergens);
            var recipes = TestUtilities.CreateRecipes(4, users, ingredients);

            // Modify ingredients/preferences to ensure filtering
            // Let's say we want IngredientId = 1 and exclude AllergenId = 2.
            // We'll configure one recipe to have IngredientId=1 without allergen=2, and another with allergen=2 to exclude it.
            var recipeWithGoodIngredient = recipes[0];
            recipeWithGoodIngredient.Recipes_Ingredients.Clear();
            recipeWithGoodIngredient.Recipes_Ingredients.Add(new Recipes_Ingredients
            {
                RecipeId = recipeWithGoodIngredient.Id,
                IngredientId = 1,
                Ingredient = ingredients.First(i => i.Id == 1)
            });

            // Add allergen to IngredientId=3 to exclude it.
            var ingWithAllergen = ingredients.First(i => i.Id == 3);
            ingWithAllergen.Ingredients_Preferences.Clear();
            ingWithAllergen.Ingredients_Preferences.Add(new Ingredients_Preferences
            {
                IngredientId = 3,
                PreferenceId = 2, // Allergen to exclude
                Preference = allAllergens.First(a => a.Id == 2)
            });

            var recipeWithBadAllergen = recipes[1];
            recipeWithBadAllergen.Recipes_Ingredients.Clear();
            recipeWithBadAllergen.Recipes_Ingredients.Add(new Recipes_Ingredients
            {
                RecipeId = recipeWithBadAllergen.Id,
                IngredientId = 3, // Contains excluded allergen
                Ingredient = ingWithAllergen
            });

            var blogsList = TestUtilities.CreateBlogs(5, users, recipes);
            // Only blogs linked to recipeWithGoodIngredient should pass the filter.
            blogsList[0].RecipeId = recipeWithGoodIngredient.Id; // Blog to include
            blogsList[0].Recipe = recipeWithGoodIngredient;

            blogsList[1].RecipeId = recipeWithBadAllergen.Id;    // Blog to exclude
            blogsList[1].Recipe = recipeWithBadAllergen;

            // Ensure other recipes do not match the filter
            var recipeWithIrrelevantIngredient = recipes[2];
            recipeWithIrrelevantIngredient.Recipes_Ingredients.Clear();
            recipeWithIrrelevantIngredient.Recipes_Ingredients.Add(new Recipes_Ingredients
            {
                RecipeId = recipeWithIrrelevantIngredient.Id,
                IngredientId = 4, // Ingredient not in IngredientIds filter
                Ingredient = ingredients.First(i => i.Id == 4)
            });

            // Assign this recipe to blogs that should not match
            blogsList[2].RecipeId = recipeWithIrrelevantIngredient.Id;
            blogsList[2].Recipe = recipeWithIrrelevantIngredient;

            var recipeWithExcludedAllergen = recipes[3];
            recipeWithExcludedAllergen.Recipes_Ingredients.Clear();
            recipeWithExcludedAllergen.Recipes_Ingredients.Add(new Recipes_Ingredients
            {
                RecipeId = recipeWithExcludedAllergen.Id,
                IngredientId = 5,
                Ingredient = ingredients.First(i => i.Id == 5)
            });

            // Add excluded allergen to IngredientId=5
            var ingExcludedAllergen = ingredients.First(i => i.Id == 5);
            ingExcludedAllergen.Ingredients_Preferences.Clear();
            ingExcludedAllergen.Ingredients_Preferences.Add(new Ingredients_Preferences
            {
                IngredientId = 5,
                PreferenceId = 2, // Allergen to exclude
                Preference = allAllergens.First(a => a.Id == 2)
            });

            // Assign this recipe to another blog that should not match
            blogsList[3].RecipeId = recipeWithExcludedAllergen.Id;
            blogsList[3].Recipe = recipeWithExcludedAllergen;

            // Ensure the remaining blogs have no recipes
            blogsList[4].RecipeId = null;
            blogsList[4].Recipe = null;

            var blogs = blogsList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Blogs.GetQueryable()).Returns(blogs.Object);

            var searchParams = new BlogSearchParams
            {
                IngredientIds = new List<int> { 1 }, // Include recipes with IngredientId=1
                AllergenIds = new List<int> { 2 }    // Exclude recipes with AllergenId=2
            };

            var result = await _blogService.SearchBlogs(searchParams, 1, 10);

            result.TotalCount.Should().Be(1); // Only one blog should match the criteria
            result.Items.Should().HaveCount(1);
            result.Items.First().RecipeId.Should().Be(recipeWithGoodIngredient.Id);
        }

        [Fact]
        public async Task SearchBlogs_ShouldApplySorting()
        {
            var users = TestUtilities.CreateUsers(2);
            var ingredients = TestUtilities.CreateIngredients(5, TestUtilities.CreateIngredientTypes(1));
            var recipes = TestUtilities.CreateRecipes(2, users, ingredients);
            var blogsList = TestUtilities.CreateBlogs(3, users, recipes);

            blogsList[0].Header = "Z-Blog";
            blogsList[1].Header = "A-Blog";
            blogsList[2].Header = "M-Blog";

            var blogs = blogsList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Blogs.GetQueryable()).Returns(blogs.Object);

            var searchParams = new BlogSearchParams
            {
                SortBy = "Header",
                SortOrder = "asc"
            };

            var result = await _blogService.SearchBlogs(searchParams, 1, 10);

            result.Items.First().Header.Should().Be("A-Blog");
            result.Items.Last().Header.Should().Be("Z-Blog");
        }

        [Fact]
        public async Task SearchBlogs_ShouldPaginateResults()
        {
            var users = TestUtilities.CreateUsers(2);
            var ingredients = TestUtilities.CreateIngredients(5, TestUtilities.CreateIngredientTypes(1));
            var recipes = TestUtilities.CreateRecipes(1, users, ingredients);
            var blogsList = TestUtilities.CreateBlogs(25, users, recipes);

            var blogs = blogsList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Blogs.GetQueryable()).Returns(blogs.Object);

            var searchParams = new BlogSearchParams();
            int pageNumber = 3;
            int pageSize = 5;

            var result = await _blogService.SearchBlogs(searchParams, pageNumber, pageSize);

            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
            result.TotalCount.Should().Be(25);
            result.Items.Should().HaveCount(5);
        }

    }
}
