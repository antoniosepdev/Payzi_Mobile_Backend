namespace Payzi.Mobile.Api.DTO.PagosDTO
{
    public class RecepcionPagosDTO
    {
        public PagosDTO Pagos { get; set; }

        public TransaccionDTO.TransaccionDTO Transaccion { get; set; }

        public ExtraDataDTO.ExtraDataDTO ExtraData { get; set; }

        public CustomFieldsDTO.CustomFieldsDTO CustomFields { get; set; }

        public VoucherDTO.VoucherDTO Voucher { get; set; }

        public TransaccionSalidaDTO.TransaccionSalidaDTO TransaccionSalida { get; set; }
    }
}
