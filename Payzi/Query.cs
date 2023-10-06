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
        #region Accion

        public static IQueryable<Payzi.Model.Accion> GetAcciones(Payzi.Context.Context context)
        {
            return
                from accion in context.Accions
                select accion;
        }

        #endregion

        #region MenuItem

        public static IQueryable<Payzi.Model.MenuItem> GetMenuItemes(Payzi.Context.Context context)
        {
            return
                from menuItem in context.MenuItems
                select menuItem;
        }

        public static IQueryable<Payzi.Model.MenuItem> GetMenuItemes(Payzi.Context.Context context, Payzi.Business.Menu menu)
        {
            return
                from menuItem in GetMenuItemes(context)
                where menuItem.MenuId == menu.Id
                select menuItem;
        }

        #endregion

        #region Menu

        public static IQueryable<Payzi.Model.Menu> GetMenus(Payzi.Context.Context context)
        {
            return
                from menu in context.Menus
                select menu;
        }
        #endregion

        #region Negocio
        internal static IQueryable<Payzi.Model.Negocio> GetNegocios(Payzi.Context.Context context)
        {
            return
                from negocio in context.Negocios
                select negocio;
        }

        internal static IQueryable<Payzi.Model.Negocio> GetNegocios(Payzi.Context.Context context, string rut)
        {
            return
                from negocio in GetNegocios(context)
                where negocio.Rut == rut.ToString()
                select negocio;
        }

        internal static IQueryable<Payzi.Model.Negocio> GetNegocios(Payzi.Context.Context context, int cuerpo, char digito)
        {
            return
                from negocio in GetNegocios(context)
                where negocio.RutCuerpo == cuerpo
                   && negocio.RutDigito == digito.ToString()
                select negocio;
        }

        #endregion

        #region Pago
        public static IQueryable<Payzi.Model.Pago> GetPagos(Payzi.Context.Context context)
        {
            return
                from pago in context.Pagos
                select pago;
        }


        #endregion

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

        #region Rol

        public static IQueryable<Payzi.Model.Rol> GetRoles(Payzi.Context.Context context)
        {
            return
                from rol in context.Rols
                select rol;
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
