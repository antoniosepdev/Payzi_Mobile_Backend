namespace Payzi.Mobile.Api.DTO.TransaccionSalidaDTO
{
    public class TransaccionSalidaDTO
    {
        public Guid Id { get; set; } = Guid.Empty;

        public bool TransactionStatus { get; set; }

        public string SequenceNumber { get; set; } = string.Empty;

        public bool PrinterVoucherCommerce { get; set; }

        public Guid ExtraData {  get; set; }

        public long TransactionTip {  get; set; }

        public long TransactionCashBack { get; set; }
    }
}
