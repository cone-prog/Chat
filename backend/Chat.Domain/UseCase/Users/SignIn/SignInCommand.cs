using Chat.Domain.Authentication;
using Chat.Domain.Monitoring;
using MediatR;

namespace Chat.Domain.UseCase.Users.SignIn;

public record SignInCommand(
    string Email,
    string Password) : IRequest<(IIdentity identity, string token)> , IMonitoredRequest
{
    private const string CounterName = "user.signIn";
    
    public void MonitoreSucces(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(true));
    }

    public void MonitoreFailure(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(false));
    }
}
