using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class Persona : Payzi.Persistent.Persona
    {
        public static async Task<Persona> GetAsync(Payzi.Context.Context context, Guid id)
        {
            Payzi.Model.Persona query = await Query.GetPersonas(context).Include("Comuna").SingleOrDefaultAsync<Payzi.Model.Persona>(x => x.Id == id);

            Persona persona = query.SingleOrDefault<Persona>();

            return persona;
        }

        public static async Task<Persona> GetAsync(Payzi.Context.Context context, int rutCuerpo, string rutDigito)
        {
            Payzi.Model.Persona? query = await Query.GetPersonas(context).SingleOrDefaultAsync<Payzi.Model.Persona>(x => x.RutCuerpo == rutCuerpo && x.RutDigito.ToLower() == rutDigito.ToLower());

            Persona persona = query.SingleOrDefault<Persona>();

            return persona;
        }
    }
}
