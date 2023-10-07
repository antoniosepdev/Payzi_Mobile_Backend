using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using Payzi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class TransaccionSalida : Payzi.Persistent.TransaccionSalida
    {
        public static async Task<TransaccionSalida> GetAsync(Payzi.Context.Context context, Guid id)
        {
            Payzi.Model.TransaccionSalidum query = await Query.GetTransaccionesSalida(context).SingleOrDefaultAsync<Payzi.Model.TransaccionSalidum>(x => x.Id == id);

            TransaccionSalida transaccionSalida = query.SingleOrDefault<TransaccionSalida>();

            return transaccionSalida;
        }
    }
}
