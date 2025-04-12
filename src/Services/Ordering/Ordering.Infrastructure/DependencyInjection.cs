using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DependencyInjection>());
            //services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<DependencyInjection>());
            var connectionString = configuration.GetConnectionString("Database");

            return services;
        }
    }

}
