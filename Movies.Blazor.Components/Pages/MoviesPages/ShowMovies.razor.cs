using AutoMapper;
using Microsoft.AspNetCore.Components;
using Movies.Domain.Models;
using Movies.BusinessLogic.Results;
using Movies.BusinessLogic.Services.Interfaces;
using Movies.Infrastructure.Models.Movie;
using Movies.Infrastructure.Models.User;
using Movies.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Blazor.Components.Pages.MoviesPages
{
    public partial class ShowMovies
    {
        private Result<IEnumerable<MovieResponse>> movies;        

        private Result<GetUserResponse> currentUser;

        [Inject]
        private IMapper mapper { get; set; }

        [Inject]
        private ICustomAuthentication customAuthentication { get; set; }

        [Inject]
        private IMovieService movieService { get; set; }

        private bool showOnlyMyMovies { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadMoviesAsync(false);

            currentUser = await customAuthentication.GetCurrentUserDataAsync();                      
        }

        private async Task OnMovieDeletedAsync(MovieResponse movie)
        {           
            await LoadMoviesAsync(showOnlyMyMovies);
        }
      
        private async Task LoadMoviesAsync(bool showOnlyMyMovies)
        {
            var getMovies = new Result<IEnumerable<Movie>>();
            if (showOnlyMyMovies)
            {
                getMovies = await movieService.GetMoviesByProducerIdAsync(currentUser.Value.UserId);
            }
            else
            {
                getMovies = await movieService.GetAllMoviesAsync();
                movies = mapper.Map<Result<IEnumerable<MovieResponse>>>(getMovies);
            }
            movies = mapper.Map<Result<IEnumerable<MovieResponse>>>(getMovies);
        }

        private async Task OnShowOnlyMyMoviesAsync(ChangeEventArgs e)
        {
            showOnlyMyMovies = (bool)e.Value;
            await LoadMoviesAsync(showOnlyMyMovies);
        }        
    }
}
