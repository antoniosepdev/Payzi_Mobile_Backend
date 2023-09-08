using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.Usuarios;
using Payzi.Mobile.Api.DTO.Tests;
using Payzi.Mobile.Api.DTO.Usuarios;
using Payzi.MySQL.Data;

namespace Payzi.Mobile.Api.Endpoints.Tests
{
    public class AddUsuario : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/Usuario/Add", [AllowAnonymous] async (HttpContext httpContext, MySQLConfiguration connectionString, [FromBody] UsuarioDTO usuarioDTO) =>
            {
                Usuario usuarioController = new Usuario(httpContext, connectionString);

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
