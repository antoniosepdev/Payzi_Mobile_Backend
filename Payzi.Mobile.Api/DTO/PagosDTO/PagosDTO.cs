namespace Payzi.Mobile.Api.DTO.PagosDTO
{
    public class PagosDTO
    {
        public Guid IdPago { get; set; } = Guid.Empty;

        public Guid IdTransaccion { get; set; } = Guid.Empty;

        public Guid IdUsuario { get; set; } = Guid.Empty;
    }
}
