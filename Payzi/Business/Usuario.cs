using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class Usuario : Payzi.Persistent.Usuario
    {

        public static Usuario Get(Payzi.Context.Context context, string email)
        {
            Payzi.Model.Usuario? user = Query.GetUsuarios(context)
                                                         .Include("Negocio")
                                                         .SingleOrDefault<Payzi.Model.Usuario>(x => x.Email == email);

            Usuario usuario = user.SingleOrDefault<Usuario>();

            return usuario;
        }

        public static async Task<Usuario> GetAsync(Payzi.Context.Context context, Guid id)
        {
            Payzi.Model.Usuario? user = await Query.GetUsuarios(context).Include("Negocio").Include("RolCodigoNavigation").SingleOrDefaultAsync<Payzi.Model.Usuario>(x => x.Id == id);

            Payzi.Business.Usuario usuario = user.SingleOrDefault<Payzi.Business.Usuario>();

            return usuario;
        }


        public static async Task<Usuario> GetAsync(Payzi.Context.Context context, string email)
        {
            Payzi.Model.Usuario? user = await Query.GetUsuarios(context).Include("Negocio").Include("RolCodigoNavigation").SingleOrDefaultAsync<Payzi.Model.Usuario>(x => x.Email == email);

            Payzi.Business.Usuario usuario = user.SingleOrDefault<Payzi.Business.Usuario>();

            return usuario;
        }

        public static async Task<List<Usuario>> GetAllAsync(Payzi.Context.Context context, Guid aplicacionId)
        {
            IQueryable<Payzi.Model.Usuario> query = (from q in Query.GetUsuarios(context) select q);

            List<Usuario> list = await query.ToList<Usuario>();

            return list;
        }
    }
}
