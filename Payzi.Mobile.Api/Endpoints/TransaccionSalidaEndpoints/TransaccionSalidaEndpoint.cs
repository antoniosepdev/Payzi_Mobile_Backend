using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.TransaccionSalidaControllers;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.DTO.TransaccionSalidaDTO;
using Payzi.Mobile.Api.Models.TransaccionModels;
using Payzi.Mobile.Api.Models.TransaccionSalidaModels;

namespace Payzi.Mobile.Api.Endpoints.TransaccionSalidaEndpoints
{
    public class TransaccionSalidaEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/TransaccionSalida/Add", [AllowAnonymous] async (HttpContext httpContext, Payzi.Context.Context context, [FromBody] TransaccionSalidaDTO transaccionSalidaDTO) =>
            {
                TransaccionSalidaController transaccionSalidaController = new TransaccionSalidaController(httpContext, context);

                return await transaccionSalidaController.TransaccionSalida(transaccionSalidaDTO);

            }).Produces<TransaccionSalidaModel>(StatusCodes.Status200OK)
              .Produces<TransaccionSalidaModel>(StatusCodes.Status400BadRequest)
              .Produces<TransaccionSalidaModel>(StatusCodes.Status401Unauthorized);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
