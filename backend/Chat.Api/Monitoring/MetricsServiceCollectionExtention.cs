using System.Diagnostics;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Chat.Api.Monitoring;

public static class MetricsServiceCollectionExtentions
{
    public static IServiceCollection AddApiMetrics(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenTelemetry()
            .WithMetrics(builder => builder
                .AddAspNetCoreInstrumentation()
                .AddMeter("Chat.Domain")
                .AddPrometheusExporter()
                .AddView("http.server.request.duration", new ExplicitBucketHistogramConfiguration
                {
                    Boundaries = new double[] { 0, 0.05, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 10 }
                }))
            .WithTracing(builder => builder
                .ConfigureResource(r => r.AddService("Chat"))
                .AddSource("Chat.Domain")
                .AddAspNetCoreInstrumentation(cfg =>
                    {
                        cfg.Filter += context =>
                            !context.Request.Path.Value!.Contains("metrics",
                                StringComparison.InvariantCultureIgnoreCase);
                        cfg.EnrichWithHttpResponse = (activity, response) =>
                        {
                            if (response.StatusCode >= 400)
                            {
                                activity.SetStatus(ActivityStatusCode.Error);
                                activity.SetTag("error", true);
                            }
                            else  activity.SetTag("error", false);
                            
                        };
                    })
                    .AddEntityFrameworkCoreInstrumentation()
                .AddOtlpExporter(cfg =>
                    {
                        cfg.Endpoint = new Uri(configuration.GetConnectionString("Tracing")!);
                        cfg.Protocol = OtlpExportProtocol.Grpc;
                    }
                ));

        return services;
    }
}
