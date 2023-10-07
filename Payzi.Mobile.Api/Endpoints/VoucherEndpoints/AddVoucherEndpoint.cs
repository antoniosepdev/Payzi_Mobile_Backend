using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payzi.Mobile.Api.Controllers.VoucherControllers;
using Payzi.Mobile.Api.DTO.TransaccionDTO;
using Payzi.Mobile.Api.DTO.VoucherDTO;
using Payzi.Mobile.Api.Models.TransaccionModels;

namespace Payzi.Mobile.Api.Endpoints.VoucherEndpoints
{
    public class AddVoucherEndpoint : IEndpoint
    {
        public IEndpointRouteBuilder AddRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/Voucher/Add", [AllowAnonymous] async (HttpContext httpContext, Payzi.Context.Context context, [FromBody] VoucherDTO voucherDTO) =>
            {
                VoucherController voucherController = new VoucherController(httpContext, context);

                return await voucherController.AddVoucher(voucherDTO);

            }).Produces<AddTransaccionModel>(StatusCodes.Status200OK)
              .Produces<AddTransaccionModel>(StatusCodes.Status400BadRequest)
              .Produces<AddTransaccionModel>(StatusCodes.Status401Unauthorized);

            return endpoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            throw new NotImplementedException();
        }
    }
}
