namespace Payzi.Mobile.Api.DTO.PagosDTO
{
    public class RecepcionPagosDTO
    {
        public PagosDTO Pagos {  get; set; }

        public TransaccionDTO.TransaccionDTO Transaccion { get; set; }

        public ExtraDataDTO.ExtraDataDTO ExtraData { get; set; }

        public CustomFieldsDTO.CustomFieldsDTO CustomFields { get; set; }

        public VoucherDTO.VoucherDTO Voucher { get; set; }

        public TransaccionSalidaDTO.TransaccionSalidaDTO TransaccionSalida { get; set; }

    }

    ////Subs
    //public class SubPagosDTO
    //{
    //    public Guid Id { get; set; } = Guid.Empty;

    //    public Guid IdTransaccion { get; set; } = Guid.Empty;

    //    public Guid UsuarioId { get; set; } = Guid.Empty;
    //}

    //public class SubTransaccionDTO
    //{
    //    public Guid idTransaccion { get; set; } = Guid.Empty;

    //    public long amount { get; set; }

    //    public long tip { get; set; }

    //    public long cashback { get; set; }

    //    public int method { get; set; }

    //    public int installmentsQuantity { get; set; }

    //    public bool printVoucherOnApp { get; set; }

    //    public int dteType { get; set; }

    //    public Guid extraData { get; set; } = Guid.Empty;

    //    public long VoucherId { get; set; }
    //}

    //public class SubExtraDataDTO
    //{
    //    public Guid Id { get; set; } = Guid.Empty;

    //    public string TaxIdnValidation { get; set; } = string.Empty;

    //    public int ExemptAmount { get; set; }

    //    public int NetAmount { get; set; }

    //    public string SourceName { get; set; } = string.Empty;

    //    public string SourceVersion { get; set; } = string.Empty;

    //    public Guid CustomFields { get; set; } = Guid.Empty;
    //}

    //public class SubCustomFieldsDTO
    //{
    //    public Guid IdCustomFields { get; set; } = Guid.Empty;

    //    public string Name { get; set; } = string.Empty;

    //    public string Value { get; set; } = string.Empty;

    //    public bool Print { get; set; }
    //}

    //public class SubTransaccionSalidaDTO
    //{
    //    public Guid Id { get; set; } = Guid.Empty;

    //    public byte TransactionStatus { get; set; }

    //    public string SequenceNumber { get; set; } = string.Empty;

    //    public byte PrinterVoucherCommerce { get; set; }

    //    public Guid ExtraData { get; set; }

    //    public long TransactionTip { get; set; }

    //    public long TransactionCashBack { get; set; }
    //}

    //public class SubVoucherDTO
    //{
    //    public long Id { get; set; }

    //    public string NombreCliente { get; set; } = string.Empty;

    //    public string NumeroDocumento { get; set; } = string.Empty;

    //    public decimal Monto { get; set; }

    //    public DateTime FechaEmision { get; set; }

    //    public string Descripcion { get; set; } = string.Empty;

    //    public int MetodoPagoCodigo { get; set; }

    //    public string NumeroTransaccion { get; set; } = string.Empty;

    //    public Guid UsuarioId { get; set; } = Guid.Empty;

    //    public byte Estado { get; set; }
    //}


}
