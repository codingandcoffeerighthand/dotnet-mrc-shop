using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Behaviors;


public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger
)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    async Task<TResponse> IPipelineBehavior<TRequest, TResponse>.Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "[START] handle reequest: {Request}< {Response} >: {message}"
            , typeof(TResponse), typeof(TResponse), request);

        var timer = new Stopwatch();
        timer.Start();
        var res = await next();
        timer.Stop();

        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] handle request: {Reqeuest} taken {TimeTaken} seconds"
            , request, timeTaken.Seconds);
        }
        logger.LogInformation("[END] handle reequest {TypeRequest} - {Request}: {Response} "
        , typeof(TRequest), request, res);
        return res;
    }
}