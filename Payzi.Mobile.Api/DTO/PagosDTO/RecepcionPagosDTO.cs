using Payzi.Model;

namespace Payzi.Mobile.Api.DTO.PagosDTO
{
    public class RecepcionPagosDTO
    {
        public Guid Id_Pago { get; set; } = Guid.Empty;

        public Guid UsuarioId { get; set; } = Guid.Empty;

        public Guid idTransaccion { get; set; } = Guid.Empty;

        //Modificable
        public long amount { get; set; }

        public long tip { get; set; }

        public long cashback { get; set; }

        public int method { get; set; }

        public int installmentsQuantity { get; set; }

        public bool printVoucherOnApp { get; set; }

        public int dteType { get; set; }

        public Guid extraData { get; set; } = Guid.Empty;

        public long VoucherId { get; set; }

        public Guid Id_TransaccionSalida { get; set; } = Guid.Empty;

        public byte TransactionStatus { get; set; }

        public string SequenceNumber { get; set; } = string.Empty;

        public byte PrinterVoucherCommerce { get; set; }

        //Prueba
        public Guid ExtraData { get; set; }


        public long TransactionTip { get; set; }

        public long TransactionCashBack { get; set; }

        public long Id_Voucher { get; set; }

        public string NombreCliente { get; set; } = string.Empty;

        public string NumeroDocumento { get; set; } = string.Empty;

        public decimal Monto { get; set; }

        public DateTime FechaEmision { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public int MetodoPagoCodigo { get; set; }

        public string NumeroTransaccion { get; set; } = string.Empty;

        public byte Estado { get; set; }

        public Guid IdCustomFields { get; set; } = Guid.Empty;

        public string Name { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public bool Print { get; set; }

        public Guid Id_ExtraData { get; set; } = Guid.Empty;

        public string TaxIdnValidation { get; set; } = string.Empty;

        public int ExemptAmount { get; set; }

        //Modificable
        public int NetAmount { get; set; }

        public string SourceName { get; set; } = string.Empty;

        public string SourceVersion { get; set; } = string.Empty;

        public Guid CustomFields { get; set; } = Guid.Empty;

        public virtual ExtraDatum ExtraDataNavigation { get; set; }

        public virtual Transaccion TransaccionNavigation {  get; set; }

        public virtual TransaccionSalidum TransaccionSalidumNavigation {  get; set; }

        public virtual CustomField CustomFieldNavigation { get; set; }

        public virtual Voucher VoucherNavigation { get; set; }
    }
}
