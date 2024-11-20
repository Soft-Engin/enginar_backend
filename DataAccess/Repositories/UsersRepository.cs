using BackEngin.Data;
using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        private readonly DataContext _db;
        public UsersRepository(DataContext db) : base(db)
        {
            _db = db;

        }     

        public async Task<FollowersDTO> GetFollowersAsync(string userId)
        {
            var followersQuery = _db.Users_Interactions
                .Where(ui => ui.TargetUserId == userId && ui.Interaction.Name == "Follow")
                .Select(ui => ui.InitiatorUser.UserName);

            var usernames = await followersQuery.ToListAsync();
            var totalCount = await followersQuery.CountAsync();

            return new FollowersDTO
            {
                Usernames = usernames,
                TotalCount = totalCount
            };
        }

        public async Task<FollowedUsersDTO> GetFollowingAsync(string userId)
        {
            var followingsQuery = _db.Users_Interactions
                .Where(ui => ui.InitiatorUserId == userId && ui.Interaction.Name == "Follow")
                .Select(ui => ui.TargetUser.UserName);

            var usernames = await followingsQuery.ToListAsync();
            var totalCount = await followingsQuery.CountAsync();

            return new FollowedUsersDTO
            {
                Usernames = usernames,
                TotalCount = totalCount
            };

        }

        public async Task<bool> FollowUserAsync(string initiatorUserId, string targetUserId)
        {
            var existingInteraction = await _db.Users_Interactions
                .FirstOrDefaultAsync(ui => ui.InitiatorUserId == initiatorUserId && ui.TargetUserId == targetUserId && ui.Interaction.Name == "Follow");

            if (existingInteraction != null)
                return false;

            var followInteraction = await _db.Interactions.FirstOrDefaultAsync(i => i.Name == "Follow");

            if (followInteraction == null)
                throw new Exception("Follow interaction not defined in database.");

            var userInteraction = new Users_Interactions
            {
                InitiatorUserId = initiatorUserId,
                TargetUserId = targetUserId,
                InteractionId = followInteraction.Id
            };

            _db.Users_Interactions.Add(userInteraction);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UnfollowUserAsync(string initiatorUserId, string targetUserId)
        {
            var userInteraction = await _db.Users_Interactions
                .FirstOrDefaultAsync(ui => ui.InitiatorUserId == initiatorUserId && ui.TargetUserId == targetUserId && ui.Interaction.Name == "Follow");

            if (userInteraction == null)
                return false;

            _db.Users_Interactions.Remove(userInteraction);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
