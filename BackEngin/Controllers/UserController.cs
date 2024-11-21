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

        [HttpGet("{userId}/followers")]
        public async Task<IActionResult> GetFollowers(string userId)
        {
            var followers = await _userService.GetFollowersAsync(userId);
            return Ok(followers);
        }

        [HttpGet("{userId}/following")]
        public async Task<IActionResult> GetFollowing(string userId)
        {
            var following = await _userService.GetFollowingAsync(userId);
            return Ok(following);
        }

        [HttpPost("{userId}/follow")]
        public async Task<IActionResult> FollowUser(string userId, [FromBody] string targetUserId)
        {
            var result = await _userService.FollowUserAsync(userId, targetUserId);
            if (!result)
                return BadRequest("Unable to follow user. Perhaps you already follow them.");

            return Ok(new { Message = "Successfully followed user." });
        }

        [HttpDelete("{userId}/unfollow")]
        public async Task<IActionResult> UnfollowUser(string userId, [FromBody] string targetUserId)
        {
            var result = await _userService.UnfollowUserAsync(userId, targetUserId);
            if (!result)
                return BadRequest("Unable to unfollow user. Perhaps you don't follow them.");

            return Ok(new { Message = "Successfully unfollowed user." });
        }

        [HttpGet("{userId}/bookmarks/recipes")]
        public async Task<IActionResult> GetBookmarkedRecipes(string userId)
        {
            var bookmarks = await _userService.GetBookmarkedRecipesAsync(userId);

            if (bookmarks == null)
                return NotFound("No bookmarked recipes found for this user.");

            return Ok(bookmarks);
        }

        [HttpGet("{userId}/bookmarks/blogs")]
        public async Task<IActionResult> GetBookmarkedBlogs(string userId)
        {
            var bookmarkedBlogs = await _userService.GetBookmarkedBlogsAsync(userId);

            if (bookmarkedBlogs == null)
                return NotFound("No bookmarked blogs found for this user.");

            return Ok(bookmarkedBlogs);
        }
    }
}
