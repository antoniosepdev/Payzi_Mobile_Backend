using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Business;

namespace Payzi.Mobile.Api.Endpoints.LoginEndpoints
{
    public class GetEmailFromTokenEndpoint : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/GetEmailFromToken/{token}", [Authorize] async (Payzi.Context.Context context, [FromRoute] string token) => {

                try
                {
                    if (string.IsNullOrEmpty(token))
                    {
                        return Results.Problem("Token error: Token no ingresado o vacio.");
                    }

                    var email = AccessToken.GetEmailFromToken(token);

                    if (string.IsNullOrEmpty(email))
                    {
                        return Results.Problem("Token error: Email no encontrado.");
                    }

                    return Results.Ok(email);
                }
                catch
                {
                    return Results.Problem("Email error: Problema interno.");
                }
            }).Produces<string>(StatusCodes.Status200OK)
              .Produces<string>(StatusCodes.Status500InternalServerError);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
