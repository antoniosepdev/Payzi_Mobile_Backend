using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class Cantidad : Payzi.Entity.Cantidad, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.Cantidad? cantidad = await context.Cantidads.SingleOrDefaultAsync<Payzi.Model.Cantidad>(x => x.IdUsuario == this.IdUsuario);

            if (cantidad == null)
            {
                cantidad = new Payzi.Model.Cantidad
                {
                    IdUsuario = this.IdUsuario,
                };

                context.Cantidads.Add(cantidad);
            }

            cantidad.IdUsuario = this.IdUsuario;
            cantidad.Cantidad1 = this.Cantidad1;
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.Cantidad? cantidad = await context.Cantidads.SingleOrDefaultAsync<Payzi.Model.Cantidad>(x => x.IdUsuario == this.IdUsuario);

            if (cantidad != null)
            {
                context.Cantidads.Remove(cantidad);
            }
        }
    }
}
