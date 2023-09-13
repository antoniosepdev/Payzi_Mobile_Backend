using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.Usuarios;
using Payzi.Mobile.Api.Models.Usuarios;
using Payzi.Mobile.Api.Services.Usuarios;
using Payzi.MySQL.Data;

namespace Payzi.Mobile.Api.Controllers.Usuarios
{
    public class UsuarioController : BaseController, IUsuario
    {
        private MySQLConfiguration _connectionString;

        public UsuarioController(HttpContext httpContext, MySQLConfiguration connectionString)
            : base(httpContext, connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
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
                var db = dbConnection();

                var sql = @"INSERT INTO usuario(Id, Rut, Nombre, Email, Clave, Bloqueo) VALUES (@Id, @Rut, @Nombre, @Email, @Clave, @Bloqueo) ";

                var result = await db.ExecuteAsync(sql, new { usuario.Id, usuario.Rut, usuario.Nombre, usuario.Email, usuario.Clave, usuario.Bloqueo });

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
