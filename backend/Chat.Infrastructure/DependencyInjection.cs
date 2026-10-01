using Chat.Domain.Data.DbContext;
using Chat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IChatDbContext, ChatDbContext>(option =>
        {
            option.UseNpgsql(configuration.GetConnectionString("PgConnection"));
        });
        return services;
    }
}
