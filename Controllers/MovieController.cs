using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using otonoMovieAppTask.Response;
using otonoMovieAppTask.Services;

namespace otonoMovieAppTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet("search/{title}")]
        public async Task<IActionResult> MovieSearch(string title)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    return BadRequest("Please ensure the movie name is valid");
                }
                var result = await _movieService.MovieSearch(title);
                return Ok(ApiResponse.Success(result));
            }
            catch (Exception ex)
            {
                return BadRequest("We ran into a problem while attempting to search your movie.");
            }
        }

        [HttpGet("searchHistory")]
        public async Task<IActionResult> GetSearchHistory()
        {
            try
            {
                var search = await _movieService.GetSearchHistory();
                if (search == null)
                {
                    return NotFound(ApiResponse.Failed("Sorry, you haven't made any recent searches ."));
                }
                return Ok(ApiResponse.Success(search));
            }
            catch (Exception ex)
            {
                return BadRequest("We are sorry, kindly try again later.");
            }
        }
    }
}
