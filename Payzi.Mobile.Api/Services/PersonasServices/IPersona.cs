using Payzi.Mobile.Api.DTO.PersonasDTO;
using Payzi.Mobile.Api.DTO.UsuariosDTO;

namespace Payzi.Mobile.Api.Services.PersonasServices
{
    public interface IPersona
    {
        Task<IResult> AddPerson(PersonaDTO personaDTO);
    }
}
