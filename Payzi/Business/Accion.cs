using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class Accion : Payzi.Entity.Accion
    {
        public static async Task<Accion> GetAsync(Int32 codigo, Payzi.Context.Context context)
        {

            Payzi.Model.Accion? query = await Query.GetAcciones(context).SingleOrDefaultAsync<Payzi.Model.Accion>(x => x.Codigo == codigo);

            Accion accion = query.SingleOrDefault<Accion>();

            return accion;

        }
    }
}
