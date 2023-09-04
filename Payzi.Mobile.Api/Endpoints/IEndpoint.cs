namespace Payzi.Mobile.Api.Endpoints
{
    public interface IEndpoint
    {
        IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints);

        IServiceCollection RegisterModule(IServiceCollection builder);
    }
}
