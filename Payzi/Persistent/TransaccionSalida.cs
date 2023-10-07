using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class TransaccionSalida : Payzi.Entity.TransaccionSalida, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.TransaccionSalidum? transaccionSalida = await context.TransaccionSalida.SingleOrDefaultAsync<Payzi.Model.TransaccionSalidum>(x => x.Id == this.Id);

            if (transaccionSalida == null)
            {
                transaccionSalida = new TransaccionSalida
                {
                    Id = this.Id
                };

                await context.TransaccionSalida.AddAsync(transaccionSalida);
            }

            transaccionSalida.TransactionStatus = this.TransactionStatus;
            transaccionSalida.SequenceNumber = this.SequenceNumber;
            transaccionSalida.PrinterVoucherCommerce = this.PrinterVoucherCommerce;
            transaccionSalida.ExtraData = this.ExtraData;
            transaccionSalida.TransactionTip = this.TransactionTip;
            transaccionSalida.TransactionCashback = this.TransactionCashback;
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.TransaccionSalidum? transaccionSalida = await context.TransaccionSalida.SingleOrDefaultAsync<Payzi.Model.TransaccionSalidum>(x => x.Id == this.Id);

            if (transaccionSalida != null)
            {
                context.TransaccionSalida.Remove(transaccionSalida);
            }
        }
    }
}
