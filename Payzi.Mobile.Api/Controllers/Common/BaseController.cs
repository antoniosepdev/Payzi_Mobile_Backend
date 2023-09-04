namespace Payzi.Mobile.Api.Controllers.Common
{
    public abstract class BaseController
    {
        private HttpContext _httpContext;

        public BaseController(HttpContext httpContext)
        {
            this._httpContext = httpContext;
        }
    }
}
