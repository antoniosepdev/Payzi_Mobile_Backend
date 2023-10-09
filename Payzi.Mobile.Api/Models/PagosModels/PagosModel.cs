using Payzi.Abstraction.Abstract;
using Payzi.Mobile.Api.DTO.CustomFieldsDTO;
using Payzi.Mobile.Api.DTO.ExtraDataDTO;
using Payzi.Mobile.Api.DTO.PagosDTO;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.DTO.TransaccionSalidaDTO;

namespace Payzi.Mobile.Api.Models.PagosModels
{
    public class PagosModel : GenericBaseModel<PagosDTO>
    {
    }
    public class GetPagosModel : GenericBaseModel<PagosDTO>
    {
    }
    public class AddPagosModel : GenericBaseModel<bool>
    {
    }

    public class RecepcionPagosModel : GenericBaseModel<RecepcionPagosDTO>
    {
    }
}
