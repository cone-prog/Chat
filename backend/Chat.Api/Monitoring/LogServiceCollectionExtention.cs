using Serilog;
using Serilog.Core;
using Serilog.Filters;
using Serilog.Sinks.Grafana.Loki;

namespace Chat.Api.Monitoring;

public static class LogServiceCollectionExtention
{
    public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var loggingLevelSwitch = new LoggingLevelSwitch();
        services.AddSingleton(loggingLevelSwitch);
        
        return services.AddLogging(b =>
            b.AddSerilog(
                new LoggerConfiguration()
                    .MinimumLevel.ControlledBy(loggingLevelSwitch)
                    .Enrich.WithProperty("Application", "Chat.Api")
                    .Enrich.WithProperty("Environment", environment.EnvironmentName)
                    .WriteTo.Logger(lc => lc
                        .Filter.ByExcluding(Matching.FromSource("Microsoft"))
                        .WriteTo.GrafanaLoki(
                            configuration.GetConnectionString("Logs"), 
                            propertiesAsLabels: new []
                            {
                                "level",
                                "Application",
                                "Environment",
                                "SourceContext"
                            }))
                    .WriteTo.Logger(lc => lc.WriteTo.Console())
                    .CreateLogger()));
    }
}