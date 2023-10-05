using Microsoft.EntityFrameworkCore;
using Payzi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Persistent
{
    public class Persona : Payzi.Entity.Persona, IRecordable
    {
        public async Task Save(Payzi.Context.Context context)
        {
            Payzi.Model.Persona? persona = await context.Personas.SingleOrDefaultAsync<Payzi.Model.Persona>(x => x.Id == this.Id);

            if (persona == null)
            {
                persona = new Persona
                {
                    Id = this.Id,
                };

                context.Personas.Add(persona);
            }

            persona.RutCuerpo = this.RutCuerpo;
            persona.RutDigito = this.RutDigito;
            persona.Rut = this.Rut == default(string) ? null : this.Rut;
            persona.NombrePrimario = this.NombrePrimario;
            persona.NombreSecundario = this.NombreSecundario == default(string) ? null : this.NombreSecundario;
            persona.ApellidoPaterno = this.ApellidoPaterno;
            persona.ApellidoMaterno = this.ApellidoMaterno == default(string) ? null : this.ApellidoMaterno;
            persona.NombreCompleto = this.NombreCompleto == default(string) ? null : this.NombreCompleto;
            persona.Email = this.Email;
            persona.SexoCodigo = this.SexoCodigo;
            persona.FechaNacimiento = this.FechaNacimiento;
            persona.PaisCodigo = this.PaisCodigo == default(Int16) ? null : this.PaisCodigo;
            persona.RegionCodigo = this.RegionCodigo == default(Int16) ? null : this.RegionCodigo;
            persona.CiudadCodigo = this.CiudadCodigo == default(Int16) ? null : this.CiudadCodigo;
            persona.ComunaCodigo = this.ComunaCodigo == default(Int16) ? null : this.ComunaCodigo;
            persona.Direccion = this.Direccion;
            persona.Telefono = this.Telefono == default(Int32) ? null : this.Telefono;
            persona.Celular = this.Celular == default(Int32) ? null : this.Celular;
            persona.Observaciones = this.Observaciones;
        }

        public async Task Delete(Payzi.Context.Context context)
        {
            Payzi.Model.Persona? persona = await context.Personas.SingleOrDefaultAsync<Payzi.Model.Persona>(x => x.Id == this.Id);

            if (persona != null)
            {
                context.Personas.Remove(persona);
            }
        }
    }
}
