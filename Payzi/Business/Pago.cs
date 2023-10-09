﻿using Microsoft.EntityFrameworkCore;
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
        public static async Task<Pago> GetAsync(Payzi.Context.Context context, Guid idPago)
        {
            Payzi.Model.Pago query = await Query.GetPagos(context).Include("Usuario").Include("IdTransaccionNavigation").SingleOrDefaultAsync<Payzi.Model.Pago>(x => x.IdPago == idPago);

            Pago pago = query.SingleOrDefault<Pago>();

            return pago;
        }
    }
}
