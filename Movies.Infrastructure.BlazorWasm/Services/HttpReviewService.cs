using Movies.BusinessLogic.Results;
using Movies.BusinessLogic.Services.Interfaces;
using Movies.Domain.Models;
using Movies.Infrastructure.BlazorWasm.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.BlazorWasm.Services
{
    public class HttpReviewService : IReviewService
    {
        private readonly IHttpResultService httpResultService;

        public HttpReviewService(IHttpResultService httpResultService)
        {
            this.httpResultService = httpResultService;
        }

        public Task<Result<Review>> AddReviewAsync(int movieId, int reviewerId, Review review)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Reviewer>> AddReviewerAsync(Reviewer reviewer)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteReviewAsync(int reviewerId, int reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteReviewerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Reviewer>>> GetAllReviewersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<Review>>> GetAllReviewsAsync()
        {
            var result = new Result<IEnumerable<Review>>();
            await ResultHandler.TryExecuteAsync(result, GetAllReviewsAsync(result));

            return result;
        }

        public async Task<Result<IEnumerable<Review>>> GetAllReviewsAsync(Result<IEnumerable<Review>> result)
        {
            await httpResultService.SendRequestAsync<IEnumerable<Review>>("Reviews", "GET", result);

            return result;
        }

        public Task<Result<IEnumerable<Review>>> GetMovieReviewsAsync(int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Review>> GetReviewAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Reviewer>> GetReviewerAsync(int id)
        {
            var result = new Result<Reviewer>();
            await ResultHandler.TryExecuteAsync(result, GetReviewerAsync(id, result));

            return result;
        }

        public async Task<Result<Reviewer>> GetReviewerAsync(int id, Result<Reviewer> result)
        {
            await httpResultService.SendRequestAsync<Reviewer>($"Reviewers/{id}", "GET", result);

            return result;
        }

        public Task<Result<IEnumerable<Review>>> GetReviewerReviewsAsync(int reviewerId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Review>> UpdateReviewAsync(int reviewId, int reviewerId, Review review)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Reviewer>> UpdateReviewerAsync(Reviewer reviewer)
        {
            throw new NotImplementedException();
        }
    }
}
