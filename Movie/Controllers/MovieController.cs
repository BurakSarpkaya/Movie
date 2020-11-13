using BLL.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieAppService _movieAppService;
        public MovieController(IMovieAppService movieAppService)
        {
            _movieAppService = movieAppService;
        }

        [Authorize]
        [HttpGet("GetList")]
        public IActionResult GetMovieList(int page, int pageSize)
        {
            var result = _movieAppService.GetMovies(page, pageSize);

            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }

        [Authorize]
        [HttpGet("GetPeriodicList")]
        public IActionResult GetMoviePeriodicList()
        {
            var result = _movieAppService.GetMoviesThread();

            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }

        //[Authorize]
        [HttpGet("GetById")]
        public IActionResult GetMovieById(int movieId)
        {
            
            var result = _movieAppService.GetMovieById(movieId);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult MovieRate([FromBody] AccountMovieCreateInput accountMovie, int movieId)
        {
            var result = _movieAppService.MovieAddRate(accountMovie, movieId);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

        [Authorize]
        [HttpPost("MailSend")]
        public IActionResult MailSend(string email, int movieId)
        {
            var result = _movieAppService.MailSend(email, movieId);

            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }
    }
}
