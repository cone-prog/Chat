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

        try
        {
            var result = await next.Invoke(cancellationToken);
            monitoredRequest.MonitoreSucces(metrics);
            return result;
        }

        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled error caught while handling command {Command}",  request);
            monitoredRequest.MonitoreFailure(metrics);
            throw;
        }
    }
}