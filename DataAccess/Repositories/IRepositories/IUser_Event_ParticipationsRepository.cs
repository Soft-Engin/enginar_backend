using Models.DTO;
using Models;

namespace DataAccess.Repositories.IRepositories
{
    public interface IUser_Event_ParticipationsRepository : IRepository<User_Event_Participations>
    {
        Task<IQueryable<User_Event_Participations>> FindAllAsync(Func<User_Event_Participations, bool> predicate);
    }
}