using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model.Model;

public partial class TesoreriaTransaccion
{
    public Guid IdTransaccion { get; set; }

    public long Amount { get; set; }

    public long Tip { get; set; }

    public long Cashback { get; set; }

    public bool Method { get; set; }

    public bool InstallmentsQuantity { get; set; }

    public bool PrintVoucherOnApp { get; set; }

    public int DteType { get; set; }

    public Guid TransaccionDetallesId { get; set; }

    public long VoucherId { get; set; }

    public virtual TesoreriaFormapago MethodNavigation { get; set; } = null!;

    public virtual TesoreriaTransacciondetalle TransaccionDetalles { get; set; } = null!;

    public virtual TesoreriaVoucher Voucher { get; set; } = null!;
}
