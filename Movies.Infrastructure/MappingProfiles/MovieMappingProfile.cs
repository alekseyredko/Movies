using System.Collections.Generic;
using AutoMapper;
using Movies.Infrastructure.Models.Movie;
using Movies.BusinessLogic.Results;
using Movies.Domain.Models;

namespace Movies.Infrastructure.MappingProfiles
{
    public class MovieMappingProfile: Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<MovieRequest, Movie>();
            CreateMap<UpdateMovieRequest, Movie>();

            CreateMap<Movie, MovieResponse>();
            CreateMap<Result<Movie>, Result<MovieResponse>>();
            CreateMap<Result<IEnumerable<Movie>>, Result<IEnumerable<MovieResponse>>>();
        }
    }
}
