using System.Diagnostics;
using Chat.Domain.Monitoring;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chat.Domain.Behaviors;

public class MonitoringBehavior<TRequest, TResponse> (DomainMetrics metrics, 
    ILogger<MonitoringBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (request is not IMonitoredRequest monitoredRequest) return await next.Invoke(cancellationToken);
        
        using var activity = DomainMetrics.ActivitySource.StartActivity("usecase", ActivityKind.Internal, default(ActivityContext));
        activity?.AddTag("chat.command", request.GetType().Name);
        try
        {
            var result = await next.Invoke(cancellationToken);
            monitoredRequest.MonitoreSucces(metrics);
            logger.LogInformation("request with command {command} handled successfully", request);
            activity!.SetTag("error", false);
            return result;
        }

        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled error caught while handling command {command}",  request);
            monitoredRequest.MonitoreFailure(metrics);
            activity!.SetTag("error", true);
            throw;
        }
    }
}