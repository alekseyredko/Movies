using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Movies.Infrastructure.Extensions;
using Movies.Infrastructure.Models.Movie;
using Movies.Infrastructure.Services;
using Movies.BusinessLogic.Services.Interfaces;
using Movies.BusinessLogic.Results;
using Movies.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies.Infrastructure.Controllers
{
    //TODO: validate entities
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IReviewService _reviewService;
        private readonly IActorsService _actorsService;
        private IMapper _mapper;

        public MoviesController(IMovieService movieService, IActorsService actorsService, IReviewService reviewService, IMapper mapper)
        {
            _movieService = movieService;
            _actorsService = actorsService;
            _reviewService = reviewService;
            _mapper = mapper;
        }

        // GET: api/<MoviesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMoviesAsync()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            var result = _mapper.Map<Result<IEnumerable<Movie>>, Result<IEnumerable<MovieResponse>>>(movies);

            switch (result.ResultType)
            {
                case ResultType.Ok:
                    return Ok(result);

                default:
                    return this.ReturnFromResponse(result);
            }
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);
            var result = _mapper.Map<Result<Movie>, Result<MovieResponse>>(movie);

            switch (result.ResultType)
            {
                case ResultType.Ok:
                    return Ok(result);

                default:
                    return this.ReturnFromResponse(result);
            }
        }

        // POST api/<MoviesController>
        [HttpPost]
        [Authorize(Roles = "Producer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostMovieAsync(MovieRequest request)
        {
            var movie = _mapper.Map<MovieRequest, Movie>(request);
            var id = RefreshTokenService.GetIdFromToken(HttpContext);

            var added = await _movieService.AddMovieAsync(id, movie);
            var result = _mapper.Map<Result<Movie>, Result<MovieResponse>>(added);

            switch (result.ResultType)
            {
                case ResultType.Ok:
                    return Ok(result);

                default:
                    return this.ReturnFromResponse(result);
            }
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Producer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutMovieAsync(int id, [FromBody] UpdateMovieRequest request)
        {
            var movie = _mapper.Map<UpdateMovieRequest, Movie>(request);
            var producerId = RefreshTokenService.GetIdFromToken(HttpContext);

            var added = await _movieService.UpdateMovieAsync(producerId, id, movie);
            var result = _mapper.Map<Result<Movie>, Result<MovieResponse>>(added);

            switch (result.ResultType)
            {
                case ResultType.Ok:
                    return Ok(result);

                default:
                    return this.ReturnFromResponse(result);
            }
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Producer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var producerId = RefreshTokenService.GetIdFromToken(HttpContext);

            var response = await _movieService.DeleteMovieAsync(producerId, id);

            switch (response.ResultType)
            {
                case ResultType.Ok:
                    return Ok(response);

                default:
                    return this.ReturnFromResponse(response);
            }
        }

        [HttpGet("{id}/reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReviewsFromMovieAsync(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieWithReviewsAsync(id);
                return Ok(movie.Reviews);
            }
            catch (InvalidOperationException e)
            {
                return NotFound();
            }
        }
    }
}
