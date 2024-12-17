using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEngin.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        public ApiControllerBase() { }

        protected async Task<bool> CanUserAccess(string userId)
        {
            try
            {
                var userIdFromToken = await GetActiveUserId();
                var userRole = await GetActiveUserRole();

                if (userIdFromToken == userId || userRole == "Admin")
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking user access: {ex.Message}");
            }
        }

        protected async Task<string> GetActiveUserId()
        {
            try
            {
                var id = User.FindAll(ClaimTypes.NameIdentifier).LastOrDefault()?.Value;
                if (id == null)
                {
                    throw new Exception("JWT token is missing NameIdentifier claim.");
                }
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving active user ID: {ex.Message}");
            }
        }

        protected async Task<string> GetActiveUserRole()
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (role == null)
                {
                    throw new Exception("JWT token is missing Role claim.");
                }
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving active user role: {ex.Message}");
            }
        }
    }
}
