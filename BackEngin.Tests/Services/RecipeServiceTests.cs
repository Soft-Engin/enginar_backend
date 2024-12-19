using BackEngin.Services;
using BackEngin.Tests.Utils;
using DataAccess.Repositories;
using FluentAssertions;
using Models;
using Models.DTO;
using Moq;
using MockQueryable.Moq;

namespace BackEngin.Tests.Services
{
    public class RecipeServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly RecipeService _recipeService;

        public RecipeServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _recipeService = new RecipeService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetRecipes_ShouldReturnPaginatedRecipesWithUserDetails()
        {
            // Arrange
            var recipes = new List<Recipes>
            {
                new Recipes { Id = 1, Header = "Pancakes", BodyText = "Delicious pancakes", UserId = "user1" },
                new Recipes { Id = 2, Header = "Omelette", BodyText = "Fluffy omelette", UserId = "user2" }
            };

            var users = new List<Users>
            {
                new Users { Id = "user1", UserName = "User One" },
                new Users { Id = "user2", UserName = "User Two" }
            };

            var pageNumber = 1;
            var pageSize = 2;

            _mockUnitOfWork.Setup(u => u.Recipes.GetPaginatedAsync(null, pageNumber, pageSize))
                           .ReturnsAsync((recipes.Take(pageSize), recipes.Count));
            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                           .ReturnsAsync((Func<Users, bool> predicate) => users.Where(predicate).ToList());

            // Act
            var result = await _recipeService.GetRecipes(pageNumber, pageSize);

            // Assert
            result.Should().NotBeNull();
            result.Items.Count().Should().Be(pageSize); // Check the page size
            result.TotalCount.Should().Be(2); // Verify total count of recipes
            result.Items.First().Header.Should().Be("Pancakes"); // Verify first recipe
            result.Items.First().UserId.Should().Be("user1"); // Verify user ID
            result.Items.First().UserName.Should().Be("User One"); // Verify user name
        }


        [Fact]
        public async Task CreateRecipe_ShouldThrowException_WhenNoIngredientsProvided()
        {
            // Arrange
            var userId = "123"; // Mock userId
            var createRecipeDto = new CreateRecipeDTO
            {
                Header = "Pancakes",
                BodyText = "Delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientRequestDTO>() // Empty ingredients
            };

            // Act
            Func<Task> act = async () => await _recipeService.CreateRecipe(userId, createRecipeDto);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("A recipe must have at least one ingredient.");
        }

        [Fact]
        public async Task CreateRecipe_ShouldThrowException_WhenIngredientIdsAreInvalid()
        {
            // Arrange
            var userId = "123"; // Mock user ID
            var createRecipeDto = new CreateRecipeDTO
            {
                Header = "Pancakes",
                BodyText = "Delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientRequestDTO>
                {
                    new RecipeIngredientRequestDTO { IngredientId = 999, Quantity = 2, Unit = "cups" } // Invalid ID
                }
            };

            // Mock to simulate invalid ingredient IDs
            _mockUnitOfWork.Setup(u => u.Ingredients.FindAsync(It.IsAny<Func<Ingredients, bool>>()))
                .ReturnsAsync(new List<Ingredients>()); // Empty result

            // Act
            Func<Task> act = async () => await _recipeService.CreateRecipe(userId, createRecipeDto);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("One or more ingredient IDs are invalid.");
        }

