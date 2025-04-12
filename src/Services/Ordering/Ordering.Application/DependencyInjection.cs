
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DependencyInjection>());
        //services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<DependencyInjection>());
        return services;
    }
}
