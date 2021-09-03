using FluentValidation;

namespace Movies.Infrastructure.Validators.User
{
    public class UserValidator: AbstractValidator<Movies.Domain.Models.User>
    {
        public UserValidator()
        {
            RuleSet("RegisterUpdate", () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(30)
                    .Matches(@"^[\w]+$")
                    .When(x => !string.IsNullOrEmpty(x.Name));
            });

            RuleFor(x => x.Login)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(30)
                .Matches(@"^[\w\d]+$")
                .When(x => !string.IsNullOrEmpty(x.Login));

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(30)
                .Matches(@"^[\S]+$")
                .When(x => !string.IsNullOrEmpty(x.Password));
        }
    }
}
