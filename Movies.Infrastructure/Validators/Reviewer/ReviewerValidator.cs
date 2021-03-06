using FluentValidation;
using Movies.Domain.Models;

namespace Movies.Infrastructure.Validators
{
    public class ReviewerValidator: AbstractValidator<Reviewer>
    {
        public ReviewerValidator()
        {
            RuleSet("PostReviewer", () =>
            {
                //RuleFor(x => x.ReviewerId).NotEmpty();
                RuleFor(x => x.Person)
                    .SetValidator(new PersonValidator(), "PostPerson", "other");
            });

            RuleSet("PutReviewer", () =>
            {
                
                //RuleFor(x => x.ReviewerId).NotEmpty();
            });

            RuleSet("other", () =>
            {
                RuleFor(x => x.ReviewerId).Empty();
                RuleForEach(x => x.Movies).Null();
                RuleForEach(x => x.ReviewerWatchHistories).Null();
                RuleForEach(x => x.Reviews).Null();
            });
        }
    }
}
