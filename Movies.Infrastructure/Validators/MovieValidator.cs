using FluentValidation;
using Movies.Domain.Models;

namespace Movies.Infrastructure.Validators
{
    public class MovieValidator: AbstractValidator<Movie>
    {
        public MovieValidator()
        {
            RuleSet("PostMovie", () =>
            {
                RuleFor(x => x.MovieId).Empty();
                RuleFor(x => x.MovieName).NotEmpty();
                RuleFor(x => x.LastUpdate).Null();
                RuleFor(x => x.Rate).Null();
            });

            RuleSet("PutMovie", () =>
            {
                RuleFor(x => x.MovieId).NotEmpty();
                RuleFor(x => x.MovieName).NotEmpty();
                RuleFor(x => x.LastUpdate).Null();
                RuleFor(x => x.Rate).Null();
            });
        }
    }
}