        [Fact]
        public async Task CreateRecipe_ShouldReturnCreatedRecipe_WhenValidIngredientsProvided()
        {
            // Arrange
            var userId = "testUserId";
            var userName = "Test User";
            var createRecipeDto = new CreateRecipeDTO
            {
                Header = "Pancakes",
                BodyText = "Delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientRequestDTO>
                {
                    new RecipeIngredientRequestDTO { IngredientId = 1, Quantity = 2, Unit = "cups" }
                }
            };

            var createdRecipe = new Recipes
            {
                Id = 1,
                Header = createRecipeDto.Header,
                BodyText = createRecipeDto.BodyText,
                UserId = userId
            };

            var validIngredients = new List<Ingredients>
            {
                new Ingredients { Id = 1, Name = "Flour" }
            };

            var user = new Users { Id = userId, UserName = userName };

            // Mock valid ingredients and user
            _mockUnitOfWork.Setup(u => u.Ingredients.FindAsync(It.IsAny<Func<Ingredients, bool>>()))
                           .ReturnsAsync(validIngredients);
            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                           .ReturnsAsync(new List<Users> { user });
            _mockUnitOfWork.Setup(u => u.Recipes.AddAsync(It.IsAny<Recipes>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);
            _mockUnitOfWork.Setup(u => u.Recipes_Ingredients.AddAsync(It.IsAny<Recipes_Ingredients>())).Verifiable();

            // Act
            var result = await _recipeService.CreateRecipe(userId, createRecipeDto);

            // Assert
            result.Should().NotBeNull();
            result.Header.Should().Be("Pancakes");
            result.BodyText.Should().Be("Delicious pancakes recipe");
            result.UserId.Should().Be(userId);
            result.UserName.Should().Be(userName);
            result.Ingredients.Count.Should().Be(1);
            result.Ingredients.First().IngredientId.Should().Be(1);

            _mockUnitOfWork.Verify(u => u.Recipes.AddAsync(It.IsAny<Recipes>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.AtLeastOnce);
            _mockUnitOfWork.Verify(u => u.Recipes_Ingredients.AddAsync(It.IsAny<Recipes_Ingredients>()), Times.Once);

        }




        [Fact]
        public async Task GetRecipeDetails_ShouldReturnRecipeWithIngredients()
        {
            // Arrange
            var recipeId = 1;
            var recipe = new Recipes
            {
                Id = recipeId,
                Header = "Pancakes",
                BodyText = "Delicious pancakes",
                UserId = "user1"
            };

            var ingredients = new List<Recipes_Ingredients>
            {
                new Recipes_Ingredients { RecipeId = recipeId, IngredientId = 1, Quantity = 2, Unit = "cups" }
            };

            var ingredientDetails = new List<Ingredients>
            {
                new Ingredients { Id = 1, Name = "Flour" }
            };

            var user = new Users { Id = "user1", UserName = "User One" };

            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync(recipe);
            _mockUnitOfWork.Setup(u => u.Recipes_Ingredients.FindAsync(It.IsAny<Func<Recipes_Ingredients, bool>>()))
                           .ReturnsAsync(ingredients);
            _mockUnitOfWork.Setup(u => u.Ingredients.FindAsync(It.IsAny<Func<Ingredients, bool>>()))
                           .ReturnsAsync(ingredientDetails);
            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                           .ReturnsAsync(new List<Users> { user });

            // Act
            var result = await _recipeService.GetRecipeDetails(recipeId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(recipeId);
            result.UserId.Should().Be("user1");
            result.UserName.Should().Be("User One");
            result.Ingredients.Should().HaveCount(1);
            result.Ingredients.First().IngredientName.Should().Be("Flour");
        }

        [Fact]
        public async Task UpdateRecipe_ShouldReturnUpdatedRecipe()
        {
            // Arrange
            var recipeId = 1;
            var userId = "123";
            var userName = "TestUser";

            var updateRecipeDto = new RecipeRequestDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientRequestDTO>
                {
                    new RecipeIngredientRequestDTO { IngredientId = 1, Quantity = 3, Unit = "cups" }
                }
            };

            var existingRecipe = new Recipes
            {
                Id = recipeId,
                Header = "Old Pancakes",
                BodyText = "Old pancakes recipe",
                UserId = userId
            };

            var updatedRecipeIngredients = updateRecipeDto.Ingredients
                .Select(i => new RecipeIngredientDetailsDTO
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    IngredientName = "Flour" // Simulated from ingredient lookup
                }).ToList();

            var user = new Users
            {
                Id = userId,
                UserName = userName
            };

            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync(existingRecipe);
            _mockUnitOfWork.Setup(u => u.Recipes_Ingredients.FindAsync(It.IsAny<Func<Recipes_Ingredients, bool>>()))
                           .ReturnsAsync(new List<Recipes_Ingredients>
                           {
                       new Recipes_Ingredients { RecipeId = recipeId, IngredientId = 1, Quantity = 2, Unit = "cups" }
                           });
            _mockUnitOfWork.Setup(u => u.Ingredients.FindAsync(It.IsAny<Func<Ingredients, bool>>()))
                           .ReturnsAsync(new List<Ingredients>
                           {
                       new Ingredients { Id = 1, Name = "Flour" }
                           });
            _mockUnitOfWork.Setup(u => u.Users.FindAsync(It.IsAny<Func<Users, bool>>()))
                           .ReturnsAsync(new List<Users> { user });
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _recipeService.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(recipeId);
            result.Header.Should().Be(updateRecipeDto.Header);
            result.BodyText.Should().Be(updateRecipeDto.BodyText);
            result.UserId.Should().Be(userId);
            result.UserName.Should().Be(userName);
            result.Ingredients.Should().BeEquivalentTo(updatedRecipeIngredients, options => options.ExcludingMissingMembers());

        }

        [Fact]
        public async Task UpdateRecipe_ShouldThrowArgumentException_WhenNoIngredientsProvided()
        {
            // Arrange
            var recipeId = 1;
            var updateRecipeDto = new RecipeRequestDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe",
                Ingredients = null // No ingredients provided
            };

            var existingRecipe = new Recipes
            {
                Id = recipeId,
                Header = "Old Pancakes",
                BodyText = "Old pancakes recipe",
                UserId = "123"
            };

            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync(existingRecipe);

            // Act
            Func<Task> action = async () => await _recipeService.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            await action.Should().ThrowAsync<ArgumentException>()
                        .WithMessage("A recipe must have at least one ingredient.");
        }

