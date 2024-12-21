using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using BackEngin.Services;
using Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BackEngin.Services.Interfaces;
using Models.DTO;
using Humanizer;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

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
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequest( new { message = "Page and pageSize must be positive integers." });
                }

                var users = await _userService.GetAllUsersAsync(page, pageSize);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // Get a user by ID
        [HttpGet("{UserName}")]
        public async Task<IActionResult> GetUserByUserName(string UserName)
        {   
            try
            {
                var id = await _userService.GetUserIdByUsernameAsync(UserName);
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }

        }

        // Get self user
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            try 
            {
                var id = await GetActiveUserId();
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }


        // Update an existing user
        [HttpPut("{UserName}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string UserName, [FromBody] UpdateUserDto userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _userService.GetUserIdByUsernameAsync(UserName);

            if (!await CanUserAccess(id))
            {
                return Unauthorized();
            }

            try 
            {
                var updatedUser = await _userService.UpdateUserAsync(id, userDTO);
                if (updatedUser == null)
                {
                    return NotFound(new { message = "Update failed! Credentials are either not valid or already in use by another user." });
                }

                return Ok(updatedUser);
            }
            

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // Delete a user
        [HttpDelete("{UserName}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string UserName)
        {
            var id = await _userService.GetUserIdByUsernameAsync(UserName);

            if (!await CanUserAccess(id))
            {
                return Unauthorized();
            }

            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "User not found." });
                }

                return Ok(new { message = "User successfully deleted." });
            }

            

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{UserName}/followers")]
        public async Task<IActionResult> GetFollowers(string UserName, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and pageSize must be positive integers." });
            }

            try
            {
                var userId = await _userService.GetUserIdByUsernameAsync(UserName);
                var followers = await _userService.GetFollowersAsync(userId, page, pageSize);
                return Ok(followers);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{UserName}/following")]
        public async Task<IActionResult> GetFollowing(string UserName, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and pageSize must be positive integers." }); ;
            }

            try
            {
                var userId = await _userService.GetUserIdByUsernameAsync(UserName);
                var following = await _userService.GetFollowingAsync(userId, page, pageSize);
                return Ok(following);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }


        [HttpPost("follow")]
        [Authorize]
        public async Task<IActionResult> FollowUser([FromQuery] string targetUserName)
        {
            try
            {
                var userId = await GetActiveUserId();
                if (!await CanUserAccess(userId))
                {
                    return Unauthorized();
                }
                var targetUserId = await _userService.GetUserIdByUsernameAsync(targetUserName);

                var result = await _userService.FollowUserAsync(userId, targetUserId);

                if (!result)
                {
                    return BadRequest(new { message = "Unable to follow user. Perhaps you already follow them." });
                }

                return Ok(new { message = "Successfully followed user." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }


        [HttpDelete("unfollow")]
        [Authorize]
        public async Task<IActionResult> UnfollowUser([FromQuery] string targetUserName)
        {
            var userId = await GetActiveUserId();
            if (!await CanUserAccess(userId))
            {
                return Unauthorized();
            }

            try
            {
                var targetUserId = await _userService.GetUserIdByUsernameAsync(targetUserName);
                var result = await _userService.UnfollowUserAsync(userId, targetUserId);
                if (!result)
                    return BadRequest(new { message = "Unable to unfollow user.Perhaps you don't follow them." });

                return Ok(new { message = "Successfully unfollowed user." });
            }
            

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{UserName}/likes/recipes")]
        [Authorize]
        public async Task<IActionResult> GetLikedRecipes(string UserName, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userId = await _userService.GetUserIdByUsernameAsync(UserName);

            if (!await CanUserAccess(userId))
            {
                return Unauthorized();
            }

            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and pageSize must be positive integers." });
            }

            try
            {
                var likedRecipes = await _userService.GetLikedRecipesAsync(userId, page, pageSize);

                if (likedRecipes == null || !likedRecipes.Items.Any())
                {
                    return NotFound(new { message = "No liked blogs found for the given user." });
                }

                return Ok(likedRecipes);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }


        [HttpGet("{UserName}/bookmarks/recipes")]
        [Authorize]
        public async Task<IActionResult> GetBookmarkedRecipes(string UserName, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userId = await _userService.GetUserIdByUsernameAsync(UserName);

            if (!await CanUserAccess(userId))
            {
                return Unauthorized();
            }

            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and pageSize must be positive integers." });
            }

            try
            {
                var bookmarks = await _userService.GetBookmarkedRecipesAsync(userId, page, pageSize);

                if (bookmarks == null || !bookmarks.Items.Any())
                {
                    return NotFound(new { message = "No bookmarked blogs found for the given user." });
                }

                return Ok(bookmarks);
            }
            

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{UserName}/likes/blogs")]
        [Authorize]
        public async Task<IActionResult> GetLikedBlogs(string UserName, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userId = await _userService.GetUserIdByUsernameAsync(UserName);

            if (!await CanUserAccess(userId))
            {
                return Unauthorized();
            }

            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and pageSize must be positive integers." });
            }

            try
            {
                var likedBlogs = await _userService.GetLikedBlogsAsync(userId, page, pageSize);

                if (likedBlogs == null || !likedBlogs.Items.Any())
                {
                    return NotFound(new { message = "No liked blogs found for the given user." });
                }

                return Ok(likedBlogs);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }



        [HttpGet("{UserName}/bookmarks/blogs")]
        [Authorize]
        public async Task<IActionResult> GetBookmarkedBlogs(string UserName, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userId = await _userService.GetUserIdByUsernameAsync(UserName);

            if (!await CanUserAccess(userId))
            {
                return Unauthorized();
            }

            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and pageSize must be positive integers." });
            }

            try
            {
                var bookmarkedBlogs = await _userService.GetBookmarkedBlogsAsync(userId, page, pageSize);

                if (bookmarkedBlogs == null || !bookmarkedBlogs.Items.Any())
                {
                    return NotFound(new { message = "No bookmarked blogs found for the given user." }); 
                }

                return Ok(bookmarkedBlogs);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }

            
        }

        [HttpGet("{UserName}/recipes")]
        public async Task<IActionResult> GetUserRecipes(string UserName, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and pageSize must be positive integers." });
            }

            try
            {
                var userId = await _userService.GetUserIdByUsernameAsync(UserName);

                var result = await _userService.GetUserRecipesAsync(userId, page, pageSize);

                if (result == null || !result.Items.Any())
                {
                    return NotFound(new { message = "No recipes found for the given user." });
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }


        [HttpGet("{UserName}/blogs")]
        public async Task<IActionResult> GetUserBlogs(string UserName, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {

            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page number and page size must be greater than zero." });
            }

            try
            {
                var userId = await _userService.GetUserIdByUsernameAsync(UserName);
                var result = await _userService.GetUserBlogsAsync(userId, pageNumber, pageSize);

                if (result == null || !result.Items.Any())
                {
                    return NotFound(new { message = "No blogs found for the given user." });
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{UserName}/events")]
        public async Task<IActionResult> GetUserEvents(string UserName, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {

            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new { message = "Page and pageSize must be positive integers." });
            }

            try
            {
                var userId = await _userService.GetUserIdByUsernameAsync(UserName);

                var result = await _userService.GetUserEventsAsync(userId, page, pageSize);

                if (result == null || !result.Items.Any())
                {
                    return NotFound(new { message = "No events found for the given user." });
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }

            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "The specified user does not exist.", Details = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }



        // GET /users/search - Search users
        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers(
            [FromQuery] UserSearchParams userSearchParams,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request data.", errors = ModelState });

            try
            {
                var paginatedResults = await _userService.SearchUsersAsync(userSearchParams, pageNumber, pageSize);
                return Ok(paginatedResults);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
