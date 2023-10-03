using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.DTO.LoginDTO;

namespace Payzi.Mobile.Api.Services.LoginServices
{
    public interface ILogin
    {
        Task<IResult> Login(LoginDTO loginDTO);
    }
}
