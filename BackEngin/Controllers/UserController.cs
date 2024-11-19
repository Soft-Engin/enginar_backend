using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using BackEngin.Services;
using Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BackEngin.Services.Interfaces;
using Models.DTO;

namespace BackEngin.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Get all users
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // Get a user by ID
        [HttpGet("GetUser{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Update an existing user
        [HttpPut("UpdateUser{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = await _userService.UpdateUserAsync(id, userDTO);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        // Delete a user
        [HttpDelete("DeleteUser{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
