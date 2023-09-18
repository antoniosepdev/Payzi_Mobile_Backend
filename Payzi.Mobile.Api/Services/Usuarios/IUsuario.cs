using Payzi.Mobile.Api.DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Mobile.Api.Services.Usuarios
{
    public interface IUsuario
    {
        //Task<IEnumerable<Usuario>> GetAllUsers();

        Task<IResult> GetUser();

        Task<IResult> AddUser(UsuarioDTO usuario);

        Task<IResult> UpdateUser();

        Task<IResult> DeleteUser();
    }
}