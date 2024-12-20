using BackEngin.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        // default one
        IRepository<WeatherForecast> WeatherForecastRepository { get; }

        // enginar
        IAddressesRepository Addresses { get; }
        IBlogsRepository Blogs { get; }
        ICitiesRepository Cities { get; }
        ICountriesRepository Countries { get; }
        IDistrictsRepository Districts { get; }
        IEventsRepository Events { get; }
        IEvents_RequirementsRepository Events_Requirements { get; }
        IIngredientsRepository Ingredients { get; }
        IIngredientTypesRepository IngredientTypes { get; }
        IIngredients_PreferencesRepository Ingredients_Preferences { get; }
        IInteractionsRepository Interactions { get; }
        IPreferencesRepository Preferences { get; }
        IRecipesRepository Recipes { get; }
        IRecipes_IngredientsRepository Recipes_Ingredients { get; }
        IRequirementsRepository Requirements { get; }
        IRolesRepository Roles { get; }
        IUsers_Blogs_InteractionsRepository Users_Blogs_Interactions { get; }
        IUsers_InteractionsRepository Users_Interactions { get; }
        IUsers_Recipes_InteractionsRepository Users_Recipes_Interactions { get; }
        IUsersRepository Users { get; }
        IBlog_LikesRepository Blog_Likes { get; }
        IBlog_CommentsRepository Blog_Comments { get; }
        IBlog_BookmarksRepository Blog_Bookmarks { get; }
        IRecipe_LikesRepository Recipe_Likes { get; }
        IRecipe_CommentsRepository Recipe_Comments { get; }
        IRecipe_BookmarksRepository Recipe_Bookmarks { get; }
        IUser_Event_ParticipationsRepository User_Event_Participations { get; }

        Task<int> CompleteAsync();
    }
}