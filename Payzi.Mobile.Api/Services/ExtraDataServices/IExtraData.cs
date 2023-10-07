using Payzi.Mobile.Api.DTO.ExtraDataDTO;

namespace Payzi.Mobile.Api.Services.ExtraDataServices
{
    public interface IExtraData
    {
        Task<IResult> AddExtraData(ExtraDataDTO extraDataDTO);
    }
}
