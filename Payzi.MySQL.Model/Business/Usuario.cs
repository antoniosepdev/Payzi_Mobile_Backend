using Dapper;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.MySQL.Model.Business
{
    public class Usuario : Payzi.MySQL.Model.Persistent.Usuario
    {
        public static async Task<Usuario> GetAsync(Payzi.MySQL.Model.Context context, string email)
        {
            Payzi.MySQL.Model.Usuario? user = await Query.GetUsuarios(context).Include("IdNavigation").Include("RolCodigoNavigation").SingleOrDefaultAsync<Payzi.MySQL.Model.Usuario>(x => x.Email == email);

            Payzi.MySQL.Model.Business.Usuario usuario = user.SingleOrDefault<Payzi.MySQL.Model.Business.Usuario>();

            return usuario;
        }

        public static async Task<List<Usuario>> GetAllAsync(Payzi.MySQL.Model.Context context, Guid aplicacionId)
        {
            IQueryable<Payzi.MySQL.Model.Usuario> query = (from q in Query.GetUsuarios(context) select q);

            List<Usuario> list = await query.ToList<Usuario>();

            return list;
        }
    }
}
