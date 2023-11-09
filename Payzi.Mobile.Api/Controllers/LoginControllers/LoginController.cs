using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.LoginDTO;
using Payzi.Mobile.Api.Models.LoginModels;
using Payzi.Mobile.Api.Services.LoginServices;
using System.Runtime.CompilerServices;

namespace Payzi.Mobile.Api.Controllers.LoginControllers
{
    public class LoginController : BaseController, ILogin
    {
        private readonly Payzi.Context.Context _context;


        public LoginController(HttpContext httpContext, Payzi.Context.Context context)
            : base(httpContext, context)
        {
            this._context = context;
        }

        public async Task<IResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                Payzi.MySQL.Model.Business.Account.LoginParametros loginParametros = new Payzi.MySQL.Model.Business.Account.LoginParametros
                {
                    Email = loginDTO.Email,
                    Password = loginDTO.Clave,
                    Persistent = false,
                    Context = this._context 
                };

                (Payzi.Enumerate.LoginStatus loginStatus, string token) = await Payzi.MySQL.Model.Business.Account.Logear(loginParametros);

                Payzi.Mobile.Api.Models.LoginModels.LoginModel login = new Payzi.Mobile.Api.Models.LoginModels.LoginModel();

                switch (loginStatus)
                {
                    case Payzi.Enumerate.LoginStatus.InvalidRunOrPassword:
                        {
                            login.Code = (int)StatusCodes.Status400BadRequest;
                            login.Message = "R.U.N. o contraseña incorrectos. Verifique sus datos e inténte acceder nuevamente.";
                            login.Status = "ERROR";

                            return Results.BadRequest(login);
                        }
                    case Payzi.Enumerate.LoginStatus.NotAccessAllowed:
                        {
                            login.Code = (int)StatusCodes.Status401Unauthorized;
                            login.Message = "Usted no tiene suficientes permisos para ingresar a la aplicación. Por favor contacte al administrador.";
                            login.Status = "ERROR";

                            return Results.BadRequest(login);
                        }
                    case Payzi.Enumerate.LoginStatus.UserApprovedOut:
                        {
                            login.Code = (int)StatusCodes.Status403Forbidden;
                            login.Message = "Su cuenta de acceso ha sido caducada. Por favor contacte al administrador del sistema.";
                            login.Status = "ERROR";

                            return Results.BadRequest(login);
                        }
                    case Payzi.Enumerate.LoginStatus.UserLocked:
                        {
                            login.Code = (int)StatusCodes.Status423Locked;
                            login.Message = "Su cuenta de acceso ha sido bloqueada por exceder el máximo de intentos fallidos permitidos.";
                            login.Status = "ERROR";

                            return Results.BadRequest(login);
                        }
                    default:
                        {
                            login.Code = (int)StatusCodes.Status200OK;
                            login.Status = "OK";
                            login.Token = token;

                            return Results.Ok(login);
                        }
                }

            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }

}
