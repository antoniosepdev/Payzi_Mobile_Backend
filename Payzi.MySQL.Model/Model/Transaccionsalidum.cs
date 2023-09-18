using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

/// <summary>
/// Parametros de salida después de una transacción.
/// </summary>
public partial class Transaccionsalidum
{
    public Guid Id { get; set; }

    public sbyte TransactionStatus { get; set; }

    public long SequenceNumber { get; set; }

    public sbyte PrinterVoucherCommerce { get; set; }

    public Guid ExtraData { get; set; }

    public long TransactionTip { get; set; }

    public long TransactionCashback { get; set; }

    public virtual Transacciondetalle ExtraDataNavigation { get; set; } = null!;
}
