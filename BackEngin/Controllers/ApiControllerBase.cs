using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using BackEngin.Services;
using Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BackEngin.Services.Interfaces;
using Models.DTO;
using System.Security.Claims;
using System.ComponentModel;

namespace BackEngin.Controllers
{
    public class ApiControllerBase: ControllerBase
    {
        public ApiControllerBase() { }

        //give the accessed object's associated userId as parameter.
        public async Task<bool> CanUserAccess(string userId)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (userIdFromToken == userId) {
                return true;
            }
            return false;
        }
    }
}
