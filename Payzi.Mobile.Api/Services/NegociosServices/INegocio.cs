using Payzi.Mobile.Api.DTO.NegociosDTO;
using Payzi.Mobile.Api.DTO.PersonasDTO;

namespace Payzi.Mobile.Api.Services.NegociosServices
{
    public interface INegocio
    {
        Task<IResult> AddNegocio(NegocioDTO negocioDTO);
    }
}
