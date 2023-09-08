using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.Tests;
using Payzi.Mobile.Api.DTO.Tests;
using Payzi.MySQL.Data;

namespace Payzi.Mobile.Api.Endpoints.Tests
{
    public class TestEndpoint : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/api/Test", [AllowAnonymous] async (HttpContext httpContext, MySQLConfiguration connectionString, [FromBody] TestDTO testDTO) =>
            {
                TestController testController = new TestController(httpContext, connectionString);

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
