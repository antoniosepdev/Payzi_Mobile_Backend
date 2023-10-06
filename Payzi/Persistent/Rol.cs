using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class Rol : Payzi.Entity.Rol, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.Rol? rol = await context.Rols.SingleOrDefaultAsync<Payzi.Model.Rol>(x => x.Codigo == this.Codigo);

            if (rol == null)
            {
                rol = new Rol
                {
                    Codigo = this.Codigo
                };

                await context.Rols.AddAsync(rol);
            }

            rol.Nombre = this.Nombre;
            rol.Clave = this.Clave;
            rol.MenuId = this.MenuId;
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.Rol? rol = await context.Rols.SingleOrDefaultAsync<Payzi.Model.Rol>(x => x.Codigo == this.Codigo);

            if (rol != null)
            {
                context.Rols.Remove(rol);
            }
        }
    }
}
