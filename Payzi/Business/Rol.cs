using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class Rol : Payzi.Persistent.Rol
    {
        public static async Task<Rol> GetAsync(Payzi.Context.Context context, int codigo)
        {
            Payzi.Model.Rol? query = await Query.GetRoles(context).SingleOrDefaultAsync<Payzi.Model.Rol>(x => x.Codigo == codigo);

            Rol rol = query.SingleOrDefault<Rol>();

            return rol;
        }

        public static async Task<Rol> GetAsync(Payzi.Context.Context context, string key)
        {
            Payzi.Model.Rol? query = await Query.GetRoles(context).SingleOrDefaultAsync<Payzi.Model.Rol>(x => x.Clave != null && x.Clave == key);

            Rol rol = query.SingleOrDefault<Rol>();

            return rol;
        }
    }
}
