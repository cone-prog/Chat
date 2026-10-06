using Chat.Api.Hubs;
using Chat.Api.Middlewares;
using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Conversations.CreateConversation;
using Chat.Domain.UseCase.Conversations.GetConversationById;
using Chat.Domain.UseCase.Conversations.Intentions;
using Chat.Infrastructure;
using Chat.Infrastructure.Storages;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Serilog.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(b =>
    b.AddSerilog(
        new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.WithProperty("Application", "Chat.Api")
    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
    .WriteTo.Logger(lc => lc
        .Filter.ByExcluding(Matching.FromSource("Microsoft"))
        .WriteTo.OpenSearch(
            builder.Configuration.GetConnectionString("Logs"),
            "chat-logs-{0:yyyy.MM.dd}"))
    .WriteTo.Logger(lc => lc.WriteTo.Console())
    .CreateLogger()));

builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddMemoryCache(options =>
    new MemoryCacheOptions());
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<IIntentionResolver, ConversationIntentionResolver>();
builder.Services.AddScoped<ICreateConversationStorage, CreateConversationStorage>();
builder.Services.AddScoped<IIntentionResolver, ConversationIntentionResolver>();
builder.Services.AddScoped<ICreateConversationUseCase, CreateConversationUseCase>();
builder.Services.AddScoped<IIntentionManager, IntentionManager>();
builder.Services.AddScoped<IIdentityProvider, IdentityProvider>();
builder.Services.AddScoped<IGetConversationByIdUseCase, GetConversationByIdUseCase>();
builder.Services.AddScoped<IGetConversationByIdStorage, GetConversationByIdStorage>();
builder.Services.AddScoped<IIdentityProvider, IdentityProvider>();
builder.Services.AddValidatorsFromAssemblyContaining<Conversation>();

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
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.MapHub<ChatHub>("/chat");
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.Run();