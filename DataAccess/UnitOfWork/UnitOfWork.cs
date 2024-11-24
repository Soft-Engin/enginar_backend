using BackEngin.Models;
using BackEngin.Models;
using Microsoft.EntityFrameworkCore;
using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using DataAccess.Repositories;
using Models;

namespace DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _db;
        public IRepository<WeatherForecast> WeatherForecastRepository { get; }

        // enginar
        public IAddressesRepository Addresses { get; }
        public IBlogsRepository Blogs { get; }
        public ICitiesRepository Cities { get; }
        public ICountriesRepository Countries { get; }
        public IDistrictsRepository Districts { get; }
        public IEventsRepository Events { get; }
        public IEvents_RequirementsRepository Events_Requirements { get; }
        public IIngredientsRepository Ingredients { get; }
        public IIngredientTypesRepository IngredientTypes { get; }
        public IIngredients_PreferencesRepository Ingredients_Preferences { get; }
        public IInteractionsRepository Interactions { get; }
        public IPreferencesRepository Preferences { get; }
        public IRecipesRepository Recipes { get; }
        public IRecipes_IngredientsRepository Recipes_Ingredients { get; }
        public IRequirementsRepository Requirements { get; }
        public IRolesRepository Roles { get; }
        public IUsers_Blogs_InteractionsRepository Users_Blogs_Interactions { get; }
        public IUsers_InteractionsRepository Users_Interactions { get; }
        public IUsers_Recipes_InteractionsRepository Users_Recipes_Interactions { get; }
        public IUsersRepository Users { get; }

        public UnitOfWork(DataContext db)
        {
            _db = db;
            WeatherForecastRepository = new Repository<WeatherForecast>(_db);

            // enginar
            Addresses = new AdressesRepository(_db);
            Blogs = new BlogsRepository(_db);
            Cities = new CitiesRepository(_db);
            Countries = new CountriesRepository(_db);
            Districts = new DistrictsRepository(_db);
            Events = new EventsRepository(_db);
            Events_Requirements = new Events_RequriementsRepository(_db);
            Ingredients = new IngredientsRepository(_db);
            IngredientTypes = new IngredientTypesRepository(_db);
            Ingredients_Preferences = new Ingredients_PreferencesRepository(_db);
            Interactions = new InteractionsRepository(_db);
            Preferences = new PreferencesRepository(_db);
            Recipes_Ingredients = new Recipes_IngredientsRepository(_db);
            Recipes = new RecipesRepository(_db);
            Requirements = new RequirementsRepository(_db);
            Roles = new RolesRepository(_db);
            Users_Blogs_Interactions = new Users_Blogs_InteractionsRepository(_db);
            Users_Interactions = new Users_InteractionsRepository(_db);
            Users_Recipes_Interactions = new Users_Recipes_InteractionsRepository(_db);
            Users = new UsersRepository(_db);


        }

        public async Task<int> CompleteAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
