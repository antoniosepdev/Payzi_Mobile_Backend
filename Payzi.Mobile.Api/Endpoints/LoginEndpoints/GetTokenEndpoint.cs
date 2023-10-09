using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Models.LoginModels;

namespace Payzi.Mobile.Api.Endpoints.LoginEndpoints
{
    public class GetTokenEndpoint : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/GetToken/{email}", [AllowAnonymous] async (Payzi.Context.Context context, [FromRoute] string email) => {

                try
                {
                    Payzi.Business.Usuario user = await Payzi.Business.Usuario.GetAsync(context, email);

                    if (user != null)
                    {
                        string accessToken = Payzi.Business.AccessToken.GenerateAccessToken(user);

                        return Results.Ok(accessToken);
                    }
                    else
                    {
                        throw new Exception("Email error: El correo electrónico retornado es vacio.");
                    }
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
