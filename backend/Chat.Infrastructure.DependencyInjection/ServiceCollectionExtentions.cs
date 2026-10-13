using System.Reflection;
using Chat.Domain.UseCase.Conversations.CreateConversation;
using Chat.Domain.UseCase.Conversations.GetConversationById;
using Chat.Domain.UseCase.Conversations.GetConversations;
using Chat.Domain.UseCase.Messages.CreateMessage;
using Chat.Domain.UseCase.Users.SignIn;
using Chat.Domain.UseCase.Users.SignOn;
using Chat.Infrastructure.Data;
using Chat.Infrastructure.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        string connectionString)
    {

        services.AddScoped<ICreateConversationStorage, CreateConversationStorage>()
            .AddScoped<IGetConversationByIdStorage, GetConversationByIdStorage>()
            .AddScoped<ICreateMessageStorage, CreateMessageStorage>()
            .AddScoped<IGetConversationsStorage, GetConversationsStorage>()
            .AddScoped<ISignOnStorage, SignOnStorage>()
            .AddScoped<ISignInStorage, SignInStorage>();

        services.AddAutoMapper(config => config.AddMaps(Assembly.GetAssembly(typeof(ChatDbContext))));

        services.AddDbContext<ChatDbContext>(option =>
        {
            option.UseNpgsql(connectionString);
        });
        return services;

    }
}
