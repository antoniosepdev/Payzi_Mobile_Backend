using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.CustomFieldsDTO;
using Payzi.Mobile.Api.DTO.ExtraDataDTO;
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
        private HttpContext _httpContext;

        private Payzi.Context.Context _context;


        public TransaccionController(HttpContext httpContext, Payzi.Context.Context context)
            : base(httpContext, context)
        {
            _httpContext = httpContext;

            _context = context;
        }

        public async Task<IResult> GetTransaccion(TransaccionDTO transaccionDTO)
        {
            GetTransaccionModel getTransaccionModel = new GetTransaccionModel();

            getTransaccionModel.Success = true;

            try
            {
                Payzi.Business.Transaccion transaccion = await Payzi.Business.Transaccion.GetAsync(this._context, transaccionDTO.idTransaccion);

                Payzi.Business.ExtraData extraData = await Payzi.Business.ExtraData.GetAsync(this._context, transaccion.ExtraData);

                Payzi.Business.Pago pago = await Payzi.Business.Pago.GetAsync(this._context, transaccion);

                Payzi.Business.Usuario usuario = await Payzi.Business.Usuario.GetAsync(this._context, pago.IdUsuario);

                List<Payzi.Business.Pago> pagos = await Payzi.Business.Pago.GetAll(this._context, usuario);

                List<Payzi.Business.Transaccion> transaccions = await Payzi.Business.Transaccion.GetAll(this._context, transaccion);

                List<Payzi.Business.ExtraData> extraDatas = await Payzi.Business.ExtraData.GetAll(this._context, transaccion);

                List<Payzi.Business.CustomFields> customFields = await Payzi.Business.CustomFields.GetAll(this._context, extraData);

                List<CustomFields2DTO> customFieldsDTO2 = new List<CustomFields2DTO>();

                GetTransaccionDTO getTransaccionDTO = new GetTransaccionDTO();

                ExtraDataDTO2 extraDataDTO2 = new ExtraDataDTO2();

                foreach (Payzi.Business.CustomFields item in customFields)
                {
                    if (item != null)
                    {
                        CustomFields2DTO customFields2DTO = new CustomFields2DTO
                        {
                            Name = item.Name,
                            Value = item.Value,
                            Print = item.Print
                        };
                        customFieldsDTO2.Add(customFields2DTO);
                    }
                }

                foreach (Payzi.Business.ExtraData item in extraDatas)
                {
                    if (item != null)
                    {
                        extraDataDTO2.TaxIdnValidation = item.TaxIdnValidation;
                        extraDataDTO2.ExemptAmount = item.ExemptAmount;
                        extraDataDTO2.NetAmount = item.NetAmount;
                        extraDataDTO2.SourceName = item.SourceName;
                        extraDataDTO2.SourceVersion = item.SourceVersion;
                        extraDataDTO2.CustomFields = customFieldsDTO2;
                    }
                }

                GetTransaccionDTO getTransaccionDTO2 = new GetTransaccionDTO
                {
                    amount = transaccion.Amount,
                    tip = transaccion.Tip,
                    cashback = transaccion.Cashback,
                    method = transaccion.Method,
                    installmentsQuantity = transaccion.InstallmentsQuantity,
                    printVoucherOnApp = transaccion.PrintVoucherOnApp,
                    dteType = transaccion.DteType,
                    extraData = extraDataDTO2
                };

                getTransaccionModel.Code = StatusCodes.Status200OK;
                getTransaccionModel.Status = "Ok";
                getTransaccionModel.Data = getTransaccionDTO2;

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

        public async Task<IResult> GetTransaccion2(Guid id)
        {
            GetTransaccionModel getTransaccionModel = new GetTransaccionModel();

            getTransaccionModel.Success = true;

            try
            {
                Payzi.Business.Transaccion transaccion = await Payzi.Business.Transaccion.GetAsync(this._context, id);

                Payzi.Business.ExtraData extraData = await Payzi.Business.ExtraData.GetAsync(this._context, transaccion.ExtraData);

                Payzi.Business.Pago pago = await Payzi.Business.Pago.GetAsync(this._context, transaccion);

                Payzi.Business.Usuario usuario = await Payzi.Business.Usuario.GetAsync(this._context, pago.IdUsuario);

                List<Payzi.Business.Pago> pagos = await Payzi.Business.Pago.GetAll(this._context, usuario);

                List<Payzi.Business.Transaccion> transaccions = await Payzi.Business.Transaccion.GetAll(this._context, transaccion);

                List<Payzi.Business.ExtraData> extraDatas = await Payzi.Business.ExtraData.GetAll(this._context, transaccion);

                List<Payzi.Business.CustomFields> customFields = await Payzi.Business.CustomFields.GetAll(this._context, extraData);

                List<CustomFields2DTO> customFieldsDTO2 = new List<CustomFields2DTO>();

                GetTransaccionDTO getTransaccionDTO = new GetTransaccionDTO();

                ExtraDataDTO2 extraDataDTO2 = new ExtraDataDTO2();

                foreach (Payzi.Business.CustomFields item in customFields)
                {
                    if (item != null)
                    {
                        CustomFields2DTO customFields2DTO = new CustomFields2DTO
                        {
                            Name = item.Name,
                            Value = item.Value,
                            Print = item.Print
                        };
                        customFieldsDTO2.Add(customFields2DTO);
                    }
                }

                foreach (Payzi.Business.ExtraData item in extraDatas)
                {
                    if (item != null)
                    {
                        extraDataDTO2.TaxIdnValidation = item.TaxIdnValidation;
                        extraDataDTO2.ExemptAmount = item.ExemptAmount;
                        extraDataDTO2.NetAmount = item.NetAmount;
                        extraDataDTO2.SourceName = item.SourceName;
                        extraDataDTO2.SourceVersion = item.SourceVersion;
                        extraDataDTO2.CustomFields = customFieldsDTO2;
                    }
                }

                GetTransaccionDTO getTransaccionDTO2 = new GetTransaccionDTO
                {
                    amount = transaccion.Amount,
                    tip = transaccion.Tip,
                    cashback = transaccion.Cashback,
                    method = transaccion.Method,
                    installmentsQuantity = transaccion.InstallmentsQuantity,
                    printVoucherOnApp = transaccion.PrintVoucherOnApp,
                    dteType = transaccion.DteType,
                    extraData = extraDataDTO2
                };

                getTransaccionModel.Code = StatusCodes.Status200OK;
                getTransaccionModel.Status = "Ok";
                getTransaccionModel.Data = getTransaccionDTO2;

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

        public async Task<IResult> GetAllTransaccion(string email)
        {
            GetTransaccionModel getTransaccionModel = new GetTransaccionModel();

            getTransaccionModel.Success = true;

            try
            {
                Payzi.Business.Usuario usuario = await Payzi.Business.Usuario.GetAsync(this._context, email);

                Payzi.Business.Pago pago = await Payzi.Business.Pago.GetAsync2(this._context, usuario.Id);

                List<Payzi.Business.Pago> pagos = await Payzi.Business.Pago.GetAll(this._context, usuario);

                Payzi.Business.Transaccion transaccion = await Payzi.Business.Transaccion.GetAsync(this._context, pago.IdTransaccion);

                Payzi.Business.ExtraData extraData = await Payzi.Business.ExtraData.GetAsync(this._context, transaccion.ExtraData);

                List<Payzi.Business.Transaccion> transaccions = await Payzi.Business.Transaccion.GetAll(this._context, transaccion);

                List<Payzi.Business.ExtraData> extraDatas = await Payzi.Business.ExtraData.GetAll(this._context, transaccion);

                List<Payzi.Business.CustomFields> customFields = await Payzi.Business.CustomFields.GetAll(this._context, extraData);

                List<CustomFields2DTO> customFieldsDTO2 = new List<CustomFields2DTO>();

                List<GetTransaccionDTO> listDTO = new List<GetTransaccionDTO>();

                ExtraDataDTO2 extraDataDTO2 = new ExtraDataDTO2();

                foreach (Payzi.Business.CustomFields item in customFields)
                {
                    if (item != null)
                    {
                        CustomFields2DTO customFields2DTO = new CustomFields2DTO
                        {
                            Name = item.Name,
                            Value = item.Value,
                            Print = item.Print
                        };
                        customFieldsDTO2.Add(customFields2DTO);
                    }
                }

                foreach (Payzi.Business.ExtraData item in extraDatas)
                {
                    if (item != null)
                    {
                        extraDataDTO2.TaxIdnValidation = item.TaxIdnValidation;
                        extraDataDTO2.ExemptAmount = item.ExemptAmount;
                        extraDataDTO2.NetAmount = item.NetAmount;
                        extraDataDTO2.SourceName = item.SourceName;
                        extraDataDTO2.SourceVersion = item.SourceVersion;
                        extraDataDTO2.CustomFields = customFieldsDTO2;
                    }
                }

                foreach (Payzi.Business.Transaccion item in transaccions)
                {
                    if (item != null)
                    {
                        GetTransaccionDTO getTransaccionDTO2 = new GetTransaccionDTO
                        {
                            amount = item.Amount,
                            tip = item.Tip,
                            cashback = item.Cashback,
                            method = item.Method,
                            installmentsQuantity = item.InstallmentsQuantity,
                            printVoucherOnApp = item.PrintVoucherOnApp,
                            dteType = item.DteType,
                            extraData = extraDataDTO2
                        };
                        listDTO.Add(getTransaccionDTO2);
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
