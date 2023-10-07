﻿namespace Payzi.Mobile.Api.DTO.TransaccionSalidaDTO
{
    public class TransaccionSalidaDTO
    {
        public Guid Id { get; set; } = Guid.Empty;

        public byte TransactionStatus { get; set; }

        public long SequenceNumber { get; set; }

        public byte PrinterVoucherCommerce { get; set; }

        public Guid ExtraData {  get; set; }

        public long TransactionTip {  get; set; }

        public long TransactionCashBack { get; set; }
    }
}