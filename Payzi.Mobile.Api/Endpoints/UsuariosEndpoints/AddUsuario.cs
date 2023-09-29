using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.UsuariosControllers;
using Payzi.Mobile.Api.DTO.UsuariosDTO;
using Payzi.MySQL.Data;

namespace Payzi.Mobile.Api.Endpoints.UsuariosEndpoints
{
    public class AddUsuario : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/Usuario/Add", [AllowAnonymous] async (HttpContext httpContext, MySQLConfiguration connectionString, [FromBody] UsuarioDTO usuarioDTO) =>
            {
                UsuarioController usuarioController = new UsuarioController(httpContext, connectionString);

                return await usuarioController.AddUser(usuarioDTO);

            }).Produces<UsuarioDTO>(StatusCodes.Status200OK)
              .Produces<UsuarioDTO>(StatusCodes.Status400BadRequest)
              .Produces<UsuarioDTO>(StatusCodes.Status401Unauthorized);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
