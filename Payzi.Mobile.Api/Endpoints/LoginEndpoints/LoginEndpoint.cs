using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.LoginControllers;
using Payzi.Mobile.Api.DTO.LoginDTO;
using Payzi.Mobile.Api.Models.LoginModels;
using Payzi.Mobile.Api.Services.LoginServices;

namespace Payzi.Mobile.Api.Endpoints.LoginEndpoints
{
    public class LoginEndpoint : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/api/Login", [AllowAnonymous] async (HttpContext httpContext, Payzi.Context.Context context, [FromBody] LoginDTO loginDTO) =>
            {
                LoginController loginController = new LoginController(httpContext, context);

                return await loginController.Login(loginDTO);

            }).Produces<LoginModel>(StatusCodes.Status200OK)
              .Produces<LoginModel>(StatusCodes.Status400BadRequest)
              .Produces<LoginModel>(StatusCodes.Status401Unauthorized)
              .Produces<LoginModel>(StatusCodes.Status403Forbidden)
              .Produces<LoginModel>(StatusCodes.Status423Locked);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
