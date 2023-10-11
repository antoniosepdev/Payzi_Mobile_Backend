using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.NegociosDTO;
using Payzi.Mobile.Api.DTO.PagosDTO;
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

        public async Task<IResult> GetTransaccion(GetTransaccionDTO getTransaccionDTO)
        {
            GetTransaccionModel getTransaccionModel = new GetTransaccionModel();

            try
            {
                Payzi.Business.Transaccion transaccion = await Payzi.Business.Transaccion.GetAsync(this._context, getTransaccionDTO.idTransaccion);

                List<Payzi.Business.Transaccion> transaccions = await Payzi.Business.Transaccion.GetAll(this._context, transaccion);

                List<GetTransaccionDTO> listDTO = new List<GetTransaccionDTO>();

                foreach (Payzi.Business.Transaccion item in transaccions)
                {
                    if(item != null)
                    {
                        GetTransaccionDTO getTransaccionDTO1 = new GetTransaccionDTO
                        {
                            amount = item.Amount,
                            tip = item.Tip,
                            cashback = item.Cashback,
                            method = item.Method,
                            installmentsQuantity = item.InstallmentsQuantity,
                            printVoucherOnApp = item.PrintVoucherOnApp,
                            dteType = item.DteType,
                            extraData = item.ExtraDataNavigation.Id
                        };
                        listDTO.Add(getTransaccionDTO1);
                    }
                }

                getTransaccionModel.Code = StatusCodes.Status200OK;
                getTransaccionModel.Status = "Ok";
                getTransaccionModel.DataList = listDTO;

                return Results.Ok(getTransaccionModel);
            }
            catch
            {
                getTransaccionModel.Code = StatusCodes.Status400BadRequest;
                getTransaccionModel.Status = "ERROR";
                getTransaccionModel.DataList = null;

                return Results.BadRequest(getTransaccionModel);
            }
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
