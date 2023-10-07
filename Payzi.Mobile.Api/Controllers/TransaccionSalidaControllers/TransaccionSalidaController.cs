using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.DTO.TransaccionSalidaDTO;
using Payzi.Mobile.Api.Models.TransaccionModels;
using Payzi.Mobile.Api.Models.TransaccionSalidaModels;
using Payzi.Mobile.Api.Services.TransaccionSalidaServices;

namespace Payzi.Mobile.Api.Controllers.TransaccionSalidaControllers
{
    public class TransaccionSalidaController : BaseController, ITransaccionSalida
    {
        private Payzi.Context.Context _context;

        public TransaccionSalidaController(HttpContext httpContext, Payzi.Context.Context context)
            : base(httpContext, context)
        {
            _context = context;
        }

        public async Task<IResult> TransaccionSalida(TransaccionSalidaDTO transaccionSalidaDTO)
        {
            TransaccionSalidaModel transaccionSalidaModel = new TransaccionSalidaModel();

            try
            {
                Payzi.Business.TransaccionSalida transaccionSalida = new Payzi.Business.TransaccionSalida
                {
                    Id = Guid.NewGuid(),
                    TransactionStatus = transaccionSalidaDTO.TransactionStatus,
                    SequenceNumber = transaccionSalidaDTO.SequenceNumber,
                    PrinterVoucherCommerce = transaccionSalidaDTO.PrinterVoucherCommerce,
                    ExtraData = transaccionSalidaDTO.ExtraData,
                    TransactionTip = transaccionSalidaDTO.TransactionTip,
                    TransactionCashback = transaccionSalidaDTO.TransactionCashBack
                };

                await transaccionSalida.Save(this._context);
                await _context.SaveChangesAsync();

                transaccionSalidaModel.Success = true;
                transaccionSalidaModel.Code = StatusCodes.Status200OK;
                transaccionSalidaModel.Data = transaccionSalidaDTO;

                return Results.Ok(transaccionSalidaModel);
            }
            catch
            {


                return Results.BadRequest(transaccionSalidaModel);
            }
        }
    }
}
