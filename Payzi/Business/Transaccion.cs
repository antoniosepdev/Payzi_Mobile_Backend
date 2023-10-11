using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class Transaccion : Payzi.Persistent.Transaccion
    {
        public static async Task<Transaccion> GetAsync(Payzi.Context.Context context, Guid idTransaccion)
        {
            Payzi.Model.Transaccion query = await Query.GetTransacciones(context).SingleOrDefaultAsync<Payzi.Model.Transaccion>(x => x.IdTransaccion == idTransaccion);

            Transaccion transaccion = query.SingleOrDefault<Transaccion>();

            return transaccion;
        }

        public static async Task<List<Transaccion>> GetAll(Payzi.Context.Context context)
        {
            IQueryable<Payzi.Model.Transaccion> query = (from q in Query.GetTransacciones(context).Include("ExtraDataNavigation").Include("Pago") orderby q.IdTransaccion select q);

            List<Transaccion> list = await query.ToList<Transaccion>();

            return list;
        }


        public static async Task<List<Transaccion>> GetAll(Payzi.Context.Context context, Transaccion transaccion)
        {
            IQueryable<Payzi.Model.Transaccion> query = (from q in Query.GetTransacciones(context).Include("ExtraDataNavigation")
                                                         where q.IdTransaccion == transaccion.IdTransaccion 
                                                         orderby q.IdTransaccion select q);

            List<Transaccion> list = await query.ToList<Transaccion>();

            return list;
        }


        public static async Task<List<Transaccion>> GetAll(Payzi.Context.Context context, ExtraData extraData)
        {
            IQueryable<Payzi.Model.Transaccion> query = (from q in Query.GetTransacciones(context, extraData) orderby q.IdTransaccion select q);

            List<Transaccion> list = await query.ToList<Transaccion>();

            return list;
        }
    }
}
