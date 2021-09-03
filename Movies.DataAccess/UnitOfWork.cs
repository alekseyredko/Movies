﻿using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Interfaces;
using Movies.DataAccess.Repositiories;
using System.Threading.Tasks;

namespace Movies.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private MoviesDBContext _moviesDBContext;

        private IDbContextFactory<MoviesDBContext> dbContextFactory;

        private readonly IActorRepository _actorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IReviewerWatchHistoryRepository _reviewerWatchHistoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository refreshTokens;

        public IActorRepository Actors => _actorRepository;

        public IGenreRepository Genres => _genreRepository;

        public IMovieRepository Movies => _movieRepository;

        public IPersonRepository Persons => _personRepository;

        public IProducerRepository Producers => _producerRepository;

        public IReviewRepository Reviews => _reviewRepository;

        public IReviewerRepository Reviewers => _reviewerRepository;

        public IReviewerWatchHistoryRepository ReviewersWatchHistory => _reviewerWatchHistoryRepository;

        public IUserRepository UserRepository => _userRepository;

        public IRefreshTokenRepository RefreshTokens => refreshTokens;

        public UnitOfWork(IDbContextFactory<MoviesDBContext> dbContextFactory): this(dbContextFactory.CreateDbContext())
        {
            
        }

        public UnitOfWork(MoviesDBContext moviesDBContext)
        {
            _moviesDBContext = moviesDBContext;

            _actorRepository = new ActorRepository(_moviesDBContext);
            _genreRepository = new GenreRepository(_moviesDBContext);
            _movieRepository = new MovieRepository(_moviesDBContext);
            _personRepository = new PersonRepository(_moviesDBContext);
            _reviewRepository = new ReviewRepository(_moviesDBContext);
            _reviewerRepository = new ReviewerRepositoty(_moviesDBContext);
            _reviewerWatchHistoryRepository = new ReviewerWatchHistoryRepository(_moviesDBContext);
            _userRepository = new UserRepository(_moviesDBContext);
            _producerRepository = new ProducerRepository(_moviesDBContext);
            refreshTokens = new RefreshTokenRepository(_moviesDBContext);
        }
        
        public void Dispose()
        {
            _moviesDBContext.Dispose();
        }

        public Task<int> SaveAsync()
        {
            return _moviesDBContext.SaveChangesAsync();
        }
    }
}
