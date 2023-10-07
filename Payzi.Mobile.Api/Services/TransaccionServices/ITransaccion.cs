using Payzi.Mobile.Api.DTO.TransaccionDTO;

namespace Payzi.Mobile.Api.Services.TransaccionServices
{
    public interface ITransaccion
    {
        Task<IResult> AddTransaccion(TransaccionDTO transaccionDTO);
    }
}
