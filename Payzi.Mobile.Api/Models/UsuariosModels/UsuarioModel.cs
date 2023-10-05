using Payzi.Abstraction.Abstract;
using Payzi.Mobile.Api.DTO.UsuariosDTO;

namespace Payzi.Mobile.Api.Models.UsuariosModels
{
    public class UsuarioModel : GenericBaseModel<UsuarioDTO>
    {
    }

    public class GetUsuarioModel : GenericBaseModel<UsuarioDTO>
    {
    }

    public class AddUsuarioModel : GenericBaseModel<bool>
    {
    }

    public class UpdateUsuarioModel : GenericBaseModel<bool>
    {
    }

}
