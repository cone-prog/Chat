using Chat.Domain.Entities;
using Chat.Domain.Monitoring;
using MediatR;

namespace Chat.Domain.UseCase.Conversations.GetConversationById;

public record GetConversationByIdQuery(Guid Id) : IRequest<Conversation>, IMonitoredRequest
{
    private const string CounterName = "conversation.fetched";
    
    public void MonitoreSucces(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(true));
    }

    public void MonitoreFailure(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(false));
    }
}