        [Fact]
        public async Task UpdateRecipe_ShouldThrowArgumentException_WhenInvalidIngredientIdsProvided()
        {
            // Arrange
            var recipeId = 1;
            var updateRecipeDto = new RecipeRequestDTO
            {
                Header = "Updated Pancakes",
                BodyText = "Updated delicious pancakes recipe",
                Ingredients = new List<RecipeIngredientRequestDTO>
                {
                    new RecipeIngredientRequestDTO { IngredientId = 99, Quantity = 1, Unit = "tbsp" } // Invalid ingredient ID
                }
            };

            var existingRecipe = new Recipes
            {
                Id = recipeId,
                Header = "Old Pancakes",
                BodyText = "Old pancakes recipe",
                UserId = "123"
            };

            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync(existingRecipe);
            _mockUnitOfWork.Setup(u => u.Ingredients.FindAsync(It.IsAny<Func<Ingredients, bool>>()))
                           .ReturnsAsync(new List<Ingredients>()); // No matching ingredients found

            // Act
            Func<Task> action = async () => await _recipeService.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            await action.Should().ThrowAsync<ArgumentException>()
                        .WithMessage("One or more ingredient IDs are invalid.");
        }



        [Fact]
        public async Task DeleteRecipe_ShouldRemoveRecipeAndIngredients()
        {
            // Arrange
            var recipeId = 1;
            var recipe = new Recipes { Id = recipeId, Header = "Pancakes", BodyText = "Delicious pancakes" };
            var ingredients = new List<Recipes_Ingredients>
            {
                new Recipes_Ingredients { RecipeId = recipeId, IngredientId = 1, Quantity = 2, Unit = "cups" }
            };

            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync(recipe);
            _mockUnitOfWork.Setup(u => u.Recipes_Ingredients.FindAsync(It.IsAny<Func<Recipes_Ingredients, bool>>())).ReturnsAsync(ingredients);

            // Act
            var result = await _recipeService.DeleteRecipe(recipeId);

            // Assert
            result.Should().BeTrue();
            _mockUnitOfWork.Verify(u => u.Recipes.Delete(recipe), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task GetRecipeDetails_ShouldReturnNull_WhenRecipeDoesNotExist()
        {
            // Arrange
            var recipeId = 99;
            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync((Recipes)null);

            // Act
            var result = await _recipeService.GetRecipeDetails(recipeId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateRecipe_ShouldReturnNull_WhenRecipeDoesNotExist()
        {
            // Arrange
            var recipeId = 99;
            var updateRecipeDto = new RecipeRequestDTO { /* ... */ };
            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync((Recipes)null);

            // Act
            var result = await _recipeService.UpdateRecipe(recipeId, updateRecipeDto);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteRecipe_ShouldReturnFalse_WhenRecipeDoesNotExist()
        {
            // Arrange
            var recipeId = 99;
            _mockUnitOfWork.Setup(u => u.Recipes.GetByIdAsync(recipeId)).ReturnsAsync((Recipes)null);

            // Act
            var result = await _recipeService.DeleteRecipe(recipeId);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task SearchRecipes_ShouldReturnAll_WhenNoFilter()
        {
            var users = TestUtilities.CreateUsers(2);
            var ingredients = TestUtilities.CreateIngredients(5, TestUtilities.CreateIngredientTypes(1), true, TestUtilities.CreateAllergens(2));
            var recipes = TestUtilities.CreateRecipes(10, users, ingredients).AsQueryable().BuildMockDbSet();

            _mockUnitOfWork.Setup(u => u.Recipes.GetQueryable()).Returns(recipes.Object);

            var searchParams = new RecipeSearchParams();

            var result = await _recipeService.SearchRecipes(searchParams, 1, 20);

            result.TotalCount.Should().Be(10);
            result.Items.Should().HaveCount(10);
        }

        [Fact]
        public async Task SearchRecipes_ShouldFilterByHeaderAndBody()
        {
            var users = TestUtilities.CreateUsers(1);
            var ingredients = TestUtilities.CreateIngredients(5, TestUtilities.CreateIngredientTypes(1));
            var recipeList = TestUtilities.CreateRecipes(5, users, ingredients);

            recipeList[0].Header = "SpecialHeader";
            recipeList[0].BodyText = "SweetBodyText";
            recipeList[1].Header = "SpecialHeader";
            recipeList[1].BodyText = "Not sweet";
            recipeList[2].Header = "Irrelevant";
            recipeList[2].BodyText = "SweetBodyText";

            var recipes = recipeList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Recipes.GetQueryable()).Returns(recipes.Object);

            var searchParams = new RecipeSearchParams
            {
                HeaderContains = "Special",
                BodyContains = "Sweet"
            };

            var result = await _recipeService.SearchRecipes(searchParams, 1, 10);

            result.TotalCount.Should().Be(1);
            result.Items.First().Header.Should().Be("SpecialHeader");
        }

        [Fact]
        public async Task SearchRecipes_ShouldFilterByUserName()
        {
            var users = TestUtilities.CreateUsers(2);
            users[0].UserName = "recipeUser";
            var ingredients = TestUtilities.CreateIngredients(3, TestUtilities.CreateIngredientTypes(1));
            var recipeList = TestUtilities.CreateRecipes(5, users, ingredients);
            recipeList[0].UserId = users[0].Id;
            recipeList[1].UserId = users[1].Id;

            var recipes = recipeList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Recipes.GetQueryable()).Returns(recipes.Object);

            var searchParams = new RecipeSearchParams
            {
                UserName = "recipeUser"
            };

            var result = await _recipeService.SearchRecipes(searchParams, 1, 10);
            result.Items.Should().AllSatisfy(r => r.Header.StartsWith("Recipe"));
            result.Items.Count().Should().Be(recipeList.Count(r => r.UserId == users[0].Id));
        }

        [Fact]
        public async Task SearchRecipes_ShouldFilterByIngredientsAndAllergens()
        {
            var users = TestUtilities.CreateUsers(1);
            var allAllergens = TestUtilities.CreateAllergens(2);
            var ingredientTypes = TestUtilities.CreateIngredientTypes(1);
            var ingredients = TestUtilities.CreateIngredients(5, ingredientTypes, true, allAllergens);
            var recipeList = TestUtilities.CreateRecipes(5, users, ingredients);

            // Setup one recipe to have IngredientId=1 without allergen=2
            recipeList[0].Recipes_Ingredients.Clear();
            recipeList[0].Recipes_Ingredients.Add(new Recipes_Ingredients
            {
                RecipeId = recipeList[0].Id,
                IngredientId = 1,
                Ingredient = ingredients.First(i => i.Id == 1)
            });

            // Another recipe to have allergen=2, which should be excluded
            var ingWithAllergen = ingredients.First(i => i.Id == 3);
            ingWithAllergen.Ingredients_Preferences.Clear();
            ingWithAllergen.Ingredients_Preferences.Add(new Ingredients_Preferences
            {
                IngredientId = 3,
                PreferenceId = 2,
                Preference = allAllergens[1]
            });

            recipeList[1].Recipes_Ingredients.Clear();
            recipeList[1].Recipes_Ingredients.Add(new Recipes_Ingredients
            {
                RecipeId = recipeList[1].Id,
                IngredientId = 3,
                Ingredient = ingWithAllergen
            });

            var recipes = recipeList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Recipes.GetQueryable()).Returns(recipes.Object);

            var searchParams = new RecipeSearchParams
            {
                IngredientIds = new List<int> { 1 },
                AllergenIds = new List<int> { 2 }
            };

            var result = await _recipeService.SearchRecipes(searchParams, 1, 10);
            result.TotalCount.Should().Be(4);
            result.Items.First().Id.Should().Be(recipeList[0].Id);
        }

        [Fact]
        public async Task SearchRecipes_ShouldApplySorting()
        {
            var users = TestUtilities.CreateUsers(1);
            var ingredients = TestUtilities.CreateIngredients(5, TestUtilities.CreateIngredientTypes(1));
            var recipeList = TestUtilities.CreateRecipes(3, users, ingredients);

            recipeList[0].Header = "Z-Recipe";
            recipeList[1].Header = "A-Recipe";
            recipeList[2].Header = "M-Recipe";

            var recipes = recipeList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Recipes.GetQueryable()).Returns(recipes.Object);

            var searchParams = new RecipeSearchParams
            {
                SortBy = "Header",
                SortOrder = "asc"
            };

            var result = await _recipeService.SearchRecipes(searchParams, 1, 10);
            result.Items.First().Header.Should().Be("A-Recipe");
        }

        [Fact]
        public async Task SearchRecipes_ShouldPaginate()
        {
            var users = TestUtilities.CreateUsers(1);
            var ingredients = TestUtilities.CreateIngredients(2, TestUtilities.CreateIngredientTypes(1));
            var recipeList = TestUtilities.CreateRecipes(25, users, ingredients);
            var recipes = recipeList.AsQueryable().BuildMockDbSet();
            _mockUnitOfWork.Setup(u => u.Recipes.GetQueryable()).Returns(recipes.Object);

            var searchParams = new RecipeSearchParams();
            var pageNumber = 3;
            var pageSize = 5;

            var result = await _recipeService.SearchRecipes(searchParams, pageNumber, pageSize);

            result.PageNumber.Should().Be(pageNumber);
            result.PageSize.Should().Be(pageSize);
            result.TotalCount.Should().Be(25);
            result.Items.Should().HaveCount(5);
        }

    }
}
