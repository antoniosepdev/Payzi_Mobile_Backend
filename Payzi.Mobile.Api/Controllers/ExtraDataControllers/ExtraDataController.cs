using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.ExtraDataDTO;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.Endpoints;
using Payzi.Mobile.Api.Models.ExtraDataModels;
using Payzi.Mobile.Api.Models.TransaccionModels;
using Payzi.Mobile.Api.Services.ExtraDataServices;

namespace Payzi.Mobile.Api.Controllers.ExtraDataControllers
{
    public class ExtraDataController : BaseController, IExtraData
    {

        private Payzi.Context.Context _context;

        public ExtraDataController(HttpContext httpContext, Payzi.Context.Context context)
            : base(httpContext, context)
        {
            _context = context;
        }

        public async Task<IResult> AddExtraData(ExtraDataDTO extraDataDTO)
        {
            AddExtraDataModel addExtraDataModel = new AddExtraDataModel();

            try
            {
                Payzi.Business.ExtraData extraData = new Payzi.Business.ExtraData
                {
                    Id = Guid.NewGuid(),
                    TaxIdnValidation = extraDataDTO.TaxIdnValidation,
                    ExemptAmount = extraDataDTO.ExemptAmount,
                    NetAmount = extraDataDTO.NetAmount,
                    SourceName = extraDataDTO.SourceName,
                    SourceVersion = extraDataDTO.SourceVersion,
                    CustomFields = extraDataDTO.CustomFields
                };

                await extraData.Save(this._context);
                await _context.SaveChangesAsync();

                addExtraDataModel.Success = true;
                addExtraDataModel.Code = StatusCodes.Status200OK;
                addExtraDataModel.Data = true;


                return Results.Ok(addExtraDataModel);
            }
            catch
            {


                return Results.BadRequest(addExtraDataModel);
            }
        }

    }
}
