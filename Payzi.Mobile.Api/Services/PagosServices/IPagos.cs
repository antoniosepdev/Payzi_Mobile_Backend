using Payzi.Mobile.Api.DTO.PagosDTO;

namespace Payzi.Mobile.Api.Services.PagosServices
{
    public interface IPagos
    {
        Task<IResult> AddPagos(PagosDTO pagosDTO);

        Task<IResult> RecepcionPago(RecepcionPagosDTO recepcionPagosDTO);
    }
}
