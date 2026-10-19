using Chat.Domain.Entities;
using Chat.Domain.Monitoring;
using MediatR;

namespace Chat.Domain.UseCase.Messages.CreateMessage;

public record CreateMessageCommand(Guid ConversationId, string Text) : IRequest<Message>, IMonitoredRequest
{
    private const string CounterName = "message.created";
    
    public void MonitoreSucces(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(true));
    }

    public void MonitoreFailure(DomainMetrics metrics)
    {
        metrics.IncrementCount(CounterName, 1, DomainMetrics.ResultTags(false));
    }
}
