using Payzi.Mobile.Api.DTO.TransaccionSalidaDTO;

namespace Payzi.Mobile.Api.Services.TransaccionSalidaServices
{
    public interface ITransaccionSalida
    {
        Task<IResult> TransaccionSalida(TransaccionSalidaDTO transaccionSalidaDTO);
    }
}
