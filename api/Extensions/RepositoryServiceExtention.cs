using api.Repositories;
using api.Services;

namespace api.Extensions;

public static class RepositoryServiceExtention
{
    public static IServiceCollection AddRepositoryService(this IServiceCollection services)
    {
        #region Dependency Injections
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ITokenService, TokenService>();

        #endregion Dependency Injections

        return services;
    }
}
