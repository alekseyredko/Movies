using AutoMapper;
using Movies.Infrastructure.Models;
using Movies.Infrastructure.Models.Producer;
using Movies.BusinessLogic.Results;
using Movies.Domain.Models;

namespace Movies.Infrastructure.MappingProfiles
{
    public class ProducerMappingProfile: Profile
    {
        public ProducerMappingProfile()
        {
            CreateMap<Producer, ProducerResponse>();
            CreateMap<Result<Producer>, Result<ProducerResponse>>();

            CreateMap<ProducerRequest, Producer>();
            CreateMap<Producer, RegisterProducerResponse>();
            CreateMap<Result<Producer>, Result<RegisterProducerResponse>>();

            CreateMap<Result, Result<TokenResponse>>();
        }
    }
}
