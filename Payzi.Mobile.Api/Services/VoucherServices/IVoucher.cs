using Payzi.Mobile.Api.DTO.VoucherDTO;

namespace Payzi.Mobile.Api.Services.VoucherServices
{
    public interface IVoucher
    {
        Task<IResult> AddVoucher(VoucherDTO voucherDTO);
    }
}
