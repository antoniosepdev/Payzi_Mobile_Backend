using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.DTO.VoucherDTO;
using Payzi.Mobile.Api.Models.TransaccionModels;
using Payzi.Mobile.Api.Models.VoucherModels;
using Payzi.Mobile.Api.Services.VoucherServices;

namespace Payzi.Mobile.Api.Controllers.VoucherControllers
{
    public class VoucherController : BaseController, IVoucher
    {
        private Payzi.Context.Context _context;

        public VoucherController(HttpContext httpContext, Payzi.Context.Context context)
            : base(httpContext, context)
        {
            _context = context;
        }

        public async Task<IResult> AddVoucher(VoucherDTO voucherDTO)
        {
            AddVoucherModel addVoucherModel = new AddVoucherModel();

            try
            {
                Payzi.Business.Voucher voucher = new Payzi.Business.Voucher
                {
                    Id = voucherDTO.Id,
                    NombreCliente = voucherDTO.NombreCliente,
                    NumeroDocumento = voucherDTO.NumeroDocumento,
                    Monto = voucherDTO.Monto,
                    FechaEmision = DateTime.Now,
                    Descripcion = voucherDTO.Descripcion,
                    MetodoPagoCodigo = voucherDTO.MetodoPagoCodigo,
                    NumeroTransaccion = voucherDTO.NumeroTransaccion,
                    UsuarioId = voucherDTO.UsuarioId, //Editar
                    Estado = voucherDTO.Estado
                };

                await voucher.Save(this._context);
                await _context.SaveChangesAsync();

                addVoucherModel.Success = true;
                addVoucherModel.Code = StatusCodes.Status200OK;
                addVoucherModel.Data = true;

                return Results.Ok(addVoucherModel);
            }
            catch
            {
                addVoucherModel.Success = false;
                addVoucherModel.Code = StatusCodes.Status400BadRequest;
                addVoucherModel.Data = false;

                return Results.BadRequest(addVoucherModel);
            }
        }
    }
}
