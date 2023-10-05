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
                Payzi.Business.Persona persona = new Payzi.Business.Persona
                {
                    Id = Guid.NewGuid(),
                    RutCuerpo = personaDTO.RutCuerpo,
                    RutDigito = personaDTO.RutDigito.ToString(),
                    Rut = personaDTO.RutCuerpo.ToString() + '-' + personaDTO.RutDigito.ToString(),
                    NombrePrimario = personaDTO.NombrePrimario,
                    NombreSecundario = personaDTO.NombreSecundario,
                    ApellidoPaterno = personaDTO.ApellidoPaterno,
                    ApellidoMaterno = personaDTO.ApellidoMaterno,
                    NombreCompleto = string.IsNullOrEmpty(personaDTO.ApellidoMaterno) ?
                  personaDTO.NombrePrimario + ' ' + personaDTO.NombreSecundario + ' ' + personaDTO.ApellidoPaterno :
                  personaDTO.NombrePrimario + ' ' + personaDTO.NombreSecundario + ' ' + personaDTO.ApellidoPaterno + ' ' + personaDTO.ApellidoMaterno,
                    Email = personaDTO.Email,
                    SexoCodigo = personaDTO.SexoCodigo,
                    FechaNacimiento = personaDTO.FechaNacimiento,
                    Direccion = personaDTO.Direccion,
                    Telefono = personaDTO.Telefono,
                    Celular = personaDTO.Celular,
                    Observaciones = personaDTO.Observaciones,
                    PaisCodigo = personaDTO.PaisCodigo,
                    RegionCodigo = personaDTO.RegionCodigo,
                    CiudadCodigo = personaDTO.CiudadCodigo,
                    ComunaCodigo = personaDTO.ComunaCodigo
                };

                await persona.Save(this._context);
                await _context.SaveChangesAsync();

                personaModel.Success = true;
                personaModel.Code = StatusCodes.Status200OK;
                personaModel.Data = true;

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
