using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Chat.Domain.Monitoring;

public class DomainMetrics(IMeterFactory factory)
{
    private readonly Meter meter = factory.Create("Chat.Domain");
    
    private readonly ConcurrentDictionary<string, Counter<int>> counters = new();
    
    public static readonly ActivitySource ActivitySource = new("Chat.Domain");

    public void IncrementCount(string name, int value, IDictionary<string, object?>? additionalTags = null)
    {
        var counter = counters.GetOrAdd(name, _ => meter.CreateCounter<int>(name));
        counter.Add(value, additionalTags?.ToArray() ??  ReadOnlySpan<KeyValuePair<string, object?>>.Empty);
    }

    public static Dictionary<string, object?>? ResultTags(bool success) => new Dictionary<string, object?>()
    {
        ["success"] = success
    };
}