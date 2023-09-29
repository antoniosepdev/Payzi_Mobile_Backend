using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.NegociosControllers;
using Payzi.Mobile.Api.Controllers.PersonasControllers;
using Payzi.Mobile.Api.DTO.NegociosDTO;
using Payzi.Mobile.Api.Models.NegociosModels;
using Payzi.MySQL.Data;

namespace Payzi.Mobile.Api.Endpoints.PersonasEndpoints
{
    public class AddNegocio : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/Negocio/Add", [AllowAnonymous] async (HttpContext httpContext, MySQLConfiguration connectionString, [FromBody] NegocioDTO negocioDTO) =>
            {
                NegocioController negocioController = new NegocioController(httpContext, connectionString);

                return await negocioController.AddNegocio(negocioDTO);

            }).Produces<NegocioModel>(StatusCodes.Status200OK)
              .Produces<NegocioModel>(StatusCodes.Status400BadRequest)
              .Produces<NegocioModel>(StatusCodes.Status401Unauthorized);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }

    }
}
