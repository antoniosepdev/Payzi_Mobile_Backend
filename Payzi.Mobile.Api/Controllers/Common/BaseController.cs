using Payzi;

namespace Payzi.Mobile.Api.Controllers.Common
{
    public abstract class BaseController
    {
        private HttpContext _httpContext;

        private Payzi.Context.Context _context;

        public BaseController(HttpContext httpContext, Payzi.Context.Context context)
        {
            this._httpContext = httpContext;

            this._context = context;
        }
    }
}
