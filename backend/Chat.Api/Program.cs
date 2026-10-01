using Chat.Api.Hubs;
using Chat.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddMemoryCache(options =>
    new MemoryCacheOptions());
builder.Services.AddInfrastructureServices(builder.Configuration);

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