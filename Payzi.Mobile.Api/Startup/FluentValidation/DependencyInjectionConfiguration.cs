using FluentValidation;
using Payzi.Mobile.Api.Startup.IEndpoint;

namespace Payzi.Mobile.Api.Startup.FluentValidation
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<Program>();

            services.AddEndpoints();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            return services;
        }
    }
}
