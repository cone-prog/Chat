using System.Collections.Concurrent;
using System.Diagnostics.Metrics;

namespace Chat.Domain.Monitoring;

public class DomainMetrics
{
    private readonly Meter meter = new Meter("Chat.Domain");
    
    private readonly ConcurrentDictionary<string, Counter<int>> counters = new();
    

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