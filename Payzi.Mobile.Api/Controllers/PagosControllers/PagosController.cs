using Mapster;
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
        //PD: Debe de traer la data del front desde el DTO.
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

                //Condicionales y manejo de errores
                #region Condicionales
                if (recepcionPagosDTO.Transaccion.cashback < -1 || recepcionPagosDTO.Transaccion.cashback > 99999999999)
                {
                    recepcionPagosModel.errorCode = "P-01";
                    recepcionPagosModel.errorMessage = "El dispositivo no admite un monto de vuelto de más de 12 dígitos.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                if (recepcionPagosDTO.Transaccion.installmentsQuantity < -1 || recepcionPagosDTO.Transaccion.installmentsQuantity > 12)
                {
                    recepcionPagosModel.errorCode = "P-02";
                    recepcionPagosModel.errorMessage = "El dispositivo no admite un monto de camtidad de cuotas de más de 12 dígitos.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(1) La app de pago no soporta propina de acuerdo a sus configuraciones.
                if (recepcionPagosDTO.Transaccion.tip < -1 || recepcionPagosDTO.Transaccion.tip > 99999999999)
                {
                    recepcionPagosModel.errorCode = "1";
                    recepcionPagosModel.errorMessage = "La app de Pago no soporta propina de acuerdo a sus configuraciones.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(2) Si el JSON recibido tiene un valor mayor a 0 en el atributo cashback y el Terminal tiene desactivada la opción de vuelto en su configuración establecida en su espacio de trabajo.
                if (recepcionPagosDTO.Transaccion.tip == 0 && recepcionPagosDTO.TransaccionSalida.PrinterVoucherCommerce == false)
                {
                    recepcionPagosModel.errorCode = "2";
                    recepcionPagosModel.errorMessage = "La app de Pago no soporta vuelto de acuerdo a sus configuraciones.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(3) El atributo method del JSON recibido en la aplicación no tiene un valor válido.
                if (recepcionPagosDTO.Transaccion.method < 0 || recepcionPagosDTO.Transaccion.method > 2)
                {
                    recepcionPagosModel.errorCode = "3";
                    recepcionPagosModel.errorMessage = "El método de pago no esta definido en las configuraciones.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(4) Si installmentsQuantity tiene un valor superior a -1 cuando el método seleccionado no sea crédito.
                if (recepcionPagosDTO.Transaccion.installmentsQuantity > -1 && recepcionPagosDTO.Transaccion.method != 1)
                {
                    recepcionPagosModel.errorCode = "4";
                    recepcionPagosModel.errorMessage = "El dispositivo no admite cuotas en este tipo de transacción.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(5) Cuando cashback tenga un valor superior a -1 y el método de transacción elegido no sea débito.
                if (recepcionPagosDTO.Transaccion.cashback > -1 && recepcionPagosDTO.Transaccion.method != 2)
                {
                    recepcionPagosModel.errorCode = "5";
                    recepcionPagosModel.errorMessage = "El dispositivo no admite vuelto en este tipo de transacción.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(6) En caso que el atributo amount tenga un valor inferior a 0 en el JSON recibido o no viene especificado.
                if (recepcionPagosDTO.Transaccion.amount < 0 || recepcionPagosDTO.Transaccion.amount == null)
                {
                    recepcionPagosModel.errorCode = "6";
                    recepcionPagosModel.errorMessage = "El monto no fue especificado.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(7) En caso de que el atributo amount tenga un valor superior al límite establecido en las configuraciones del Terminal.
                if (recepcionPagosDTO.Transaccion.amount > 999999999999 && recepcionPagosDTO.Transaccion.tip != 0)
                {
                    recepcionPagosModel.errorCode = "7";
                    recepcionPagosModel.errorMessage = "El monto excede el máximo permitido.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(8) En caso de que algún atributo no haya sido añadido en el JSON.
                if(customFields.Print == null || extraData == null || transaccion == null || transaccionSalida == null || voucher == null ||
                    transaccion.Amount == null || transaccion.Tip == null || transaccion.Cashback == null || transaccion.Method == null || transaccion.InstallmentsQuantity == null || transaccion.InstallmentsQuantity == null || transaccion.PrintVoucherOnApp == null || transaccion.DteType == null ||
                    transaccionSalida.TransactionStatus == null || transaccionSalida.SequenceNumber == null || transaccionSalida.PrinterVoucherCommerce == null || transaccionSalida.TransactionTip == null || transaccionSalida.TransactionCashback == null)
                {
                    recepcionPagosModel.errorCode = "8";
                    recepcionPagosModel.errorMessage = "No todos los atributos requeridos están presentes.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(9) Error ocurrido durante el proceso de pago en la aplicación.
                /*PENDIENTE*/

                //(10) En caso de que durante el proceso de la transacción esta haya sido cancelada por el usuario.
                /*PENDIENTE*/

                //(11) El Terminal aun no ha sido correctamente configurado, esto puede deberse a que aún no ha sido agregado y activado en el espacio de trabajo del usuario,
                //o que el dispositivo no haya sido aprobado desde Backoffice para su uso, o que el dispositivo haya sido previamente deshabilitado.
                /*PENDIENTE*/

                //(12) El Terminal fue aprobado a través de los ejecutivos de Back Office, sin embargo aún no ha podido descargar y almacenar los certificados y llaves necesarias para su correcto funcionamiento.
                //Es necesario abrir la aplicación para que se lleve a cabo este procedimiento y posteriormente poder hacer uso de la aplicación.
                /*PENDIENTE*/

                //(13) El atributo amount que fue enviado excede el límite de 12 dígitos establecido para el correcto funcionamiento de la aplicación.
                if (recepcionPagosDTO.Transaccion.amount > 99999999999)
                {
                    recepcionPagosModel.errorCode = "13";
                    recepcionPagosModel.errorMessage = "El dispositivo no admite un monto de más de 12 dígitos.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(14) El Terminal actualmente no esta conectado a alguna red o la red a la cual esta conectado no presenta salida a internet, por lo tanto no es posible cargar las configuraciones.
                /*PENDIENTE*/

                //(15) El Terminal está conectado a internet, sin embargo no se pudo establecer conexión con la cuenta de pago correctamente.
                /*PENDIENTE*/

                //(16) El canal de pago asociado al POS aun espera su asignación.

                //(17) El canal de pago asociado al Terminal experimentó algún error durante su asignación.

                //(18) En caso de que el atributo dteType traiga un valor que no esta registrado como un tipo válido.
                if (recepcionPagosDTO.Transaccion.dteType != 0 && recepcionPagosDTO.Transaccion.dteType != 48 && recepcionPagosDTO.Transaccion.dteType != 33 && recepcionPagosDTO.Transaccion.dteType != 99)
                {
                    recepcionPagosModel.errorCode = "18";
                    recepcionPagosModel.errorMessage = "El tipo de documento electrónico no esta definido en las configuraciones.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(19) Ocurre cuando se envía un RUT en el campo de validación y este no coincide con el utilizado para habilitar la aplicación de Pagos
                if (recepcionPagosDTO.ExtraData.TaxIdnValidation != null && recepcionPagosDTO.ExtraData.TaxIdnValidation != this.CurrentCommerce.Rut)
                {
                    recepcionPagosModel.errorCode = "19";
                    recepcionPagosModel.errorMessage = "El RUT indicado no coincide con el utilizado.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(20) Ocurre cuando el SDK “SunmiPayHardwareService“ no esta instalado o en su defecto no cuenta con una versión igual o superior a 3.3.96.
                /*PENDIENTE*/

                //(21) Ocurre cuando la aplicación de pagos rechaza el intent ya que necesita ser actualizada.
                /*PENDIENTE*/

                //(I-01) Si no declara en cada uno de sus customFields “name” o “value“.
                if (recepcionPagosDTO.CustomFields.Name == null || recepcionPagosDTO.CustomFields.Value == null)
                {
                    recepcionPagosModel.errorCode = "I-01";
                    recepcionPagosModel.errorMessage = "Problemas al procesar campos requeridos.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(I-02) Si supera el máximo numero de caracteres por cada Customfield.
                if (recepcionPagosDTO.CustomFields.Name.Length <= 28 || recepcionPagosDTO.CustomFields.Value.Length <= 28)
                {
                    recepcionPagosModel.errorCode = "I-02";
                    recepcionPagosModel.errorMessage = "Los campos superan el máximo de caracteres.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(I-03) Si se incluye los caracteres reservados “&“ y “/“, en alguno de sus Customfield.
                if (recepcionPagosDTO.CustomFields.Name.Contains("/") && recepcionPagosDTO.CustomFields.Name.Contains("&")
                    && recepcionPagosDTO.CustomFields.Value.Contains("/") && recepcionPagosDTO.CustomFields.Value.Contains("&"))
                {
                    recepcionPagosModel.errorCode = "I-03";
                    recepcionPagosModel.errorMessage = "Formato no valido. Posee carácteres reservados del sistema.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }

                //(I-04) Si se ingresan caracteres en blanco tanto como en el "name" o "value".
                if (recepcionPagosDTO.CustomFields.Name == " " || recepcionPagosDTO.CustomFields.Value == " ")
                {
                    recepcionPagosModel.errorCode = "I-04";
                    recepcionPagosModel.errorMessage = "Los campos ingresados no son validos.";
                    recepcionPagosModel.errorCodeOnApp = StatusCodes.Status500InternalServerError;
                    recepcionPagosModel.errorMessageOnApp = "Internal Server Error";

                    return Results.BadRequest(recepcionPagosModel);
                }
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
                recepcionPagosModel.Code = StatusCodes.Status500InternalServerError;
                recepcionPagosModel.Data = null;

                return Results.BadRequest(recepcionPagosModel);
            }

        }
    }
}
