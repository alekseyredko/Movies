using Movies.BusinessLogic.Results;
using Movies.BusinessLogic.Services.Interfaces;
using Movies.Domain.Models;
using Movies.Infrastructure.BlazorWasm.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Infrastructure.BlazorWasm.Services
{
    public class HttpMovieService : IMovieService
    {
        private readonly IHttpResultService httpResultService;

        public HttpMovieService(IHttpResultService httpResultService)
        {
            this.httpResultService = httpResultService;
        }

        public Task AddActorToMovieAsync(int movieId, int actorId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Movie>> AddMovieAsync(int producerId, Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteActorFromMovieAsync(int movieId, int actorId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteMovieAsync(int producerId, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<Movie>>> GetAllMoviesAsync()
        {
            var result = new Result<IEnumerable<Movie>>();
            await ResultHandler.TryExecuteAsync(result, GetAllMoviesAsync(result));

            return result;
        }

        protected async Task<Result<IEnumerable<Movie>>> GetAllMoviesAsync(Result<IEnumerable<Movie>> result)
        {
            await httpResultService.SendRequestAsync<IEnumerable<Movie>>("Movies", "GET", result);

            return result;
        }

        public Task<IEnumerable<Movie>> GetAllMoviesWithInfoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Actor>> GetMovieActorsAsync(int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Movie>> GetMovieAsync(int id)
        {
            var result = new Result<Movie>();
            await ResultHandler.TryExecuteAsync(result, GetMovieAsync(id, result));

            return result;
        }

        public async Task<Result<Movie>> GetMovieAsync(int id, Result<Movie> result)
        {
            await httpResultService.SendRequestAsync($"Movies/{id}", "GET", result);

            return result;
        }

        public async Task<Result<IEnumerable<Movie>>> GetMoviesByProducerIdAsync(int producerId)
        {
            var result = new Result<IEnumerable<Movie>>();
            await ResultHandler.TryExecuteAsync(result, GetMoviesByProducerIdAsync(producerId, result));

            return result;
        }

        protected async Task<Result<IEnumerable<Movie>>> GetMoviesByProducerIdAsync(int producerId, 
            Result<IEnumerable<Movie>> result)
        {
            await httpResultService.SendRequestAsync<IEnumerable<Movie>>($"Producers/{producerId}/movies", "GET", result);
            return result;
        }

        public Task<Movie> GetMovieWithReviewsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Movie>> UpdateMovieAsync(int producerId, int movieId, Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
