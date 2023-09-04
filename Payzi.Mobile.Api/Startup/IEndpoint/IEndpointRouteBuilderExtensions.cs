using Payzi.Mobile.Api.Endpoints;

namespace Payzi.Mobile.Api.Startup.IEndpoint
{
    public static class IEndpointRouteBuilderExtensions
    {
        public static void MapEndpoints(this WebApplication builder)
        {
            IServiceScope scope = builder.Services.CreateScope();

            IEnumerable<Payzi.Mobile.Api.Endpoints.IEndpoint> endpoints = scope.ServiceProvider.GetServices<Payzi.Mobile.Api.Endpoints.IEndpoint>();

            foreach (Payzi.Mobile.Api.Endpoints.IEndpoint endpoint in endpoints)
            {
                endpoint.AddRoutes(builder);
            }
        }
    }
}
