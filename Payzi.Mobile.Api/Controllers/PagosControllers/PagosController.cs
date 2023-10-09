using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.CustomFieldsDTO;
using Payzi.Mobile.Api.DTO.ExtraDataDTO;
using Payzi.Mobile.Api.DTO.PagosDTO;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.DTO.TransaccionSalidaDTO;
using Payzi.Mobile.Api.Models.CustomFieldsModels;
using Payzi.Mobile.Api.Models.PagosModels;
using Payzi.Mobile.Api.Services.PagosServices;

namespace Payzi.Mobile.Api.Controllers.PagosControllers
{
    public class PagosController : BaseController, IPagos
    {
        private Payzi.Context.Context _context;

        public PagosController(HttpContext httpContext, Payzi.Context.Context context)
            : base(httpContext, context)
        {
            _context = context;
        }

        public async Task<IResult> AddPagos(PagosDTO pagosDTO)
        {
            AddPagosModel addPagosModel = new AddPagosModel();

            try
            {
                Payzi.Business.Pago pago = new Payzi.Business.Pago
                {
                    Id = Guid.NewGuid(),
                    IdTransaccion = pagosDTO.IdTransaccion,
                    UsuarioId = pagosDTO.UsuarioId
                };

                await pago.Save(this._context);
                await _context.SaveChangesAsync();

                addPagosModel.Success = true;
                addPagosModel.Code = StatusCodes.Status200OK;
                addPagosModel.Data = true;

                return Results.Ok(addPagosModel);
            }
            catch
            {
                addPagosModel.Success = false;
                addPagosModel.Code = StatusCodes.Status400BadRequest;
                addPagosModel.Data = false;

                return Results.BadRequest(addPagosModel);
            }
        }

