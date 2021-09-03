using System;
using System.Threading.Tasks;

namespace Movies.DataAccess.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IActorRepository Actors { get; }
        IGenreRepository Genres { get; }
        IMovieRepository Movies { get; }
        IPersonRepository Persons { get; }
        IProducerRepository Producers { get; }
        IReviewRepository Reviews { get; }
        IReviewerRepository Reviewers { get; }
        IReviewerWatchHistoryRepository ReviewersWatchHistory { get; }
        IUserRepository UserRepository { get; }
        IRefreshTokenRepository RefreshTokens { get; }

        Task<int> SaveAsync();
    }
}
