using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEngin.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        public ApiControllerBase() { }

        //give the accessed object's associated userId as parameter.
        protected async Task<bool> CanUserAccess(string userId)
        {
            var userIdFromToken = await GetActiveUserId();
            var userRole = await GetActiveUserRole();

            if (userIdFromToken == userId || userRole == "Admin")
            {
                return true;
            }

            return false;
        }

        protected async Task<string> GetActiveUserId()
        {
            var id = User.FindAll(ClaimTypes.NameIdentifier).Last()?.Value;
            if (id == null)
            {
                throw new Exception("JWT token misconfigured!");
            }
            return id;
        }

        protected async Task<string> GetActiveUserRole()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == null)
            {
                throw new Exception("JWT token misconfigured!");
            }
            return role;
        }
    }
}
