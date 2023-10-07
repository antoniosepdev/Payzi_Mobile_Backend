using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class Transaccion : Payzi.Persistent.Transaccion
    {
        public static async Task<Transaccion> GetAsync(Payzi.Context.Context context, Guid idTransaccion)
        {
            Payzi.Model.Transaccion query = await Query.GetTransacciones(context).SingleOrDefaultAsync<Payzi.Model.Transaccion>(x => x.IdTransaccion == idTransaccion);

            Transaccion transaccion = query.SingleOrDefault<Transaccion>();

            return transaccion;
        }
    }
}
