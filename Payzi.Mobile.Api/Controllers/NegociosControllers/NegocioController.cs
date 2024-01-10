using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.NegociosDTO;
using Payzi.Mobile.Api.Models.NegociosModels;
using Payzi.Mobile.Api.Services.NegociosServices;

namespace Payzi.Mobile.Api.Controllers.NegociosControllers
{
    public class NegocioController : BaseController, INegocio
    {
        private Payzi.Context.Context _context;

        public NegocioController(HttpContext httpContext, Payzi.Context.Context context)
            : base(httpContext, context)
        {
            _context = context;
        }

        public async Task<IResult> GetNegocio()
        {
            await Task.Delay(1000);

            return Results.Ok();
        }

        public async Task<IResult> AddNegocio(NegocioDTO negocioDTO)
        {
            AddNegocioModel model = new AddNegocioModel();

            try
            {
                string rut = negocioDTO.RutCuerpo + "-" + negocioDTO.RutDigito;

                Payzi.Business.Negocio negocio1 = await Payzi.Business.Negocio.GetAsync(this._context, rut);

                if (negocio1.Rut != rut)
                {
                    Payzi.Business.Negocio negocio = new Payzi.Business.Negocio
                    {
                        Id = Guid.NewGuid(),
                        Nombre = negocioDTO.Nombre,
                        RutCuerpo = negocioDTO.RutCuerpo,
                        RutDigito = negocioDTO.RutDigito.ToString(),
                        Rut = negocioDTO.RutCuerpo.ToString() + '-' + negocioDTO.RutDigito.ToString(),
                        Direccion = negocioDTO.Direccion,
                        DuenoId = negocioDTO.DuenoId,
                        PaisCodigo = negocioDTO.PaisCodigo,
                        RegionCodigo = negocioDTO.RegionCodigo,
                        CiudadCodigo = negocioDTO.CiudadCodigo,
                        ComunaCodigo = negocioDTO.ComunaCodigo
                    };

                    await negocio.Save(this._context);

                    await _context.SaveChangesAsync();

                    model.Success = true;
                    model.Code = StatusCodes.Status200OK;
                    model.Data = true;

                    return Results.Ok(model);
                }
                else
                {
                    model.Success = false;
                    model.Code = StatusCodes.Status400BadRequest;
                    model.Data = false;

                    return Results.BadRequest(model);
                }

            }
            catch
            {
                model.Success = false;
                model.Code = StatusCodes.Status400BadRequest;
                model.Data = false;

                return Results.BadRequest(model);
            }
        }

        public async Task<IResult> UpdateNegocio()
        {
            await Task.Delay(1000);

            return Results.Ok();
        }
        public async Task<IResult> DeleteNegocio()
        {
            await Task.Delay(1000);

            return Results.Ok();
        }
    }
}
