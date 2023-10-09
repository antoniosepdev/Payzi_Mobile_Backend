using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.CustomFieldsControllers;
using Payzi.Mobile.Api.DTO.CustomFieldsDTO;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.Models.CustomFieldsModels;
using Payzi.Mobile.Api.Models.TransaccionModels;

namespace Payzi.Mobile.Api.Endpoints.CustomFieldsEndpoints
{
    public class AddCustomFieldsEndpoint : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/CustomFields/Add", [Authorize] async (HttpContext httpContext, Payzi.Context.Context context, [FromBody] CustomFieldsDTO customFieldsDTO ) =>
            {
                CustomFieldsController customFieldsController = new CustomFieldsController(httpContext, context);

                return await customFieldsController.AddCustomFields(customFieldsDTO);

            }).Produces<AddCustomFieldsModel>(StatusCodes.Status200OK)
              .Produces<AddCustomFieldsModel>(StatusCodes.Status400BadRequest)
              .Produces<AddCustomFieldsModel>(StatusCodes.Status401Unauthorized);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
