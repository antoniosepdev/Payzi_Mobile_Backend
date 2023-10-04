using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.PersonasDTO;
using Payzi.Mobile.Api.Models.PersonasModels;
using Payzi.Mobile.Api.Services.PersonasServices;

namespace Payzi.Mobile.Api.Controllers.PersonasControllers
{
    public class PersonaController : BaseController, IPersona
    {
        private Payzi.Context.Context _context;

        public PersonaController(HttpContext httpContext, Payzi.Context.Context context)
            : base(httpContext, context)
        {
            _context = context;        }

        public async Task<IResult> GetPerson()
        {
            return Results.Ok();
        }

        public async Task<IResult> AddPerson(PersonaDTO personaDTO)
        {
            PersonaModel personaModel = new PersonaModel();

            try
            {
                //var db = dbConnection();

                var sql = @"INSERT INTO persona(Id, Rut, RutCuerpo, RutDigito, NombreCompleto, NombrePrimario, NombreSecundario, ApellidoPaterno, ApellidoMaterno, Email, SexoCodigo, FechaNacimiento, Direccion, Telefono, Celular, Observaciones, PaisCodigo, RegionCodigo, CiudadCodigo, ComunaCodigo)
                            VALUES(@Id, @Rut, @RutCuerpo, @RutDigito, @NombreCompleto, @NombrePrimario, @NombreSecundario, @ApellidoPaterno, @ApellidoMaterno, @Email, @SexoCodigo, @FechaNacimiento, @Direccion, @Telefono, @Celular, @Observaciones, @PaisCodigo, @RegionCodigo, @CiudadCodigo, @ComunaCodigo) ";


                personaDTO.Id = Guid.NewGuid();
                personaDTO.RutCuerpo = personaDTO.RutCuerpo;
                personaDTO.RutDigito = personaDTO.RutDigito;
                personaDTO.Rut = (personaDTO.RutCuerpo.ToString()) + "-" + (personaDTO.RutDigito.ToString());
                personaDTO.NombrePrimario = personaDTO.NombrePrimario;
                personaDTO.NombreSecundario = personaDTO.NombreSecundario;
                personaDTO.ApellidoPaterno = personaDTO.ApellidoPaterno;
                personaDTO.ApellidoMaterno = personaDTO.ApellidoMaterno;
                personaDTO.NombreCompleto = personaDTO.NombrePrimario + " " + personaDTO.NombreSecundario + " " + personaDTO.ApellidoPaterno + " " + personaDTO.ApellidoMaterno;
                personaDTO.Email = personaDTO.Email;
                personaDTO.SexoCodigo = personaDTO.SexoCodigo;
                personaDTO.FechaNacimiento = personaDTO.FechaNacimiento.Date;
                personaDTO.Direccion = personaDTO.Direccion;
                personaDTO.Telefono = personaDTO.Telefono;
                personaDTO.Celular = personaDTO.Celular;
                personaDTO.Observaciones = personaDTO.Observaciones;
                personaDTO.PaisCodigo = personaDTO.PaisCodigo;
                personaDTO.RegionCodigo = personaDTO.RegionCodigo;
                personaDTO.CiudadCodigo = personaDTO.CiudadCodigo;
                personaDTO.ComunaCodigo = personaDTO.ComunaCodigo;

                //var result = await db.ExecuteAsync(sql, new { personaDTO.Id, personaDTO.Rut, personaDTO.RutCuerpo, personaDTO.RutDigito, personaDTO.NombreCompleto, personaDTO.NombrePrimario, personaDTO.NombreSecundario, personaDTO.ApellidoPaterno, personaDTO.ApellidoMaterno, personaDTO.Email, personaDTO.SexoCodigo, personaDTO.FechaNacimiento, personaDTO.Direccion, personaDTO.Telefono, personaDTO.Celular, personaDTO.Observaciones, personaDTO.PaisCodigo, personaDTO.RegionCodigo, personaDTO.CiudadCodigo, personaDTO.ComunaCodigo });
                personaModel.Success = true;
                personaModel.Data = personaDTO;

                return Results.Ok(personaModel);
            }
            catch
            {
                personaModel.Success = false;
                return Results.BadRequest(personaModel);
            }
        }

        public async Task<IResult> UpdatePerson()
        {
            return Results.Ok();
        }
        public async Task<IResult> DeletePerson()
        {
            return Results.Ok();
        }


    }
}
