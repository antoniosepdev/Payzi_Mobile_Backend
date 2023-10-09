using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.PagosControllers;
using Payzi.Mobile.Api.DTO.PagosDTO;
using Payzi.Mobile.Api.Models.CustomFieldsModels;
using Payzi.Mobile.Api.Models.PagosModels;

namespace Payzi.Mobile.Api.Endpoints.PagosEndpoints
{
    public class RecepcionPagosEndpoint : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/Pagos/RecepcionPagos", [Authorize] async (HttpContext httpContext, Payzi.Context.Context context, [FromBody] RecepcionPagosDTO recepcionPagosDTO) =>
            {
                PagosController pagosController = new PagosController(httpContext, context);

                return await pagosController.RecepcionPago(recepcionPagosDTO);

            }).Produces<RecepcionPagosModel>(StatusCodes.Status200OK)
              .Produces<RecepcionPagosModel>(StatusCodes.Status400BadRequest)
              .Produces<RecepcionPagosModel>(StatusCodes.Status401Unauthorized);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
