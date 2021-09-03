using Moq;
using Movies.Data.DataAccess;
using Movies.Data.DataAccess.Repositiories;
using Movies.Data.Models;
using Movies.Data.Services;
using MoviesDataLayer;
using MoviesDataLayer.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data.Test
{
    [Category("Integrational")]
    [Order(2)]
    class ReviewServiceTest
    {
        private Mock<IUnitOfWork> mockUnitOfWork;
        private MoviesDBContext moviesDBContext;
        private UnitOfWork unitOfWork;
        private MovieService movieService;
        private PersonService personService;
        private ReviewService reviewService;


        [OneTimeSetUp]
        public void Setup()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            moviesDBContext = new MoviesDBContext();
            unitOfWork = new UnitOfWork(
                new ActorRepository(moviesDBContext),
                new GenreRepository(moviesDBContext),
                new MovieRepository(moviesDBContext),
                new PersonRepository(moviesDBContext),
                new ReviewRepository(moviesDBContext), 
                new ReviewerRepositoty(moviesDBContext),
                new ReviewerWatchHistoryRepository(moviesDBContext),
                moviesDBContext);
            movieService = new MovieService(unitOfWork);
            personService = new PersonService(unitOfWork);
            reviewService = new ReviewService(unitOfWork);
        }

        [Test, Order(1), Repeat(10)]
        [Category("Integrational")]
        public async Task AddReviewerSuccess()
        {
            var people = await personService.GetAllPersonAsync();
            var randomPersonIndex = TestContext.CurrentContext.Random.Next(people.Count() - 1);
            var reviewer = new Reviewer
            {
                ReviewerId = people.ElementAt(randomPersonIndex).PersonId,
                Person = people.ElementAt(randomPersonIndex)
            };

            await reviewService.AddReviewerAsync(reviewer);

            var getReviewer = await reviewService.GetReviewerAsync(reviewer.ReviewerId);

            Assert.AreEqual(reviewer, getReviewer);
        }

        [Test, Order(2)]
        [Category("Integrational")]
        public async Task AddReviewAndRateRecalculateSuccess([Random(1, 10, 10)] int randRate)
        {
            var reviewers = await reviewService.GetAllReviewersAsync();
            var randomReviewer = GetRandomMember(reviewers);

            var movies = await movieService.GetAllMoviesWithInfoAsync();
            var oldMovie = GetRandomMember(movies);
            var oldMovieRate = oldMovie.Rate;

            var review = new Review
            {
                RevievText = "WOW!",
                Rate = randRate,
                ReviewerId = randomReviewer.ReviewerId,
                MovieId = oldMovie.MovieId
            };


            await reviewService.AddReviewAsync(review, oldMovie.MovieId);
            var getMovie = await movieService.GetMovieWithReviewsAsync(oldMovie.MovieId);
            var getReview = getMovie.Reviews.Last();

            if (oldMovieRate == null)
            {
                Assert.AreEqual(getMovie.Rate, review.Rate);
            }
            else
            {
                Assert.AreEqual(getMovie.Rate, oldMovieRate + review.Rate);
            }

            Assert.AreEqual(review.RevievText, getReview.RevievText);
            Assert.AreEqual(review.Rate, getReview.Rate);
            Assert.AreEqual(review.ReviewerId, getReview.ReviewerId);
        }

        [Test, Order(3)]
        [Category("Integrational")]
        public async Task AddReviewThrowsArgumentExceptionLessThanOne([Random(-5, -1, 10)] int randRate)
        {
            var reviewers = await reviewService.GetAllReviewersAsync();
            var randomReviewer = GetRandomMember(reviewers);

            var movies = await movieService.GetAllMoviesWithInfoAsync();
            var oldMovie = GetRandomMember(movies);
            var oldMovieRate = oldMovie.Rate;

            var review = new Review
            {
                RevievText = "WOW!",
                Rate = randRate,
                ReviewerId = randomReviewer.ReviewerId,
                MovieId = oldMovie.MovieId
            };

            Assert.ThrowsAsync<ArgumentException>(async () => await reviewService.AddReviewAsync(review, oldMovie.MovieId));

        }


        [Test, Order(4)]
        [Category("Integrational")]
        public async Task AddReviewThrowsArgumentExceptionMoreThanTen([Random(11, 100, 10)] int randRate)
        {
            var reviewers = await reviewService.GetAllReviewersAsync();
            var randomReviewer = GetRandomMember(reviewers);

            var movies = await movieService.GetAllMoviesWithInfoAsync();
            var oldMovie = GetRandomMember(movies);
            var oldMovieRate = oldMovie.Rate;

            var review = new Review
            {
                RevievText = "WOW!",
                Rate = randRate,
                ReviewerId = randomReviewer.ReviewerId,
                MovieId = oldMovie.MovieId
            };

            Assert.ThrowsAsync<ArgumentException>(async () => await reviewService.AddReviewAsync(review, oldMovie.MovieId));

        }

        private static T GetRandomMember<T>(IEnumerable<T> members)
        {
            return members.ElementAt(TestContext.CurrentContext.Random.Next(members.Count() - 1));
        }

        [Test, Order(5)]
        [Category("Integrational")]
        public async Task DeleteReviewAndRateRecalculateSuccess()
        {
            var movies = await movieService.GetAllMoviesWithInfoAsync();    
            
            var oldMovie = GetRandomMember(movies);

            while(!oldMovie.Rate.HasValue)
            {
                oldMovie = GetRandomMember(movies);
            }

            var oldMovieRate = oldMovie.Rate;
            var randomReview = GetRandomMember(oldMovie.Reviews);
                            

            await reviewService.DeleteReviewAsync(randomReview.ReviewId);
            var getMovie = await movieService.GetMovieWithReviewsAsync(oldMovie.MovieId);            
            var getReview = await reviewService.GetReviewAsync(randomReview.ReviewId);

            if (getMovie.Reviews.Count() != 0)
            {
                Assert.AreEqual(getMovie.Rate, oldMovieRate - randomReview.Rate);
            }
            else
            {
                Assert.AreEqual(getMovie.Rate, 0);
            }
            
            Assert.AreEqual(null, getReview);
        }
    }
}
