using System.Reflection;
using Chat.Api.Authentication;
using Chat.Api.Hubs;
using Chat.Api.Middlewares;
using Chat.Api.Monitoring;
using Chat.Domain.Authentication;
using Chat.Domain.DependencyInjection;
using Chat.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Serilog.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<AuthenticationConfiguration>(builder.Configuration.GetSection("Authentication").Bind);
builder.Services.AddOpenApi();
builder.Services.AddScoped<IAuthTokenStorage, AuthTokenStorage>();
builder.Services.AddSignalR();

builder.Services
    .AddApiMetrics(builder.Configuration)
    .AddLogging(builder.Configuration, builder.Environment);

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthTokenStorage, AuthTokenStorage>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(config => config
    .AddMaps(Assembly.GetAssembly(typeof(Program))));

builder.Services.AddDomainServices();

builder.Services.AddMemoryCache(options =>
    new MemoryCacheOptions());

builder.Services.AddInfrastructureServices(builder.Configuration.GetConnectionString("PgConnection")!);


builder.Services.AddCors(option =>
        option.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            }
        )
    );

var app = builder.Build();

app.MapControllers();
app.MapHub<ChatHub>("/chat");
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.Run();