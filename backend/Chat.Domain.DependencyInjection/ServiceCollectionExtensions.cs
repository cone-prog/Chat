using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Entities;
using Chat.Domain.UseCase.Conversations.CreateConversation;
using Chat.Domain.UseCase.Conversations.GetConversationById;
using Chat.Domain.UseCase.Conversations.Intentions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Domain.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateConversationUseCase, CreateConversationUseCase>()
            .AddScoped<IGetConversationByIdUseCase, GetConversationByIdUseCase>();

        services.AddScoped<IIntentionResolver, ConversationIntentionResolver>()
            .AddScoped<IIntentionManager, IntentionManager>();

        services.AddScoped<IIdentityProvider, IdentityProvider>();

        services.AddValidatorsFromAssemblyContaining<Conversation>();

        return services;
    }
}
