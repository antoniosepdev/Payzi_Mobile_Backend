using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class Pago : Payzi.Persistent.Pago
    {
        public static async Task<Pago> GetAsync(Payzi.Context.Context context, Guid idPago)
        {
            Payzi.Model.Pago query = await Query.GetPagos(context).Include("IdUsuarioNavigation").Include("IdTransaccionNavigation").SingleOrDefaultAsync<Payzi.Model.Pago>(x => x.IdPago == idPago);

            Pago pago = query.SingleOrDefault<Pago>();

            return pago;
        }

        public static async Task<Pago> GetAsync2(Payzi.Context.Context context, Guid idUsuario)
        {
            Payzi.Model.Pago query = await Query.GetPagos(context).Include("IdUsuarioNavigation").Include("IdTransaccionNavigation").SingleOrDefaultAsync<Payzi.Model.Pago>(x => x.IdUsuario == idUsuario);

            Pago pago = query.SingleOrDefault<Pago>();

            return pago;
        }

        public static async Task<Pago> GetAsync(Payzi.Context.Context context, Transaccion transaccion)
        {
            Payzi.Model.Pago query = await Query.GetPagos(context, transaccion).Include("IdUsuarioNavigation").Include("IdTransaccionNavigation").SingleOrDefaultAsync<Payzi.Model.Pago>(x => x.IdTransaccion == transaccion.IdTransaccion);

            Pago pago = query.SingleOrDefault<Pago>();

            return pago;
        }

        public static async Task<List<Pago>> GetAll(Payzi.Context.Context context)
        {
            IQueryable<Payzi.Model.Pago> query = (from q in Query.GetPagos(context)  select q);

            List<Pago> list = await query.ToList<Pago>();

            return list;
        }

        public static async Task<List<Pago>> GetAll(Payzi.Context.Context context, Payzi.Business.Usuario usuario)
        {
            IQueryable<Payzi.Model.Pago> query = (from q in Query.GetPagos(context, usuario) 
                                                  where q.IdUsuario == usuario.Id 
                                                  select q);

            List<Pago> list = await query.ToList<Pago>();

            return list;
        }
    }
}
