using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.Tests;
using Payzi.Mobile.Api.DTO.Tests;


namespace Payzi.Mobile.Api.Endpoints.Tests
{
    public class TestDeleteEndpoint : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapDelete("/api/Test/Delete", [AllowAnonymous] async (HttpContext httpContext, Payzi.Context.Context context, [FromBody] TestDTO testDTO) =>
            {
                TestController testController = new TestController(httpContext, context);

                return await testController.Test(testDTO);

            }).Produces<TestDTO>(StatusCodes.Status200OK);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
