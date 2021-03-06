using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.BusinessLogic.Results;

namespace Movies.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {
        public static ActionResult ReturnFromResponse(this ControllerBase controllerBase, Result response)
        {
            switch (response.ResultType)
            {
                case ResultType.NotFound:
                    return controllerBase.NotFound(response);

                case ResultType.NotValid:
                    return controllerBase.BadRequest(response);

                case ResultType.Forbidden:
                    return controllerBase.StatusCode(StatusCodes.Status403Forbidden, response);

                case ResultType.Unauthorized:
                    return controllerBase.Unauthorized(response);

                case ResultType.Unexpected:
                    return controllerBase.BadRequest(response);

                default:
                    return controllerBase.BadRequest(response);
            }
        }
    }
}
