﻿using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
    public interface IUnitOfWork
    {
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
        IUsers_InteractionsRepository Users_Interactions { get; }
        IUsersRepository Users { get; }
        IBlog_LikesRepository Blog_Likes { get; }
        IBlog_CommentsRepository Blog_Comments { get; }
        IBlog_BookmarksRepository Blog_Bookmarks { get; }
        IEvent_LikesRepository Event_Likes { get; }
        IEvent_CommentsRepository Event_Comments { get; }
        IEvent_BookmarksRepository Event_Bookmarks { get; }
        IRecipe_LikesRepository Recipe_Likes { get; }
        IRecipe_CommentsRepository Recipe_Comments { get; }
        IRecipe_BookmarksRepository Recipe_Bookmarks { get; }
        IUser_Event_ParticipationsRepository User_Event_Participations { get; }
        IUser_AllergensRepository User_Allergens { get; }

        Task<int> CompleteAsync();
    }
}