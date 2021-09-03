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
    public class HttpProducerService : IProducerService
    {
        private readonly IHttpResultService httpResultService;

        public HttpProducerService(IHttpResultService httpResultService)
        {
            this.httpResultService = httpResultService;
        }

        public Task<Result<Producer>> AddProducerAsync(Producer producer)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteProducerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Producer>>> GetAllProducersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Producer>> GetProducerAsync(int id)
        {
            var result = new Result<Producer>();
            await ResultHandler.TryExecuteAsync(result, GetProducerAsync(id, result));

            return result;
        }

        public async Task<Result<Producer>> GetProducerAsync(int id, Result<Producer> result)
        {
            await httpResultService.SendRequestAsync<Producer>($"Producers/{id}", "GET", result);

            return result;
        }

        public Task<Result<Producer>> UpdateProducerAsync(Producer Producer)
        {
            throw new NotImplementedException();
        }
    }
}
