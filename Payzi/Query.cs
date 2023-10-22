using Microsoft.VisualBasic.FileIO;
using Payzi.Business;
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

        #region CustomField

        public static IQueryable<Payzi.Model.CustomField> GetCustomFields(Payzi.Context.Context context)
        {
            return
                from customField in context.CustomFields
                select customField;
        }

        public static IQueryable<Payzi.Model.CustomField> GetCustomFields(Payzi.Context.Context context, Payzi.Business.ExtraData extraData)
        {
            return
                from customField in context.CustomFields
                where customField.IdCustomFields == extraData.CustomFields
                select customField;
        }

        #endregion

        #region ExtraData

        public static IQueryable<Payzi.Model.ExtraDatum> GetExtraData(Payzi.Context.Context context)
        {
            return 
                from extraData in context.ExtraData 
                select extraData;
        }

        public static IQueryable<Payzi.Model.ExtraDatum> GetExtraData(Payzi.Context.Context context, Payzi.Business.Transaccion transaccion)
        {
            return
                from extraData in context.ExtraData
                where extraData.Id == transaccion.ExtraData
                select extraData;
        }


        #endregion

        #region FormaPago

        public static IQueryable<Payzi.Model.FormaPago> GetFormaPagos(Payzi.Context.Context context)
        {
            return
                from FormaPago in context.FormaPagos
                select FormaPago;
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

        public static IQueryable<Payzi.Model.Pago> GetPagos(Payzi.Context.Context context, Transaccion transaccion)
        {
            return
                from pago in context.Pagos 
                where pago.IdTransaccion == transaccion.IdTransaccion 
                select pago;
        }

        public static IQueryable<Payzi.Model.Pago> GetPagos(Payzi.Context.Context context, Usuario usuario)
        {
            return
                from pago in context.Pagos
                where pago.IdUsuario == usuario.Id
                select pago;
        }

        public static IQueryable<Payzi.Model.Pago> GetPagos(Payzi.Context.Context context, Transaccion transaccion, Usuario usuario)
        {
            return from pago in context.Pagos 
                   where pago.IdTransaccion == transaccion.IdTransaccion && pago.IdUsuario == usuario.Id
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

        #region TransaccionSalida

        public static IQueryable<Payzi.Model.TransaccionSalidum> GetTransaccionesSalida(Payzi.Context.Context context)
        {
            return
                from transaccionSalida in context.TransaccionSalida
                select transaccionSalida;
        }

        #endregion

        #region Transaccion

        public static IQueryable<Payzi.Model.Transaccion> GetTransacciones(Payzi.Context.Context context)
        {
            return
                from transaccion in context.Transaccions
                select transaccion;
        }

        public static IQueryable<Payzi.Model.Transaccion> GetTransacciones(Payzi.Context.Context context, Pago pago)
        {
            return
                from transaccion in context.Transaccions
                where transaccion.IdTransaccion == pago.IdTransaccion 
                select transaccion;
        }


        public static IQueryable<Payzi.Model.Transaccion> GetTransacciones(Payzi.Context.Context context, ExtraData extraData)
        {
            return
                from transaccion in context.Transaccions 
                where transaccion.ExtraDataNavigation == extraData 
                select transaccion;
        }


        #endregion

        #region Usuario

        public static IQueryable<Payzi.Model.Usuario> GetUsuarios(Payzi.Context.Context context)
        {
            return from usuario in context.Usuarios select usuario;
        }

        #endregion

        #region Voucher

        public static IQueryable<Payzi.Model.Voucher> GetVoucher(Payzi.Context.Context context)
        {
            return
                from voucher in context.Vouchers
                select voucher;
        }

        #endregion
    }
}
