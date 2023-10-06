using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class Menu : Payzi.Persistent.Menu
    {
        public static async Task<Menu> GetAsync(Payzi.Context.Context context, string clave)
        {
            Payzi.Model.Menu? query = await Query.GetMenus(context).SingleOrDefaultAsync<Payzi.Model.Menu>(x => x.Clave == clave);

            Menu menu = query.SingleOrDefault<Menu>();

            return menu;
        }

        public static Task<Menu> MenuAdmin(Payzi.Context.Context context)
        {
            return Menu.GetAsync(context, "MenuAdmin");
        }

        public static Task<Menu> MenuUser(Payzi.Context.Context context)
        {
            return Menu.GetAsync(context, "MenuUser");
        }

    }
}
