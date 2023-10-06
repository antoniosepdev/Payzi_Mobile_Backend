using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class Pago : Payzi.Persistent.Pago
    {
        public static async Task<Pago> GetAsync(Payzi.Context.Context context, Guid id)
        {
            Payzi.Model.Pago query = await Query.GetPagos(context).SingleOrDefaultAsync<Payzi.Model.Pago>(x => x.Id == id);

            Pago pago = query.SingleOrDefault<Pago>();

            return pago;
        }
    }
}
