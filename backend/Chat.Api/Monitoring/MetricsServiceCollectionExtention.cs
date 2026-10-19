using OpenTelemetry.Metrics;

namespace Chat.Api.Monitoring;

public static class MetricsServiceCollectionExtentions
{
    public static IServiceCollection AddApiMetrics(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithMetrics(builder => builder
                .AddAspNetCoreInstrumentation()
                .AddMeter("Chat.Domain")
                .AddPrometheusExporter()
                .AddView("http.server.request.duration", new ExplicitBucketHistogramConfiguration
                {
                    Boundaries = new double[]{  0, 0.05, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 10}
                }));

        return services;
    }
}
