using Payzi.Mobile.Api.DTO.NegociosDTO;
using Payzi.Mobile.Api.DTO.PersonasDTO;

namespace Payzi.Mobile.Api.Services.NegociosServices
{
    public interface INegocio
    {
        //Task<IResult> Getegocio(NegocioDTO negocioDTO);

        Task<IResult> AddNegocio(NegocioDTO negocioDTO);

        //Task<IResult> UpdateNegocio(NegocioDTO negocioDTO);
    }
}
