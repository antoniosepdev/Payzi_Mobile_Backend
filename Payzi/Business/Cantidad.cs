using Microsoft.EntityFrameworkCore;
using Payzi.Abstraction.PartialOverload;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class Cantidad : Payzi.Persistent.Cantidad
    {

        public static async Task<Cantidad> GetAsync(Payzi.Context.Context context, Guid idUsuario)
        {
            Payzi.Model.Cantidad query = await Query.GetCantidad(context, idUsuario).SingleOrDefaultAsync<Payzi.Model.Cantidad>(x => x.IdUsuario == idUsuario);

            Cantidad cantidad = query.SingleOrDefault<Cantidad>();

            return cantidad;
        }

        public static async Task<Cantidad> GetAsync(Payzi.Context.Context context, Usuario usuario)
        {
            Payzi.Model.Cantidad query = await Query.GetCantidad(context, usuario).SingleOrDefaultAsync<Payzi.Model.Cantidad>(x => x.IdUsuario == usuario.Id);

            Cantidad cantidad = query.SingleOrDefault<Cantidad>();

            return cantidad;
        }

        public static async Task<List<Cantidad>> GetAll(Payzi.Context.Context context)
        {
            IQueryable<Payzi.Model.Cantidad> query = (from q in Query.GetCantidad(context) select q);

            List<Cantidad> list = await query.ToList<Cantidad>();

            return list;
        }

        public static async Task<List<Cantidad>> GetAll(Payzi.Context.Context context, Usuario usuario)
        {
            IQueryable<Payzi.Model.Cantidad> query = (from q in Query.GetCantidad(context, usuario) select q);

            List<Cantidad> list = await query.ToList<Cantidad>();

            return list;
        }
    }
}