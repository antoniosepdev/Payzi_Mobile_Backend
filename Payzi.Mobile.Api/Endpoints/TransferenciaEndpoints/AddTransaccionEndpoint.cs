using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.TransaccionControllers;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.DTO.UsuariosDTO;
using Payzi.Mobile.Api.Models.TransaccionModels;

namespace Payzi.Mobile.Api.Endpoints.TransferenciaEndpoints
{
    public class AddTransaccionEndpoint : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/Transferencia/Add", [AllowAnonymous] async (HttpContext httpContext, Payzi.Context.Context context, [FromBody] TransaccionDTO transaccionDTO) =>
            {
                TransaccionController transaccionController = new TransaccionController(httpContext, context);

                return await transaccionController.AddTransaccion(transaccionDTO);

            }).Produces<AddTransaccionModel>(StatusCodes.Status200OK)
              .Produces<AddTransaccionModel>(StatusCodes.Status400BadRequest)
              .Produces<AddTransaccionModel>(StatusCodes.Status401Unauthorized);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
