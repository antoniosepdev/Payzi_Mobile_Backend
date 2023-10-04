using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class FormaPago
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
