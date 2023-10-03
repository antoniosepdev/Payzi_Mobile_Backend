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
        private readonly Payzi.MySQL.Model.Context _context;
        private readonly MySQL.Data.MySQLConfiguration _connectionString;

        public LoginController(HttpContext httpContext, MySQL.Data.MySQLConfiguration connectionString)
            : base(httpContext, connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task<IResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                Payzi.MySQL.Model.Business.Account.LoginParametros loginParametros = new Payzi.MySQL.Model.Business.Account.LoginParametros
                {
                    Email = loginDTO.Email,
                    Password = loginDTO.Password,
                    Persistent = false,
                    Context = this._context 
                };

                (Payzi.MySQL.Model.Enumerate.LoginStatus loginStatus, string token) = await Payzi.MySQL.Model.Business.Account.Logear(loginParametros);

                Payzi.Mobile.Api.Models.LoginModels.LoginModel login = new Payzi.Mobile.Api.Models.LoginModels.LoginModel();

                switch (loginStatus)
                {
                    case Payzi.MySQL.Model.Enumerate.LoginStatus.InvalidRunOrPassword:
                        {
                            login.Code = (int)StatusCodes.Status400BadRequest;
                            login.Message = "R.U.N. o contraseña incorrectos. Verifique sus datos e inténte acceder nuevamente.";
                            login.Status = "ERROR";
                            login.SubStatus = "ERROR";

                            return Results.BadRequest(login);
                        }
                    case Payzi.MySQL.Model.Enumerate.LoginStatus.NotAccessAllowed:
                        {
                            login.Code = (int)StatusCodes.Status401Unauthorized;
                            login.Message = "Usted no tiene suficientes permisos para ingresar a la aplicación. Por favor contacte al administrador.";
                            login.Status = "ERROR";
                            login.SubStatus = "ERROR";

                            return Results.BadRequest(login);
                        }
                    case Payzi.MySQL.Model.Enumerate.LoginStatus.UserApprovedOut:
                        {
                            login.Code = (int)StatusCodes.Status403Forbidden;
                            login.Message = "Su cuenta de acceso ha sido caducada. Por favor contacte al administrador del sistema.";
                            login.Status = "ERROR";
                            login.SubStatus = "ERROR";

                            return Results.BadRequest(login);
                        }
                    case Payzi.MySQL.Model.Enumerate.LoginStatus.UserLocked:
                        {
                            login.Code = (int)StatusCodes.Status423Locked;
                            login.Message = "Su cuenta de acceso ha sido bloqueada por exceder el máximo de intentos fallidos permitidos.";
                            login.Status = "ERROR";
                            login.SubStatus = "ERROR";

                            return Results.BadRequest(login);
                        }
                    default:
                        {
                            login.Code = (int)StatusCodes.Status200OK;
                            login.Status = "OK";
                            login.SubStatus = "OK";
                            login.Token = token;
                            //login.ReCaptchaResult = isRecaptchaValid;

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
