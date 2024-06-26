using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Fragomen.Infrastructure.Exceptions
{
    public class InternalServerErrorExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<InternalServerErrorExceptionHandler> _logger;

        public InternalServerErrorExceptionHandler(ILogger<InternalServerErrorExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception,
                "Exception occurred: {Message}",
                exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
