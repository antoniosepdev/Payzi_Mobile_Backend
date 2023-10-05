using Payzi.Abstraction.Abstract;
using Payzi.Mobile.Api.DTO.NegociosDTO;
using Payzi.Mobile.Api.DTO.UsuariosDTO;

namespace Payzi.Mobile.Api.Models.NegociosModels
{
    public class NegocioModel : GenericBaseModel<NegocioDTO>
    {
    }

    public class AddNegocioModel : GenericBaseModel<bool>
    {
    }

    public class UpdateNegocioModel : GenericBaseModel<bool>
    {
    }

}
