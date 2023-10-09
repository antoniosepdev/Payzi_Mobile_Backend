namespace Payzi.Mobile.Api.DTO.PagosDTO
{
    public class PagosDTO
    {
        public Guid Id { get; set; } = Guid.Empty;

        public Guid IdTransaccion { get; set; } = Guid.Empty;

        public Guid UsuarioId { get; set; } = Guid.Empty;
    }
}
