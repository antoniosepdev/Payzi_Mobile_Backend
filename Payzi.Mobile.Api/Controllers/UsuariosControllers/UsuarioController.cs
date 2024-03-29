﻿using Payzi.Business;
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
            await Task.Delay(1000);

            throw new NotImplementedException();
        }

        public async Task<IResult> GetUser()
        {
            await Task.Delay(1000);

            return Results.Ok();
        }

        public async Task<IResult> AddUser(UsuarioDTO usuarioDTO)
        {
            AddUsuarioModel model = new AddUsuarioModel();

            try
            {
                Payzi.Business.Usuario usuario = new Business.Usuario
                {
                    Id = Guid.NewGuid(),
                    Email = usuarioDTO.Email,
                    Clave = Account.EncryptPassword(usuarioDTO.Clave),
                    Aprobado = true,
                    Bloqueado = false,
                    RolCodigo = usuarioDTO.RolCodigo,
                    Creacion = DateTime.Now,
                    UltimoAcceso = null,
                    UltimoCambioPassword = null,
                    FechaIntentoFallido = null,
                    NegocioId = usuarioDTO.NegocioId
                };

                await usuario.Save(this._context);
                await _context.SaveChangesAsync();

                model.Success = true;
                model.Code = StatusCodes.Status200OK;
                model.Data = true;

                return Results.Ok(model);
            }
            catch
            {
                model.Success = false;
                model.Code = StatusCodes.Status400BadRequest;
                model.Data = false;

                return Results.BadRequest(model);
            }
        }

        public async Task<IResult> UpdateUser()
        {
            await Task.Delay(1000);

            return Results.Ok();
        }

        public async Task<IResult> DeleteUser()
        {
            await Task.Delay(1000);

            return Results.Ok();
        }

    }
}
