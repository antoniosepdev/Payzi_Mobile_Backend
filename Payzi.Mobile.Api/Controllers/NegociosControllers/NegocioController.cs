using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.NegociosDTO;
using Payzi.Mobile.Api.DTO.PersonasDTO;
using Payzi.Mobile.Api.Models.NegociosModels;
using Payzi.Mobile.Api.Models.UsuariosModels;
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
            return Results.Ok();
        }

        public async Task<IResult> AddNegocio(NegocioDTO negocioDTO)
        {
            NegocioModel negocioModel = new NegocioModel();

            try
            {
                //var db = dbConnection();

                //var sql = @"INSERT INTO negocio(Id, Nombre, Rut, Direccion, DuenoId, ComunaCodigo, CiudadCodigo, RegionCodigo, PaisCodigo) 
                //            VALUES (@Id, @Nombre, @Rut, @Direccion, @DuenoId, @ComunaCodigo, @CiudadCodigo, @RegionCodigo, @PaisCodigo) ";

                negocioDTO.Id = Guid.NewGuid();

                negocioDTO.Nombre = negocioDTO.Nombre;
                negocioDTO.Rut = negocioDTO.Rut;
                negocioDTO.Direccion = negocioDTO.Direccion;

                //negocioDTO.DueñoId = negocioDTO.DueñoId;
                //negocioDTO.ComunaCodigo = negocioDTO.ComunaCodigo;
                //negocioDTO.CiudadCodigo = negocioDTO.CiudadCodigo;
                //negocioDTO.RegionCodigo = negocioDTO.RegionCodigo;
                //negocioDTO.PaisCodigo = negocioDTO.PaisCodigo;

                //var result = await db.ExecuteAsync(sql, new { negocioDTO.Id, negocioDTO.Nombre, negocioDTO.Rut, negocioDTO.Direccion, negocioDTO.DuenoId, negocioDTO.ComunaCodigo, negocioDTO.CiudadCodigo, negocioDTO.RegionCodigo, negocioDTO.PaisCodigo });
                negocioModel.Success = true;
                negocioModel.Data = negocioDTO;
                return Results.Ok(negocioModel);
            }
            catch
            {
                negocioModel.Success = false;
                return Results.BadRequest();
            }
        }

        public async Task<IResult> UpdateNegocio()
        {
            return Results.Ok();
        }
        public async Task<IResult> DeleteNegocio()
        {
            return Results.Ok();
        }
    }
}
