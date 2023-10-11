using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.TransaccionControllers;
using Payzi.Mobile.Api.DTO.PagosDTO;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.Models.TransaccionModels;

namespace Payzi.Mobile.Api.Endpoints.TransferenciaEndpoints
{
    public class GetTransaccionEndpoint: IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/Transaccion/Get", [AllowAnonymous] async (HttpContext httpContext, Payzi.Context.Context context, [FromBody] GetTransaccionDTO getTransaccionDTO) =>
            {
                TransaccionController transaccionController = new TransaccionController(httpContext, context);

                return await transaccionController.GetTransaccion(getTransaccionDTO);

            }).Produces<GetTransaccionModel>(StatusCodes.Status200OK)
              .Produces<GetTransaccionModel>(StatusCodes.Status400BadRequest)
              .Produces<GetTransaccionModel>(StatusCodes.Status401Unauthorized)
              .Produces<GetTransaccionModel>(StatusCodes.Status403Forbidden)
              .Produces<GetTransaccionModel>(StatusCodes.Status423Locked)
              .Produces<GetTransaccionModel>(StatusCodes.Status500InternalServerError);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

    }
}
