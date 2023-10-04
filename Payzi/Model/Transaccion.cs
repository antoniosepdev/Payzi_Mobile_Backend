using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Transaccion
{
    public Guid IdTransaccion { get; set; }

    public long Amount { get; set; }

    public long Tip { get; set; }

    public long Cashback { get; set; }

    public int Method { get; set; }

    public int InstallmentsQuantity { get; set; }

    public byte PrintVoucherOnApp { get; set; }

    public int DteType { get; set; }

    public Guid ExtraData { get; set; }

    public long VoucherId { get; set; }

    public int AccionCodigo { get; set; }

    public virtual Accion AccionCodigoNavigation { get; set; } = null!;

    public virtual ExtraDatum ExtraDataNavigation { get; set; } = null!;

    public virtual FormaPago MethodNavigation { get; set; } = null!;

    public virtual Voucher Voucher { get; set; } = null!;
}
