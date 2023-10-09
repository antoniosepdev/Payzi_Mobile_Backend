using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.CustomFieldsDTO;
using Payzi.Mobile.Api.DTO.ExtraDataDTO;
using Payzi.Mobile.Api.DTO.PagosDTO;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.DTO.TransaccionSalidaDTO;
using Payzi.Mobile.Api.DTO.VoucherDTO;
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
                    IdPago = Guid.NewGuid(),
                    IdTransaccion = pagosDTO.IdTransaccion,
                    IdUsuario = pagosDTO.IdUsuario
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

                #region CustomFields

                Payzi.Business.CustomFields customFields = new Payzi.Business.CustomFields
                {
                    IdCustomFields = Guid.NewGuid(),
                    Name = recepcionPagosDTO.CustomFields.Name,
                    Value = recepcionPagosDTO.CustomFields.Value,
                    Print = recepcionPagosDTO.CustomFields.Print
                };

                #endregion

                #region ExtraData

                Payzi.Business.ExtraData extraData = new Payzi.Business.ExtraData
                {
                    Id = Guid.NewGuid(),
                    TaxIdnValidation = recepcionPagosDTO.ExtraData.TaxIdnValidation, //Opcional, validación del rut negocio con sistema
                    ExemptAmount = recepcionPagosDTO.ExtraData.ExemptAmount, //Monto del teclado
                    NetAmount = (long)((recepcionPagosDTO.ExtraData.ExemptAmount * 0.19m)), //Monto IVA ingresado del teclado
                    SourceName = recepcionPagosDTO.ExtraData.SourceName,
                    SourceVersion = recepcionPagosDTO.ExtraData.SourceVersion,
                    CustomFields = customFields.IdCustomFields //ID del CustomFields anterior
                };

                long amount = (long)(extraData.NetAmount + extraData.ExemptAmount);

                #endregion

                #region Voucher

                Payzi.Business.Voucher voucher = new Payzi.Business.Voucher
                {
                    Id = recepcionPagosDTO.Voucher.Id, //Numerico BigInt
                    NombreCliente = recepcionPagosDTO.Voucher.NombreCliente, //Se obtendrá del pago? nullable
                    NumeroDocumento = recepcionPagosDTO.Voucher.NumeroDocumento, //Nullable
                    Monto = amount,
                    FechaEmision = DateTime.Now,
                    Descripcion = recepcionPagosDTO.Voucher.Descripcion, //Nullable
                    MetodoPagoCodigo = recepcionPagosDTO.Transaccion.method, // 0, 1, 2
                    NumeroTransaccion = recepcionPagosDTO.Voucher.NumeroTransaccion,
                    UsuarioId = this.CurrentUser().Id, //Debe ser RUT
                    Estado = recepcionPagosDTO.Voucher.Estado //True False
                };

                #endregion

                #region Transaccion

                if (amount < 0 || amount > 99999999999)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);
                }
                if (recepcionPagosDTO.Transaccion.tip < -1 || recepcionPagosDTO.Transaccion.tip > 99999999999)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);
                }
                if (recepcionPagosDTO.Transaccion.cashback < -1 || recepcionPagosDTO.Transaccion.cashback > 99999999999)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);
                }
                if (recepcionPagosDTO.Transaccion.method < 0 || recepcionPagosDTO.Transaccion.method > 2)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);
                }
                if (recepcionPagosDTO.Transaccion.installmentsQuantity < -1 || recepcionPagosDTO.Transaccion.installmentsQuantity > 12)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);
                }
                if (recepcionPagosDTO.Transaccion.dteType != 0 && recepcionPagosDTO.Transaccion.dteType != 48 && recepcionPagosDTO.Transaccion.dteType != 33 && recepcionPagosDTO.Transaccion.dteType != 99)
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
                    Tip = recepcionPagosDTO.Transaccion.tip, //-1, 0, ... , 12 máximo.
                    Cashback = recepcionPagosDTO.Transaccion.cashback, //-1, 0, ... , 12 máximo.
                    Method = recepcionPagosDTO.Transaccion.method, //0,1,2
                    InstallmentsQuantity = recepcionPagosDTO.Transaccion.installmentsQuantity, //Cuotas, -1 = no, 0 = App Pago, n = numero cuotas.
                    PrintVoucherOnApp = recepcionPagosDTO.Transaccion.printVoucherOnApp, //true o false.
                    DteType = recepcionPagosDTO.Transaccion.dteType, //0, 48, 33 o 99
                    ExtraData = extraData.Id,
                    ExtraDataNavigation = extraData, //Debe entregar la data completa del Id ExtraData
                    VoucherId = voucher.Id,
                    //Voucher = voucher //Debería entregar la data del voucher? modificable
                };

                #endregion

                #region TransaccionSalida

                if (recepcionPagosDTO.TransaccionSalida.SequenceNumber.Length != 12)
                {
                    recepcionPagosModel.Success = false;
                    recepcionPagosModel.Code = StatusCodes.Status400BadRequest;
                    recepcionPagosModel.Data = null;

                    return Results.BadRequest(recepcionPagosModel);

                }

                Payzi.Business.TransaccionSalida transaccionSalida = new Payzi.Business.TransaccionSalida
                {
                    Id = Guid.NewGuid(),
                    TransactionStatus = recepcionPagosDTO.TransaccionSalida.TransactionStatus, //True o false si la transacción se realizo correctamente.
                    SequenceNumber = recepcionPagosDTO.TransaccionSalida.SequenceNumber, //12 digitos varchar max
                    PrinterVoucherCommerce = recepcionPagosDTO.TransaccionSalida.PrinterVoucherCommerce,
                    ExtraData = extraData.Id,
                    TransactionTip = transaccion.Tip,
                    TransactionCashback = transaccion.PrintVoucherOnApp == true ? transaccion.Cashback : transaccion.Cashback, //Debe ser dependiente de PrintVoucherOnApp
                };

                #endregion

                #region Pago

                Payzi.Business.Pago pago = new Payzi.Business.Pago
                {
                    IdPago = Guid.NewGuid(),
                    IdTransaccion = transaccion.IdTransaccion,
                    IdUsuario = this.CurrentUser().Id,

                };

                #endregion

                #region Save

                await customFields.Save(this._context);

                await extraData.Save(this._context);

                await voucher.Save(this._context);

                await transaccion.Save(this._context);

                await transaccionSalida.Save(this._context);

                await pago.Save(this._context);

                await _context.SaveChangesAsync();

                #endregion

                recepcionPagosModel.Success = true;
                recepcionPagosModel.Code = StatusCodes.Status200OK;
                recepcionPagosModel.Data = null;

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
