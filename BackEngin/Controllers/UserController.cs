using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using BackEngin.Services;
using Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BackEngin.Services.Interfaces;
using Models.DTO;
using DataAccess.Migrations;
using Humanizer;

namespace BackEngin.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/user")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Get all users
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and pageSize must be positive integers.");
            }

            var users = await _userService.GetAllUsersAsync(page, pageSize);
            return Ok(users);
        }

        // Get a user by ID
        [HttpGet("GetUser/{id}")]
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
        [HttpPut("UpdateUser/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await CanUserAccess(id)) 
            { 
                return Unauthorized();
            }

            var updatedUser = await _userService.UpdateUserAsync(id, userDTO);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        // Delete a user
        [HttpDelete("DeleteUser/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!await CanUserAccess(id))
            {
                return Unauthorized();
            }

            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("followers")]
        public async Task<IActionResult> GetFollowers(string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and pageSize must be positive integers.");
            }

            var followers = await _userService.GetFollowersAsync(userId, page, pageSize);
            return Ok(followers);
        }

        [HttpGet("following")]
        public async Task<IActionResult> GetFollowing(string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and pageSize must be positive integers.");
            }

            var following = await _userService.GetFollowingAsync(userId, page, pageSize);
            return Ok(following);
        }


        [HttpPost("follow")]
        [Authorize]
        public async Task<IActionResult> FollowUser([FromQuery] string targetUserId)
        {
            var userId = await GetActiveUserId();
            if (!await CanUserAccess(userId))
            {
                return Unauthorized();
            }

            var result = await _userService.FollowUserAsync(userId, targetUserId);
            if (!result)
                return BadRequest(new { Message = "Unable to follow user. Perhaps you already follow them." });

            return Ok(new { Message = "Successfully followed user." });
        }

        [HttpDelete("unfollow")]
        [Authorize]
        public async Task<IActionResult> UnfollowUser([FromQuery] string targetUserId)
        {
            var userId = await GetActiveUserId();
            if (!await CanUserAccess(userId))
            {
                return Unauthorized();
            }

            var result = await _userService.UnfollowUserAsync(userId, targetUserId);
            if (!result)
                return BadRequest(new { Message = "Unable to unfollow user.Perhaps you don't follow them." });

            return Ok(new { Message = "Successfully unfollowed user." });
        }

        [HttpGet("{userId}/bookmarks/recipes")]
        [Authorize]
        public async Task<IActionResult> GetBookmarkedRecipes(string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (!await CanUserAccess(userId))
            {
                return Unauthorized();
            }

            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and pageSize must be positive integers.");
            }

            var bookmarks = await _userService.GetBookmarkedRecipesAsync(userId, page, pageSize);

            if (bookmarks == null || !bookmarks.Recipes.Any())
            {
                return NotFound("No bookmarked recipes found for this user.");
            }

            return Ok(bookmarks);
        }


        [HttpGet("{userId}/bookmarks/blogs")]
        [Authorize]
        public async Task<IActionResult> GetBookmarkedBlogs(string userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (!await CanUserAccess(userId))
            {
                return Unauthorized();
            }

            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and pageSize must be positive integers.");
            }

            var bookmarkedBlogs = await _userService.GetBookmarkedBlogsAsync(userId, page, pageSize);

            if (bookmarkedBlogs == null || !bookmarkedBlogs.Blogs.Any())
            {
                return NotFound("No bookmarked blogs found for this user.");
            }

            return Ok(bookmarkedBlogs);
        }

    }
}
