using Chat.Domain.Entities;
using Chat.Domain.Monitoring;
using MediatR;

namespace Chat.Domain.UseCase.Conversations.GetConversations;

public record GetConversationsQuery() : IRequest<List<Conversation>>, IMonitoredRequest
{
    private const string CounterName = "conversations.fetched";
    
    public void MonitoreSucces(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(true));
    }

    public void MonitoreFailure(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(false));
    }
}