using Payzi.Abstraction.Abstract;
using Payzi.Mobile.Api.DTO.TransaccionDTO;

namespace Payzi.Mobile.Api.Models.TransaccionModels
{
    public class TransaccionModel : GenericBaseModel<TransaccionDTO>
    {
    }

    public class GetTransaccionModel : GenericBaseModel<GetTransaccionDTO>
    {
    }

    public class AddTransaccionModel : GenericBaseModel<bool>
    {
    }
}
