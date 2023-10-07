using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.NegociosDTO;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.Models.NegociosModels;
using Payzi.Mobile.Api.Models.TransaccionModels;
using Payzi.Mobile.Api.Services.TransaccionServices;

namespace Payzi.Mobile.Api.Controllers.TransaccionControllers
{
    public class TransaccionController : BaseController, ITransaccion
    {
        private Payzi.Context.Context _context;

        public TransaccionController(HttpContext httpContext, Payzi.Context.Context context)
            : base(httpContext, context)
        {
            _context = context;
        }

        public async Task<IResult> AddTransaccion(TransaccionDTO transaccionDTO)
        {
            AddTransaccionModel addTransaccionModel = new AddTransaccionModel();

            try
            {
                Payzi.Business.Transaccion transaccion = new Payzi.Business.Transaccion
                {
                    IdTransaccion = Guid.NewGuid(),
                    Amount = transaccionDTO.amount,
                    Tip = transaccionDTO.tip,
                    InstallmentsQuantity = transaccionDTO.installmentsQuantity,
                    PrintVoucherOnApp = transaccionDTO.printVoucherOnApp,
                    DteType = transaccionDTO.dteType,
                    ExtraData = transaccionDTO.extraData, //Editar
                    VoucherId = transaccionDTO.VoucherId //Editar
                };

                await transaccion.Save(this._context);
                await _context.SaveChangesAsync();

                addTransaccionModel.Success = true;
                addTransaccionModel.Code = StatusCodes.Status200OK;
                addTransaccionModel.Data = true;


                return Results.Ok(addTransaccionModel);
            }
            catch
            {


                return Results.BadRequest(addTransaccionModel);
            }
        }
    }
}
