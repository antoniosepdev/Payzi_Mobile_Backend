using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi
{
    public static class Query
    {

        #region Persona
        internal static IQueryable<Payzi.Model.Persona> GetPersonas(Payzi.Context.Context context)
        {
            return
                from persona in context.Personas
                select persona;
        }
        internal static IQueryable<Payzi.Model.Persona> GetPersonas(Payzi.Context.Context context, int cuerpo, char digito)
        {
            return
                from persona in GetPersonas(context)
                where persona.RutCuerpo == cuerpo
                   && persona.RutDigito == digito.ToString()
                select persona;
        }

        #endregion

        #region Usuario

        public static IQueryable<Payzi.Model.Usuario> GetUsuarios(Payzi.Context.Context context)
        {
            return from usuario in context.Usuarios select usuario;
        }

        #endregion
    }
}
