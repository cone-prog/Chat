using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Conversations.CreateConversation;
using Chat.Domain.UseCase.Conversations.GetConversationById;
using Chat.Domain.UseCase.Conversations.Intentions;
using Chat.Domain.UseCase.Messages.CreateMessage;
using Chat.Domain.UseCase.Messages.Intentions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Domain.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateConversationUseCase, CreateConversationUseCase>()
            .AddScoped<IGetConversationByIdUseCase, GetConversationByIdUseCase>()
            .AddScoped<ICreateMessageUseCase, CreateMessageUseCase>();

        services.AddScoped<IIntentionResolver, ConversationIntentionResolver>()
            .AddScoped<IIntentionResolver, MessageIntentionResolver>()
            .AddScoped<IIntentionManager, IntentionManager>();

        services.AddScoped<IIdentityProvider, IdentityProvider>();

        services.AddValidatorsFromAssemblyContaining<Conversation>(includeInternalTypes: true);

        return services;
    }
}
