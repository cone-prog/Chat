using Chat.Domain.Authentication;
using Chat.Domain.Monitoring;
using MediatR;

namespace Chat.Domain.UseCase.Users.SignOn;

public record SignOnCommand(string Name,string Email,  string Password) : IRequest<IIdentity>,
    IMonitoredRequest
{
    private const string CounterName = "user.signOn";
    public void MonitoreSucces(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(true));
    }

    public void MonitoreFailure(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(false));
    }
}
