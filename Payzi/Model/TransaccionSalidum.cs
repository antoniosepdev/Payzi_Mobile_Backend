﻿using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class TransaccionSalidum
{
    public Guid Id { get; set; }

    public bool TransactionStatus { get; set; }

    public string SequenceNumber { get; set; } = null!;

    public bool PrinterVoucherCommerce { get; set; }

    public Guid ExtraData { get; set; }

    public long TransactionTip { get; set; }

    public long TransactionCashback { get; set; }

    public virtual ExtraDatum ExtraDataNavigation { get; set; } = null!;
}
