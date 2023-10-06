using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class MenuItem : Payzi.Entity.MenuItem, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.MenuItem? menuItem = await context.MenuItems.SingleOrDefaultAsync<Payzi.Model.MenuItem>(x => x.MenuId == this.MenuId && x.Id == this.Id);

            if (menuItem == null)
            {
                menuItem = new MenuItem
                {
                    MenuId = this.MenuId,
                    Id = this.Id
                };

                await context.MenuItems.AddAsync(menuItem);
            }

            menuItem.Nombre = this.Nombre;
            menuItem.Titulo = this.Titulo;
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.MenuItem? menuItem = await context.MenuItems.SingleOrDefaultAsync<Payzi.Model.MenuItem>(x => x.MenuId == this.MenuId && x.Id == this.Id);

            if (menuItem != null)
            {
                context.MenuItems.Remove(menuItem);
            }
        }
    }
}
