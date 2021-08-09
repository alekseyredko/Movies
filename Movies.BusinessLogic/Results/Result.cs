using System.Collections.Generic;
using FluentValidation.Results;

namespace Movies.BusinessLogic.Results
{
    public class Result
    {
        public string Title { get; set; }

        public ResultType ResultType { get; set; }

        public Dictionary<string, ICollection<string>> Errors { get; }

        public Result()
        {
            Errors = new Dictionary<string, ICollection<string>>();
        }

        public void AddError(string propertyError, string errorDesc)
        {
            ICollection<string> errorsDesc;
            if (Errors.TryGetValue(propertyError, out errorsDesc))
            {
                errorsDesc.Add(errorDesc);
            }

            else
            {
                errorsDesc = new List<string>();
                errorsDesc.Add(errorDesc);
                Errors.Add(propertyError, errorsDesc);
            }
        }

        public void AddErrors(IEnumerable<ValidationFailure> failures)
        {
            foreach (var failure in failures)
            {
                AddError(failure.PropertyName, failure.ErrorMessage);
            }
        }
    }

    public class Result<T>: Result
    {
        public T Value { get; set; }

        public Result():base()
        {
            Value = default(T);
        }
    }

    public enum ResultType
    {
        Ok,
        AlreadyExists,
        NotFound,
        NotValid,
        Unauthorized,
        Forbidden,
        Unexpected
    }
}