        //Recibir pagos: La SuperAPI que realiza recepción de un pago, agregando transaccion, extradata, customId y su salida.
        /*DEBE TRAER TODA LA DATA DEL FRONT.*/
        public async Task<IResult> RecepcionPago(RecepcionPagosDTO recepcionPagosDTO)
        {
            RecepcionPagosModel recepcionPagosModel = new RecepcionPagosModel();

            try
            {
                //Voucher primero
                Payzi.Business.Voucher voucher = new Payzi.Business.Voucher
                {
                    Id = recepcionPagosDTO.Id_Voucher, //Numerico BigInt
                    NombreCliente = recepcionPagosDTO.NombreCliente, //Se obtendrá del pago? nullable
                    NumeroDocumento = recepcionPagosDTO.NumeroDocumento, //Nullable
                    Monto = recepcionPagosDTO.Monto,
                    FechaEmision = DateTime.Now,
                    Descripcion = recepcionPagosDTO.Descripcion, //Nullable
                    MetodoPagoCodigo = recepcionPagosDTO.MetodoPagoCodigo, // 0, 1, 2
                    NumeroTransaccion = recepcionPagosDTO.NumeroTransaccion,
                    UsuarioId = this.CurrentUser().Id, //Debe ser RUT
                    Estado = recepcionPagosDTO.Estado //True False
                };

                await voucher.Save(this._context);

                //Inicia con CustomFields
                Payzi.Business.CustomFields customFields = new Payzi.Business.CustomFields
                {
                    IdCustomFields = Guid.NewGuid(),
                    Name = recepcionPagosDTO.Name,
                    Value = recepcionPagosDTO.Value,
                    Print = recepcionPagosDTO.Print
                };

                //Agrega el detalle especificado
                Payzi.Business.ExtraData extraData = new Payzi.Business.ExtraData 
                {
                    Id = Guid.NewGuid(),
                    TaxIdnValidation = recepcionPagosDTO.TaxIdnValidation,
                    ExemptAmount = recepcionPagosDTO.ExemptAmount, //Monto del teclado
                    NetAmount = (long)((recepcionPagosDTO.ExemptAmount * 0.19m)) , //Monto IVA ingresado del teclado
                    SourceName = recepcionPagosDTO.SourceName,
                    SourceVersion = recepcionPagosDTO.SourceVersion,
                    CustomFields = customFields.IdCustomFields //ID del CustomFields anterior
                };

                long amount = (long)(extraData.NetAmount + extraData.ExemptAmount);

                //Modificable el valor mínimo.
                if(amount < 0 || amount > 99999999999)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);
                }
                if(recepcionPagosDTO.tip != -1 || recepcionPagosDTO.tip != 0 || recepcionPagosDTO.tip > 99999999999)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);
                }
                if (recepcionPagosDTO.cashback != -1 || recepcionPagosDTO.cashback != 0 || recepcionPagosDTO.cashback > 99999999999)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);
                }
                if (recepcionPagosDTO.method != 0 || recepcionPagosDTO.method != 1 || recepcionPagosDTO.method != 2)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);
                }
                if (recepcionPagosDTO.installmentsQuantity < -1 || recepcionPagosDTO.installmentsQuantity > 12)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);
                }
                if (recepcionPagosDTO.dteType != 0 || recepcionPagosDTO.dteType != 48 || recepcionPagosDTO.dteType != 33 || recepcionPagosDTO.dteType != 99)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);
                }

                Payzi.Business.Transaccion transaccion = new Payzi.Business.Transaccion
                {
                    IdTransaccion = Guid.NewGuid(),
                    Amount = amount, //No mas allá de 12 digitos.
                    Tip = recepcionPagosDTO.tip, //-1, 0, ... , 12 máximo.
                    Cashback = recepcionPagosDTO.cashback, //-1, 0, ... , 12 máximo.
                    Method = recepcionPagosDTO.method, //0,1,2
                    InstallmentsQuantity = recepcionPagosDTO.installmentsQuantity, //Cuotas, -1 = no, 0 = App Pago, n = numero cuotas.
                    PrintVoucherOnApp = recepcionPagosDTO.printVoucherOnApp, //true o false.
                    DteType = recepcionPagosDTO.dteType, //0, 48, 33 o 99
                    ExtraData = extraData.Id,
                    ExtraDataNavigation = extraData, //Debe entregar la data completa del Id ExtraData
                    VoucherId = voucher.Id,                    
                    Voucher = voucher //Debería entregar la data del voucher? modificable
                };


                if (recepcionPagosDTO.SequenceNumber.Length != 12)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);

                }

                Payzi.Business.TransaccionSalida transaccionSalida = new Payzi.Business.TransaccionSalida 
                {
                    Id = Guid.NewGuid(),
                    TransactionStatus = recepcionPagosDTO.TransactionStatus, //True o false si la transacción se realizo correctamente.
                    SequenceNumber = recepcionPagosDTO.SequenceNumber, //12 digitos varchar max
                    PrinterVoucherCommerce = recepcionPagosDTO.PrinterVoucherCommerce,
                    ExtraData = recepcionPagosDTO.ExtraData,
                    TransactionTip = transaccion.Tip,
                    TransactionCashback = recepcionPagosDTO.printVoucherOnApp == true ? transaccion.Cashback : transaccion.Cashback, //Debe ser dependiente de PrintVoucherOnApp
                };

                Payzi.Business.Pago pago = new Payzi.Business.Pago
                {
                    Id = Guid.NewGuid(),
                    IdTransaccion = recepcionPagosDTO.idTransaccion,
                    UsuarioId = this.CurrentUser().Id,

                };

                await _context.SaveChangesAsync();


                recepcionPagosModel.Success = true;
                recepcionPagosModel.Code = StatusCodes.Status200OK;
                recepcionPagosModel.Data = recepcionPagosDTO;

                return Results.Ok(recepcionPagosModel);
            }
            catch
            {
                recepcionPagosModel.Success = false;
                recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                recepcionPagosModel.Data = null;

                return Results.BadRequest(recepcionPagosModel);
            }

        }
    }
}
