using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class Negocio : Payzi.Entity.Negocio, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.Negocio? negocio = await context.Negocios.SingleOrDefaultAsync<Payzi.Model.Negocio>(x => x.Id == this.Id);

            if (negocio == null)
            {
                negocio = new Payzi.Model.Negocio
                {
                    Id = this.Id,
                };

                context.Negocios.Add(negocio);
            }

            negocio.Nombre = this.Nombre;
            negocio.RutCuerpo = this.RutCuerpo;
            negocio.RutDigito = this.RutDigito;
            negocio.Rut = this.Rut;
            negocio.Direccion = this.Direccion;
            negocio.DuenoId = this.DuenoId;
            negocio.PaisCodigo = this.PaisCodigo == default(Int16) ? null : this.PaisCodigo;
            negocio.RegionCodigo = this.RegionCodigo == default(Int16) ? null : this.RegionCodigo;
            negocio.CiudadCodigo = this.CiudadCodigo == default(Int16) ? null : this.CiudadCodigo;
            negocio.ComunaCodigo = this.ComunaCodigo == default(Int16) ? null : this.ComunaCodigo;
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.Negocio? negocio = await context.Negocios.SingleOrDefaultAsync<Payzi.Model.Negocio>(x => x.Id == this.Id);

            if (negocio != null)
            {
                context.Negocios.Remove(negocio);
            }
        }
    }
}
