using Chat.Domain.Monitoring;
using MediatR;

namespace Chat.Domain.UseCase.Users.SignOut;

public record SignOutCommand : IRequest, IMonitoredRequest
{
    private const string CounterName = "user.signOut";
    
    public void MonitoreSucces(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(true));
    }

    public void MonitoreFailure(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(false));
    }
}