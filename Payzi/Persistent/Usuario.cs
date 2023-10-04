using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class Usuario : Payzi.Entity.Usuario, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.Usuario? usuario = await context.Usuarios.SingleOrDefaultAsync<Payzi.Model.Usuario>(x => x.Id == this.Id);

            if (usuario == null)
            {
                usuario = new Usuario
                {
                    Id = this.Id
                };

                await context.Usuarios.AddAsync(usuario);
            }

            usuario.Clave = this.Clave;
            usuario.Aprobado = this.Aprobado;
            usuario.Bloqueado = this.Bloqueado;
            usuario.Creacion = this.Creacion;
            usuario.UltimoAcceso = this.UltimoAcceso;
            usuario.UltimoCambioPassword = this.UltimoCambioPassword == default(DateTime) ? null : this.UltimoCambioPassword;
            usuario.FechaIntentoFallido = this.FechaIntentoFallido == default(DateTime) ? null : this.FechaIntentoFallido;
        }
    }
}
