using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class FormaPago : Payzi.Entity.FormaPago, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.FormaPago? formaPago = await context.FormaPagos.SingleOrDefaultAsync<Payzi.Model.FormaPago>(x => x.Codigo == this.Codigo);

            if (formaPago == null)
            {
                formaPago = new Payzi.Model.FormaPago
                {
                    Codigo = this.Codigo,
                };

                context.FormaPagos.Add(formaPago);
            }

            formaPago.Nombre = this.Nombre;
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.FormaPago? formaPago = await context.FormaPagos.SingleOrDefaultAsync<Payzi.Model.FormaPago>(x => x.Codigo == this.Codigo);

            if (formaPago != null)
            {
                context.FormaPagos.Remove(formaPago);
            }
        }
    }
}
