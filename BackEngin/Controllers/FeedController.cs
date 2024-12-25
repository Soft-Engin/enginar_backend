using Microsoft.AspNetCore.Mvc;
using BackEngin.Services.Interfaces;
using Models.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace BackEngin.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/feed")]
    [ApiController]
    public class FeedController : ApiControllerBase
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        // GET /ingredients - Get paginated list of Recipes
        [HttpGet("recipe")]
        public async Task<IActionResult> GetPaginatedRecipes([FromQuery] string seed = "seed", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var paginatedIngredients = await _feedService.GetRecipeFeed(seed, pageNumber, pageSize);
                return Ok(paginatedIngredients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // GET /ingredients - Get paginated list of Blogs
        [HttpGet("blog")]
        public async Task<IActionResult> GetPaginatedBlogs([FromQuery] string seed = "seed", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var paginatedIngredients = await _feedService.GetBlogsFeed(seed, pageNumber, pageSize);
                return Ok(paginatedIngredients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // GET /ingredients - Get paginated list of Events
        [HttpGet("event")]
        public async Task<IActionResult> GetPaginatedEvents([FromQuery] string seed = "seed", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var paginatedIngredients = await _feedService.GetEventFeed(seed, pageNumber, pageSize);
                return Ok(paginatedIngredients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // GET /ingredients - Get paginated list of Recipes
        [HttpGet("recipe/recent-followed-feed")]
        [Authorize]
        public async Task<IActionResult> GetPaginatedRecentFollowedRecipes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var paginatedIngredients = await _feedService.GetFollowedRecentRecipeFeed(pageNumber, pageSize, await GetActiveUserId());
                return Ok(paginatedIngredients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // GET /ingredients - Get paginated list of Blogs
        [HttpGet("blog/recent-followed-feed")]
        public async Task<IActionResult> GetPaginatedRecentFollowedBlogs([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var paginatedIngredients = await _feedService.GetFollowedRecentBlogsFeed(pageNumber, pageSize, await GetActiveUserId());
                return Ok(paginatedIngredients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // GET /ingredients - Get paginated list of Events
        [HttpGet("event/recent-followed-feed")]
        public async Task<IActionResult> GetPaginatedRecentFollowedEvents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var paginatedIngredients = await _feedService.GetFollowedRecentEventFeed(pageNumber, pageSize, await GetActiveUserId());
                return Ok(paginatedIngredients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // GET /ingredients - Get paginated list of Events
        [HttpGet("event/upcoming-followed-feed")]
        public async Task<IActionResult> GetPaginatedUpcomingFollowedEvents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var paginatedIngredients = await _feedService.GetFollowedUpcomingEventFeed(pageNumber, pageSize, await GetActiveUserId());
                return Ok(paginatedIngredients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
