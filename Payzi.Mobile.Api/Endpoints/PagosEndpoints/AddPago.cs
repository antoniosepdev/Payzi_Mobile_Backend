using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.PagosControllers;
using Payzi.Mobile.Api.DTO.CustomFieldsDTO;
using Payzi.Mobile.Api.DTO.PagosDTO;
using Payzi.Mobile.Api.Models.CustomFieldsModels;

namespace Payzi.Mobile.Api.Endpoints.PagosEndpoints
{
    public class AddPago : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/Pagos/Add", [Authorize] async (HttpContext httpContext, Payzi.Context.Context context, [FromBody] PagosDTO pagosDTO) =>
            {
                PagosController pagosController = new PagosController(httpContext, context);

                return await pagosController.AddPagos(pagosDTO);

            }).Produces<AddCustomFieldsModel>(StatusCodes.Status200OK)
              .Produces<AddCustomFieldsModel>(StatusCodes.Status400BadRequest)
              .Produces<AddCustomFieldsModel>(StatusCodes.Status401Unauthorized);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
