namespace Chat.Domain.Monitoring;

public interface IMonitoredRequest
{
    void MonitoreSucces(DomainMetrics metrics);
    void MonitoreFailure(DomainMetrics metrics);
}