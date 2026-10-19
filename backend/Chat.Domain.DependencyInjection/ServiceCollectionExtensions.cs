using System.Reflection;
using Chat.Domain.Authentication;
using Chat.Domain.Authorization;
using Chat.Domain.Behaviors;
using Chat.Domain.Entities;
using Chat.Domain.Monitoring;
using Chat.Domain.UseCase.Conversations.Intentions;
using Chat.Domain.UseCase.Messages.Intentions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Domain.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddMediatR(cnf =>
            cnf.AddOpenBehavior(typeof(ValidationBehavior<,>))
                .AddOpenBehavior(typeof(MonitoringBehavior<,>))
                .RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(Conversation))!));
        
        services
            .AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<IPasswordManager, PasswordManager>()
            .AddScoped<ISymmetricDecryptor, AesSymmetricEncryptorDecryptor>()
            .AddScoped<ISymmetricEncryptor, AesSymmetricEncryptorDecryptor>();

        services.AddScoped<IIntentionResolver, ConversationIntentionResolver>()
            .AddScoped<IIntentionResolver, MessageIntentionResolver>()
            .AddScoped<IIntentionManager, IntentionManager>();

        services.AddScoped<IIdentityProvider, IdentityProvider>();

        services.AddSingleton<DomainMetrics>();
        
        services.AddValidatorsFromAssemblyContaining<Conversation>(includeInternalTypes: true);

        return services;
    }
}
