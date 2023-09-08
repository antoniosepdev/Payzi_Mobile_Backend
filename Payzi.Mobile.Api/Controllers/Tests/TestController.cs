using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.Tests;
using Payzi.Mobile.Api.Services.Tests;
using Payzi.MySQL.Data;

namespace Payzi.Mobile.Api.Controllers.Tests
{
    public class TestController : BaseController, ITest
    {
        public TestController(HttpContext httpContext, MySQLConfiguration connectionString)//Context context)
        : base(httpContext, connectionString)
        {
            //_remunerationsContext = remunerationsContext;
        }

        public async Task<IResult> Test(TestDTO testDTO)
        {
            try
            {
                return Results.Ok(testDTO);
            }
            catch
            {
                return Results.BadRequest(testDTO);
            }

        }
    }
}
