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
            // Arrange
            var users = TestUtilities.CreateUsers(3);
            var ingredients = TestUtilities.CreateIngredients(5, TestUtilities.CreateIngredientTypes(2), addAllergens: true, allAllergens: TestUtilities.CreateAllergens(3));
            var recipes = TestUtilities.CreateRecipes(5, users, ingredients);
            var blogsList = TestUtilities.CreateBlogs(10, users, recipes);

            var blogsQueryable = blogsList.AsQueryable();
            var mockDbSet = blogsQueryable.BuildMockDbSet();

            _mockUnitOfWork.Setup(u => u.Blogs.GetQueryable())
                .Returns(mockDbSet.Object);

            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                .ReturnsAsync(users);

            var searchParams = new BlogSearchParams();

            // Act
            var result = await _blogService.SearchBlogs(searchParams, 1, 20);

            // Assert
            result.TotalCount.Should().Be(10);
            result.Items.Should().HaveCount(10);
            result.Items.All(b => !string.IsNullOrEmpty(b.UserName)).Should().BeTrue();
        }

        [Fact]
        public async Task SearchBlogs_ShouldFilterByUserName()
        {
            // Arrange
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

            var blogsQueryable = blogsList.AsQueryable();
            var mockDbSet = blogsQueryable.BuildMockDbSet();

            _mockUnitOfWork.Setup(u => u.Blogs.GetQueryable())
                .Returns(mockDbSet.Object);

            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                .ReturnsAsync(users);

            var searchParams = new BlogSearchParams
            {
                UserName = "matchingUser"
            };

            // Act
            var result = await _blogService.SearchBlogs(searchParams, 1, 10);

            // Assert
            result.Items.Should().OnlyContain(b => b.UserName == "matchingUser");
            result.TotalCount.Should().Be(blogsList.Count(b => b.User.UserName == "matchingUser"));
        }

        [Fact]
        public async Task SearchBlogs_ShouldApplySorting()
        {
            // Arrange
            var users = TestUtilities.CreateUsers(2);
            var ingredients = TestUtilities.CreateIngredients(5, TestUtilities.CreateIngredientTypes(1));
            var recipes = TestUtilities.CreateRecipes(2, users, ingredients);
            var blogsList = TestUtilities.CreateBlogs(3, users, recipes);

            blogsList[0].Header = "Z-Blog";
            blogsList[0].User = users[0];
            blogsList[1].Header = "A-Blog";
            blogsList[1].User = users[0];
            blogsList[2].Header = "M-Blog";
            blogsList[2].User = users[1];

            var blogsQueryable = blogsList.AsQueryable();
            var mockDbSet = blogsQueryable.BuildMockDbSet();

            _mockUnitOfWork.Setup(u => u.Blogs.GetQueryable())
                .Returns(mockDbSet.Object);

            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                .ReturnsAsync(users);

            var searchParams = new BlogSearchParams
            {
                SortBy = "Header",
                SortOrder = "asc"
            };

            // Act
            var result = await _blogService.SearchBlogs(searchParams, 1, 10);

            // Assert
            result.Items.First().Header.Should().Be("A-Blog");
            result.Items.Last().Header.Should().Be("Z-Blog");
            result.Items.All(b => !string.IsNullOrEmpty(b.UserName)).Should().BeTrue();
        }

        [Fact]
        public async Task SearchBlogs_ShouldPaginateResults()
        {
            // Arrange
            var users = TestUtilities.CreateUsers(2);
            var blogsList = TestUtilities.CreateBlogs(25, users, null);
            var blogsQueryable = blogsList.AsQueryable();
            var mockDbSet = blogsQueryable.BuildMockDbSet();

            _mockUnitOfWork.Setup(u => u.Blogs.GetQueryable())
                .Returns(mockDbSet.Object);

            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                .ReturnsAsync(users);

            var searchParams = new BlogSearchParams();

            // Act
            var page1 = await _blogService.SearchBlogs(searchParams, 1, 10);
            var page2 = await _blogService.SearchBlogs(searchParams, 2, 10);
            var page3 = await _blogService.SearchBlogs(searchParams, 3, 10);

            // Assert
            page1.Items.Should().HaveCount(10);
            page2.Items.Should().HaveCount(10);
            page3.Items.Should().HaveCount(5);
            page1.TotalCount.Should().Be(25);
            page1.PageNumber.Should().Be(1);
            page2.PageNumber.Should().Be(2);
        }


        [Fact]
        public async Task GetBlogBannerImage_ShouldReturnImage_WhenExists()
        {
            // Arrange
            var blogId = 1;
            var expectedImage = new byte[] { 1, 2, 3, 4, 5 };
            var blog = new Blogs { Id = blogId, BannerImage = expectedImage };

            _mockUnitOfWork.Setup(u => u.Blogs.GetByIdAsync(blogId))
                .ReturnsAsync(blog);

            // Act
            var result = await _blogService.GetBlogBannerImage(blogId);

            // Assert
            result.Should().BeEquivalentTo(expectedImage);
            _mockUnitOfWork.Verify(u => u.Blogs.GetByIdAsync(blogId), Times.Once);
        }

        [Fact]
        public async Task GetBlogBannerImage_ShouldReturnNull_WhenBlogDoesNotExist()
        {
            // Arrange
            var blogId = 99;
            _mockUnitOfWork.Setup(u => u.Blogs.GetByIdAsync(blogId))
                .ReturnsAsync((Blogs)null);

            // Act
            var result = await _blogService.GetBlogBannerImage(blogId);

            // Assert
            result.Should().BeNull();
            _mockUnitOfWork.Verify(u => u.Blogs.GetByIdAsync(blogId), Times.Once);
        }

        [Fact]
        public async Task GetBlogBannerImage_ShouldReturnNull_WhenBlogHasNoImage()
        {
            // Arrange
            var blogId = 1;
            var blog = new Blogs { Id = blogId, BannerImage = null };

            _mockUnitOfWork.Setup(u => u.Blogs.GetByIdAsync(blogId))
                .ReturnsAsync(blog);

            // Act
            var result = await _blogService.GetBlogBannerImage(blogId);

            // Assert
            result.Should().BeNull();
            _mockUnitOfWork.Verify(u => u.Blogs.GetByIdAsync(blogId), Times.Once);
        }
    }
}
