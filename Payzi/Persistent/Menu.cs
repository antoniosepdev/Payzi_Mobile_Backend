using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class Menu : Payzi.Entity.Menu, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.Menu? menu = await context.Menus.SingleOrDefaultAsync<Payzi.Model.Menu>(x => x.Id == this.Id);

            if (menu == null)
            {
                menu = new Menu
                {
                    Id = this.Id
                };

                await context.Menus.AddAsync(menu);
            }

            menu.Nombre = this.Nombre;
            menu.Clave = this.Clave;
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.Menu? menu = await context.Menus.SingleOrDefaultAsync<Payzi.Model.Menu>(x => x.Id == this.Id);

            if (menu != null)
            {
                context.Menus.Remove(menu);
            }
        }
    }
}
