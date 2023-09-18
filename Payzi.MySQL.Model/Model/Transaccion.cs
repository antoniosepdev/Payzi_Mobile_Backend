using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

/// <summary>
/// Transaccion es la boleta a facturar.
/// </summary>
public partial class Transaccion
{
    public Guid IdTransaccion { get; set; }

    public long Amount { get; set; }

    public long Tip { get; set; }

    public long Cashback { get; set; }

    public int Method { get; set; }

    public int InstallmentsQuantity { get; set; }

    public sbyte PrintVoucherOnApp { get; set; }

    public int DteType { get; set; }

    public Guid TransaccionDetallesId { get; set; }

    public long VoucherId { get; set; }

    public virtual Formapago MethodNavigation { get; set; } = null!;

    public virtual Transacciondetalle TransaccionDetalles { get; set; } = null!;

    public virtual Voucher Voucher { get; set; } = null!;
}
