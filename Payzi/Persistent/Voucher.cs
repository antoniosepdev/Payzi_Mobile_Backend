using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class Voucher : Payzi.Entity.Voucher, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.Voucher? voucher = await context.Vouchers.SingleOrDefaultAsync<Payzi.Model.Voucher>(x => x.Id == this.Id);

            if (voucher == null)
            {
                voucher = new Voucher
                {
                    Id = this.Id
                };

                await context.Vouchers.AddAsync(voucher);
            }

            voucher.NombreCliente = this.NombreCliente;
            voucher.NumeroDocumento = this.NumeroDocumento;
            voucher.Monto = this.Monto;
            voucher.FechaEmision = this.FechaEmision;
            voucher.Descripcion = this.Descripcion;
            voucher.MetodoPagoCodigo = this.MetodoPagoCodigo;
            voucher.NumeroTransaccion = this.NumeroTransaccion;
            voucher.UsuarioId = this.UsuarioId;
            voucher.Estado = this.Estado;          
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.Voucher? voucher = await context.Vouchers.SingleOrDefaultAsync<Payzi.Model.Voucher>(x => x.Id == this.Id);

            if (voucher != null)
            {
                context.Vouchers.Remove(voucher);
            }
        }
    }
}
