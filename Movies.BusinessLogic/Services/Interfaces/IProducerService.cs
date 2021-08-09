using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Domain.Models;
using Movies.BusinessLogic.Results;

namespace Movies.BusinessLogic.Services.Interfaces
{
    public interface IProducerService
    {
        Task<Result<IEnumerable<Producer>>> GetAllProducersAsync();
        Task<Result<Producer>> GetProducerAsync(int id);
        Task<Result<Producer>> AddProducerAsync(Producer producer);
        Task<Result<Producer>> UpdateProducerAsync(Producer Producer);
        Task<Result> DeleteProducerAsync(int id);
    }
}
