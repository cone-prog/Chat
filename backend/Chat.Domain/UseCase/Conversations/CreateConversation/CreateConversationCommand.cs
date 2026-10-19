using Chat.Domain.Entities;
using Chat.Domain.Monitoring;
using MediatR;

namespace Chat.Domain.UseCase.Conversations.CreateConversation;

public record CreateConversationCommand(string Name) :  IRequest<Conversation>, IMonitoredRequest
{
    private const string CounterName = "conversation.created";
    
    public void MonitoreSucces(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(true));
    }

    public void MonitoreFailure(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(false));
    }
}
