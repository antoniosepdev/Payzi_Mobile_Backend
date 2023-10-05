using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class Negocio : Payzi.Persistent.Negocio
    {
        public static async Task<Negocio> GetAsync(Payzi.Context.Context context, Guid id)
        {
            Payzi.Model.Negocio query = await Query.GetNegocios(context).Include("Dueno").SingleOrDefaultAsync<Payzi.Model.Negocio>(x => x.Id == id);

            Negocio negocio = query.SingleOrDefault<Negocio>();

            return negocio;
        }

        public static async Task<Negocio> GetAsync(Payzi.Context.Context context, string rut)
        {
            Payzi.Model.Negocio? query = await Query.GetNegocios(context).Include("Dueno").SingleOrDefaultAsync<Payzi.Model.Negocio>(x => x.Rut == rut);

            Negocio negocio = query.SingleOrDefault<Negocio>();

            return negocio;
        }

        public static async Task<Negocio> GetAsync(Payzi.Context.Context context, int rutCuerpo, string rutDigito)
        {
            Payzi.Model.Negocio? query = await Query.GetNegocios(context).Include("Dueno").SingleOrDefaultAsync<Payzi.Model.Negocio>(x => x.RutCuerpo == rutCuerpo && x.RutDigito.ToLower() == rutDigito.ToLower());

            Negocio persona = query.SingleOrDefault<Negocio>();

            return persona;
        }
    }
}
