using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.PersonasControllers;
using Payzi.Mobile.Api.DTO.PersonasDTO;
using Payzi.Mobile.Api.DTO.UsuariosDTO;
using Payzi.MySQL.Data;

namespace Payzi.Mobile.Api.Endpoints.PersonasEndpoints
{
    public class AddPersona : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/Persona/Add", [AllowAnonymous] async (HttpContext httpContext, MySQLConfiguration connectionString, [FromBody] PersonaDTO personaDTO) =>
            {
                PersonaController personaController = new PersonaController(httpContext, connectionString);

                return await personaController.AddPerson(personaDTO);

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
