using Payzi.Mobile.Api.DTO.CustomFieldsDTO;
using Payzi.Mobile.Api.DTO.ExtraDataDTO;
using Payzi.Mobile.Api.DTO.PagosDTO;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.DTO.TransaccionSalidaDTO;
using Payzi.Mobile.Api.DTO.VoucherDTO;

namespace Payzi.Mobile.Api.Services.PagosServices
{
    public interface IPagos
    {
        Task<IResult> AddPagos(PagosDTO pagosDTO);

        //Task<IResult> RecepcionPago(RecepcionPagosDTO recepcionPagosDTO);

        Task<IResult> RecepcionPago(RecepcionPagosDTO recepcionPagosDTO);

    }
}
