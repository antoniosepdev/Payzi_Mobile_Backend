using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class FormaPago : Payzi.Persistent.FormaPago
    {
        public static async Task<FormaPago> GetAsync(Payzi.Context.Context context, int codigo)
        {
            Payzi.Model.FormaPago query = await Query.GetFormaPagos(context).SingleOrDefaultAsync<Payzi.Model.FormaPago>(x => x.Codigo == codigo);

            FormaPago formaPago = query.SingleOrDefault<FormaPago>();

            return formaPago;
        }
    }
}
