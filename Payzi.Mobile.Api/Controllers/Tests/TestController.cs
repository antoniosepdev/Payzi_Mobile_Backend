using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.Tests;
using Payzi.Mobile.Api.Services.Tests;

namespace Payzi.Mobile.Api.Controllers.Tests
{
    public class TestController : BaseController, ITest
    {
        private Payzi.Context.Context _context;

        public TestController(HttpContext httpContext, Payzi.Context.Context context)//Context context)
        : base(httpContext, context)
        {
            _context = context;
        }

        public async Task<IResult> Test(TestDTO testDTO)
        {
            try
            {
                await Task.Delay(1000);

                return Results.Ok(testDTO);
            }
            catch
            {
                await Task.Delay(1000);

                return Results.BadRequest(testDTO);
            }

        }
    }
}
