namespace Payzi.Mobile.Api.DTO.VoucherDTO
{
    public class VoucherDTO
    {
        public long Id { get; set; }

        public string NombreCliente { get; set; } = string.Empty;

        public string NumeroDocumento { get; set; } = string.Empty;

        public decimal Monto { get; set; }

        public DateTime FechaEmision { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public int MetodoPagoCodigo { get; set; }

        public string NumeroTransaccion { get; set; } = string.Empty;

        public Guid UsuarioId { get; set; } = Guid.Empty;

        public byte Estado { get; set; }
    }
}
