using Chat.Domain.Entities;
using Chat.Domain.Monitoring;
using MediatR;

namespace Chat.Domain.UseCase.Messages.GetMessages;

public record GetMessagesQuery(Guid ConversationId, Guid? BeforeMessageId, int Limit) : IRequest<IEnumerable<Message>>
    , IMonitoredRequest
{
    private const string CounterName = "messages.fetched";
    public void MonitoreSucces(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(true));
    }

    public void MonitoreFailure(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(false));
    }
}
