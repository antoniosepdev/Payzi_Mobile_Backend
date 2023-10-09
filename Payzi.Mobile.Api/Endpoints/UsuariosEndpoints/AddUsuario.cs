using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.UsuariosControllers;
using Payzi.Mobile.Api.DTO.UsuariosDTO;
using Payzi.Mobile.Api.Models.UsuariosModels;

namespace Payzi.Mobile.Api.Endpoints.UsuariosEndpoints
{
    public class AddUsuario : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/Usuario/Add", [Authorize] async (HttpContext httpContext, Payzi.Context.Context context, [FromBody] UsuarioDTO usuarioDTO) =>
            {
                UsuarioController usuarioController = new UsuarioController(httpContext, context);

                return await usuarioController.AddUser(usuarioDTO);

            }).Produces<AddUsuarioModel>(StatusCodes.Status200OK)
              .Produces<AddUsuarioModel>(StatusCodes.Status400BadRequest)
              .Produces<AddUsuarioModel>(StatusCodes.Status401Unauthorized);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
