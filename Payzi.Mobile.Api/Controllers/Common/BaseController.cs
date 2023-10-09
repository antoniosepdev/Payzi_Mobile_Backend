using Mapster;
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
        public Payzi.Business.Persona CurrentPerson
        {

            get
            {
                return this.CurrentUser().Negocio.Dueno.Adapt<Payzi.Business.Persona>();
            }
        }

        public Payzi.Business.Negocio CurrentCommerce
        {

            get
            {
                return this.CurrentUser().Negocio.Adapt<Payzi.Business.Negocio>();
            }
        }

        public Payzi.Business.Usuario CurrentUser()
        {
            string email = this._httpContext.User.Claims.First(claim => claim.Type == Payzi.Enumerate.EnumerateClaims.Email.ToString()).Value;

            try
            {
                this._context.ConfigureAwait(true);

                return Payzi.Business.Usuario.Get(this._context, email);

            }
            catch
            {
                return null;
            }
        }


        //public async Task<Payzi.Business.Usuario> CurrentUser()
        //{
        //    string email = this._httpContext.User.Claims.First(claim => claim.Type == Payzi.Enumerate.EnumerateClaims.Email.ToString()).Value;

        //    try
        //    {
        //        this._context.ConfigureAwait(true);

        //        return await Payzi.Business.Usuario.GetAsync(this._context, email);

        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

    }
}
