using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Fragomen.Messaging.Application.Common.Behaviors;
public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fragomen.Clean Request: {@Request}", request);
    }
}
