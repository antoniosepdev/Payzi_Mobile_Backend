using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.UsuariosDTO;
using Payzi.Mobile.Api.Models.UsuariosModels;
using Payzi.Mobile.Api.Services.Usuarios;


namespace Payzi.Mobile.Api.Controllers.UsuariosControllers
{
    public class UsuarioController : BaseController, IUsuario
    {
        private Payzi.Context.Context _context;

        public UsuarioController(HttpContext httpContext, Payzi.Context.Context context)
            : base(httpContext, context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioController>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> GetUser()
        {
            return Results.Ok();
        }

        public async Task<IResult> AddUser(UsuarioDTO usuario)
        {
            UsuarioModel usuarioModel = new UsuarioModel();

            try
            {
                //var db = dbConnection();

                var sql = @"INSERT INTO usuario(Id, Email, Clave, Aprobado, Bloqueado, RolCodigo, Creacion) VALUES (@Id, @Email, @Clave, @Aprobado, @Bloqueado, @RolCodigo, @Creacion) ";

                //usuario.Id = Guid.NewGuid();

                usuario.Id = usuario.Id;
                usuario.Email = usuario.Email;
                usuario.Clave = Filters.Procesadores.Encriptar.EncryptPassword(usuario.Clave);
                usuario.Aprobado = true;
                usuario.Bloqueado = false;
                usuario.RolCodigo = usuario.RolCodigo;
                usuario.Creacion = DateTime.Now;

                //var result = await db.ExecuteAsync(sql, new { usuario.Id, usuario.Email, usuario.Clave, usuario.Aprobado, usuario.Bloqueado, usuario.RolCodigo, usuario.Creacion });

                usuarioModel.Success = true;
                usuarioModel.Data = usuario;

                return Results.Ok(usuarioModel);
            }
            catch
            {
                usuarioModel.Success = false;
                usuarioModel.Data = null;

                return Results.BadRequest(usuarioModel);
            }
        }

        public async Task<IResult> UpdateUser()
        {
            return Results.Ok();
        }

        public async Task<IResult> DeleteUser()
        {
            return Results.Ok();
        }

    }
}
