using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Payzi.Business
{
    public class MenuItem : Payzi.Persistent.MenuItem
    {
        public static async Task<bool> Exists(Payzi.Context.Context context, MenuItem menuItem)
        {
            return await (from q in Query.GetMenuItemes(context) select q).AnyAsync<Payzi.Model.MenuItem>(x => x == menuItem);
        }

        public static async Task<MenuItem> GetAsync(Payzi.Context.Context context, Guid menuId)
        {
            Payzi.Model.MenuItem? query = await Query.GetMenuItemes(context).SingleOrDefaultAsync<Payzi.Model.MenuItem>(x =>  x.MenuId == menuId );

            MenuItem menu = query.SingleOrDefault<MenuItem>();

            return menu;
        }

    }
}
