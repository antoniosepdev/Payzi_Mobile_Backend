using Payzi.Mobile.Api.DTO.CustomFieldsDTO;

namespace Payzi.Mobile.Api.Services.CustomFieldsServices
{
    public interface ICustomFields
    {
        Task<IResult> AddCustomFields(CustomFieldsDTO customFieldsDTO);
    }
}
