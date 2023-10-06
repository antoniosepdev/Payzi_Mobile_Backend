using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class Transaccion : Payzi.Entity.Transaccion, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.Transaccion? transaccion = await context.Transaccions.SingleOrDefaultAsync<Payzi.Model.Transaccion>(x => x.IdTransaccion == this.IdTransaccion);

            if (transaccion == null)
            {
                transaccion = new Transaccion
                {
                    IdTransaccion = this.IdTransaccion
                };

                await context.Transaccions.AddAsync(transaccion);
            }

            transaccion.Amount = this.Amount;
            transaccion.Tip = this.Tip;
            transaccion.Cashback = this.Cashback;
            transaccion.Method = this.Method;
            transaccion.InstallmentsQuantity = this.InstallmentsQuantity;
            transaccion.PrintVoucherOnApp = this.PrintVoucherOnApp;
            transaccion.ExtraData = this.ExtraData;
            transaccion.VoucherId = this.VoucherId;
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.Transaccion? transaccion = await context.Transaccions.SingleOrDefaultAsync<Payzi.Model.Transaccion>(x => x.IdTransaccion == this.IdTransaccion);

            if (transaccion != null)
            {
                context.Transaccions.Remove(transaccion);
            }
        }
    }
}
