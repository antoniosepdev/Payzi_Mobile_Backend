using Payzi.Abstraction.Abstract;
using Payzi.Mobile.Api.DTO.VoucherDTO;

namespace Payzi.Mobile.Api.Models.VoucherModels
{
    public class VoucherModel : GenericBaseModel<VoucherDTO>
    {
    }

    public class AddVoucherModel : GenericBaseModel<bool> { }
}
