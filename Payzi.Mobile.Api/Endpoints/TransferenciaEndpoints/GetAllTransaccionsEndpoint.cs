using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Context;
using Payzi.Mobile.Api.Controllers.TransaccionControllers;
using Payzi.Mobile.Api.DTO.PagosDTO;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.Models.TransaccionModels;

namespace Payzi.Mobile.Api.Endpoints.TransferenciaEndpoints
{
    public class GetAllTransaccionsEndpoint : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/transaccion/GetAll/{email}", [Authorize] async (HttpContext httpContext, Payzi.Context.Context context, [FromRoute] string email) =>
            {
                Payzi.Business.Usuario user = await Payzi.Business.Usuario.GetAsync(context, email);

                if(user != null)
                {
                    TransaccionController transaccionController = new TransaccionController(httpContext, context);

                    return await transaccionController.GetAllTransaccion(email);
                }
                else
                {
                    throw new Exception("Email error: El correo electrónico retornado es vacio.");
                }


            }).Produces<GetTransaccionModel>(StatusCodes.Status200OK)
              .Produces<GetTransaccionModel>(StatusCodes.Status400BadRequest)
              .Produces<GetTransaccionModel>(StatusCodes.Status401Unauthorized)
              .Produces<GetTransaccionModel>(StatusCodes.Status403Forbidden)
              .Produces<GetTransaccionModel>(StatusCodes.Status423Locked);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }

    }
}
