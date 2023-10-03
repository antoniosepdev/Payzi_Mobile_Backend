using Payzi.MySQL.Data;

namespace Payzi.Mobile.Api.Controllers.Common
{
    public abstract class BaseController
    {
        private HttpContext _httpContext;

        private Payzi.MySQL.Model.Context _context;

        private MySQLConfiguration _connectionString;


        public BaseController(HttpContext httpContext, MySQLConfiguration connectionString)
        {
            this._httpContext = httpContext;

            this._connectionString = connectionString;
        }
    }
}
