using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi
{
    public static class Query
    {
        #region Usuario

        public static IQueryable<Payzi.Model.Usuario> GetUsuarios(Payzi.Context.Context context)
        {
            return from usuario in context.Usuarios select usuario;
        }

        #endregion
    }
}
