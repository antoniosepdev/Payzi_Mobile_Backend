using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class TransaccionSalidum
{
    public Guid Id { get; set; }

    public byte TransactionStatus { get; set; }

    public long SequenceNumber { get; set; }

    public byte PrinterVoucherCommerce { get; set; }

    public Guid ExtraData { get; set; }

    public long TransactionTip { get; set; }

    public long TransactionCashback { get; set; }

    public virtual ExtraDatum ExtraDataNavigation { get; set; } = null!;
}
