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
    [Order(1)]
    public class MovieServiceTest
    {
        private Mock<IUnitOfWork> mockUnitOfWork;
        private MoviesDBContext moviesDBContext;
        private UnitOfWork unitOfWork;
        private MovieService movieService;
        private PersonService personService;
        private ActorsService actorsService;

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
            actorsService = new ActorsService(unitOfWork);
        }

        [Test, Combinatorial, Order(1)]
        [Category("Integrational")]
        public async Task AddMovieSuccess(
            [Range(1, 4, 1)] int durationHours, [Range(1, 60, 5)] int durationMinutes)
        {
            var movie = new Movie
            {
                MovieName = GetRandomString(20),
                Duration = new TimeSpan(durationHours, durationMinutes, 0)
            };

            await movieService.AddMovieAsync(movie);
            var getMovie = await movieService.GetMovieAsync(movie.MovieId);

            Assert.AreEqual(movie, getMovie);
        }

        private static string GetRandomString(int length)
        {
            return TestContext.CurrentContext.Random.GetString(length, "ABCDEFGHJKLMNOPQRSTUVWXYZ")
                + " " + TestContext.CurrentContext.Random.GetString(length, "ABCDEFGHJKLMNOPQRSTUVWXYZ");
        }

        [Test, Order(2)]
        [Category("Integrational")]
        public async Task AddActorsSuccess([Range(1, 90)] int range)
        {
            var person = new Person
            {
                PersonName = GetRandomString(5)
            };
            var actor = new Actor
            {
                Age = range
            };

            await personService.AddPersonAsync(person);

            var getAllPersonAsync = await personService.GetAllPersonAsync();
            actor.ActorId = getAllPersonAsync.Last().PersonId;

            await actorsService.AddActorAsync(actor);
        }

        [Test, Order(3)]
        [Category("Integrational")]
        public async Task AddActorsToMoviesAsync()
        {
            var movies = await movieService.GetAllMoviesAsync();
            var actors = await actorsService.GetActorsAsync();
            foreach (var movie in movies)
            {
                var maxActorsCount = TestContext.CurrentContext.Random.Next(5);
                for (int i = 0; i < maxActorsCount; i++)
                {
                    movie.Actors.Add(actors.ElementAt(TestContext.CurrentContext.Random.Next(actors.Count() - 1)));
                }
                await movieService.UpdateMovieAsync(movie);
            }     
        }


        [Test, Order(10)]
        [Category("Integrational")]
        public async Task DeleteMovieSuccess()
        {
            var getMovie = movieService.GetAllMoviesAsync().Result.Last();            
            await movieService.DeleteMovieAsync(getMovie.MovieId);
        }

        [Test, Order(4)]
        [Category("Integrational")]
        public async Task DeleteActorFromMovieSuccess()
        {
            var movies = await movieService.GetAllMoviesWithInfoAsync();

            var movie = movies.First();
            var actor = movie.Actors.Last();
            await movieService.DeleteActorFromMovieAsync(movie.MovieId, actor.ActorId);
        }

        [Test, Order(5)]
        [Category("Integrational")]
        public void DeleteActorFromMovieReturnsArgumentActorException()
        {           
            Assert.ThrowsAsync<InvalidOperationException>(async () => await movieService.DeleteActorFromMovieAsync(10, 4));
        }

        [Test, Order(6)]
        [Category("Integrational")]
        public void DeleteActorFromMovieReturnsArgumentMovieException()
        {      
            Assert.ThrowsAsync<InvalidOperationException>(async () => await movieService.DeleteActorFromMovieAsync(111, 4));
        }     

        [Repeat(20), Order(7)]
        [Category("Integrational")]
        public async Task GetRandomMovieActorsSuccess()
        {
            var actors = await movieService.GetAllMoviesWithInfoAsync();
            var randomIndex = TestContext.CurrentContext.Random.Next(actors.Count() - 1);

            var actor = await movieService.GetMovieActorsAsync(actors.ElementAt(randomIndex).MovieId);
            Assert.AreNotEqual(actor, null);
        }

        [Repeat(20), Order(8)]
        [Category("Integrational")]
        public async Task AddRandomActorToRandomMovieSuccess()
        {
            var actors = await actorsService.GetAllActorsWithMoviesAsync();
            var movies = await movieService.GetAllMoviesWithInfoAsync();
            var randomActorIndex = TestContext.CurrentContext.Random.Next(actors.Count() - 1);
            var randomMovieIndex = TestContext.CurrentContext.Random.Next(movies.Count() - 1);

            var movie = movies.ElementAt(randomMovieIndex);
            var actor = actors.ElementAt(randomActorIndex);

            while (movie.Actors.FirstOrDefault(x => x.ActorId == actor.ActorId) != null)
            {
                randomActorIndex = TestContext.CurrentContext.Random.Next(actors.Count() - 1);
                actor = actors.ElementAt(randomActorIndex);
            }

            await movieService.AddActorToMovieAsync(randomMovieIndex, randomActorIndex);
        }
    }
}
