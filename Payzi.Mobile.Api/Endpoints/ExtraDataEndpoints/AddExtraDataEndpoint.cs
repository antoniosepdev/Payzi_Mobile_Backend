using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.ExtraDataControllers;
using Payzi.Mobile.Api.DTO.ExtraDataDTO;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.Models.ExtraDataModels;
using Payzi.Mobile.Api.Models.TransaccionModels;

namespace Payzi.Mobile.Api.Endpoints.ExtraDataEndpoints
{
    public class AddExtraDataEndpoint : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/ExtraData/Add", [AllowAnonymous] async (HttpContext httpContext, Payzi.Context.Context context, [FromBody] ExtraDataDTO extraDataDTO) =>
            {
                ExtraDataController extraDataController = new ExtraDataController(httpContext, context);

                return await extraDataController.AddExtraData(extraDataDTO);

            }).Produces<AddExtraDataModel>(StatusCodes.Status200OK)
              .Produces<AddExtraDataModel>(StatusCodes.Status400BadRequest)
              .Produces<AddExtraDataModel>(StatusCodes.Status401Unauthorized);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
