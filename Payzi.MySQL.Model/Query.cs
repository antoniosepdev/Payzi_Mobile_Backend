using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.MySQL.Model
{
    public static class Query
    {
        #region Usuario

        public static IQueryable<Payzi.MySQL.Model.Usuario> GetUsuarios(Payzi.MySQL.Model.Context context)
        {
            return from usuario in context.Usuarios select usuario;
        }

        #endregion
    }
}
