using Models;

namespace DataAccess.Repositories.IRepositories
{
    public interface IUsersRepository : IRepository<Users>
    {
        /*
         * the update (or other methods) method can be overwritten later according to table's needs
        void Update(Users obj);

        */
    }
}