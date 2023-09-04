namespace Payzi.Mobile.Api.Startup.IEndpoint
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            IEnumerable<Type> endpoints = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                                                                                 .Where(t => t.GetInterfaces().Contains(typeof(Payzi.Mobile.Api.Endpoints.IEndpoint)))
                                                                                 .Where(t => !t.IsInterface);

            foreach (Type endpoint in endpoints)
            {
                services.AddScoped(typeof(Payzi.Mobile.Api.Endpoints.IEndpoint), endpoint);
            }

            return services;
        }

    }
}
