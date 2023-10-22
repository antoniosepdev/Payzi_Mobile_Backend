using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Context;
using Payzi.Mobile.Api.Controllers.TransaccionControllers;
using Payzi.Mobile.Api.DTO.PagosDTO;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.Models.TransaccionModels;

namespace Payzi.Mobile.Api.Endpoints.TransferenciaEndpoints
{
    public class GetTransaccion2Endpoint : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/transaccion/Get/{id}", [AllowAnonymous] async (HttpContext httpContext, Payzi.Context.Context context, [FromRoute] string id) =>
            {
                TransaccionController transaccionController = new TransaccionController(httpContext, context);

                return await transaccionController.GetTransaccion2(Guid.Parse(id));

            }).Produces<GetTransaccionModel>(StatusCodes.Status200OK)
              .Produces<GetTransaccionModel>(StatusCodes.Status400BadRequest)
              .Produces<GetTransaccionModel>(StatusCodes.Status401Unauthorized)
              .Produces<GetTransaccionModel>(StatusCodes.Status403Forbidden)
              .Produces<GetTransaccionModel>(StatusCodes.Status423Locked);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
