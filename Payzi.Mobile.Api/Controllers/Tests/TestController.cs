using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.Tests;
using Payzi.Mobile.Api.Services.Tests;

namespace Payzi.Mobile.Api.Controllers.Tests
{
    public class TestController : BaseController, ITest
    {
        public TestController(HttpContext httpContext)//Context context)
        : base(httpContext)//, context)
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
