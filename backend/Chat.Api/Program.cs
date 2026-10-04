using System.Security.Principal;
using Chat.Api.Hubs;
using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.UseCase.Conversations.CreateConversation;
using Chat.Domain.UseCase.Conversations.Intentions;
using Chat.Infrastructure;
using Chat.Infrastructure.Storages;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddMemoryCache(options =>
    new MemoryCacheOptions());
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<IIntentionResolver, ConversationIntentionResolver>();
builder.Services.AddScoped<ICreateConversationStorage, CreateConversationStorage>();
builder.Services.AddScoped<IIntentionResolver, ConversationIntentionResolver>();
builder.Services.AddScoped<ICreateConversationUseCase, CreateConversationUseCase>();
builder.Services.AddScoped<IIntentionManager, IntentionManager>();
builder.Services.AddScoped<IIdentityProvider, IdentityProvider>();

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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.MapHub<ChatHub>("/chat");
app.UseHttpsRedirection();
app.Run();