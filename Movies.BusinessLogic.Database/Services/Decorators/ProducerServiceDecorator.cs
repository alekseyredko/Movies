using Movies.BusinessLogic.Results;
using Movies.BusinessLogic.Services.Interfaces;
using Movies.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.DataAccess;

namespace Movies.BusinessLogic.Database.Services.Decorators
{
    public class ProducerServiceDecorator : IProducerService
    {
        private readonly IDbContextFactory<MoviesDBContext> dbContextFactory;

        public ProducerServiceDecorator(IDbContextFactory<MoviesDBContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<Result<Producer>> AddProducerAsync(Producer producer)
        {
            using (var unitOfWork = new UnitOfWork(dbContextFactory.CreateDbContext()))
            {
                var service = new ProducerService(unitOfWork);
                return await service.AddProducerAsync(producer);
            }
        }

        public async Task<Result> DeleteProducerAsync(int id)
        {
            using (var unitOfWork = new UnitOfWork(dbContextFactory.CreateDbContext()))
            {
                var service = new ProducerService(unitOfWork);
                return await service.DeleteProducerAsync(id);
            }
        }

        public async Task<Result<IEnumerable<Producer>>> GetAllProducersAsync()
        {
            using (var unitOfWork = new UnitOfWork(dbContextFactory.CreateDbContext()))
            {
                var service = new ProducerService(unitOfWork);
                return await service.GetAllProducersAsync();
            }
        }

        public async Task<Result<Producer>> GetProducerAsync(int id)
        {
            using (var unitOfWork = new UnitOfWork(dbContextFactory.CreateDbContext()))
            {
                var service = new ProducerService(unitOfWork);
                return await service.GetProducerAsync(id);
            }
        }

        public async Task<Result<Producer>> UpdateProducerAsync(Producer Producer)
        {
            using (var unitOfWork = new UnitOfWork(dbContextFactory.CreateDbContext()))
            {
                var service = new ProducerService(unitOfWork);
                return await service.UpdateProducerAsync(Producer);
            }
        }
    }
}
