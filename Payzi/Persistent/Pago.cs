using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class Pago : Payzi.Entity.Pago, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.Pago? pago = await context.Pagos.SingleOrDefaultAsync<Payzi.Model.Pago>(x => x.IdPago == this.IdPago);

            if (pago == null)
            {
                pago = new Pago
                {
                    IdPago = this.IdPago
                };

                await context.Pagos.AddAsync(pago);
            }

            pago.IdTransaccion = this.IdTransaccion;
            pago.IdUsuario = this.IdUsuario;
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.Pago? pago = await context.Pagos.SingleOrDefaultAsync<Payzi.Model.Pago>(x => x.IdPago == this.IdPago);

            if (pago != null)
            {
                context.Pagos.Remove(pago);
            }
        }
    }
}
