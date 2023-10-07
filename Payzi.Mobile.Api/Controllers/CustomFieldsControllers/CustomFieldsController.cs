using Payzi.Mobile.Api.Controllers.Common;
using Payzi.Mobile.Api.DTO.CustomFieldsDTO;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.Models.CustomFieldsModels;
using Payzi.Mobile.Api.Models.TransaccionModels;
using Payzi.Mobile.Api.Services.CustomFieldsServices;

namespace Payzi.Mobile.Api.Controllers.CustomFieldsControllers
{
    public class CustomFieldsController : BaseController, ICustomFields
    {
        private Payzi.Context.Context _context;

        public CustomFieldsController(HttpContext httpContext, Payzi.Context.Context context)
            : base(httpContext, context)
        {
            _context = context;
        }

        public async Task<IResult> AddCustomFields(CustomFieldsDTO customFieldsDTO)
        {
            AddCustomFieldsModel addCustomFieldsModel = new AddCustomFieldsModel();

            try
            {
                Payzi.Business.CustomFields customFields = new Payzi.Business.CustomFields
                {
                    IdCustomFields = Guid.NewGuid(),
                    Name = customFieldsDTO.Name,
                    Value = customFieldsDTO.Value,
                    Print = customFieldsDTO.Print
                };

                await customFields.Save(this._context);
                await _context.SaveChangesAsync();

                addCustomFieldsModel.Success = true;
                addCustomFieldsModel.Code = StatusCodes.Status200OK;
                addCustomFieldsModel.Data = true;

                return Results.Ok(addCustomFieldsModel);
            }
            catch
            {
                addCustomFieldsModel.Success = false;
                addCustomFieldsModel.Code = StatusCodes.Status400BadRequest;
                addCustomFieldsModel.Data = false;

                return Results.BadRequest(addCustomFieldsModel);
            }
        }

    }
}
