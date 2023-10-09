using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Pago
{
    public Guid IdPago { get; set; }

    public Guid IdTransaccion { get; set; }

    public Guid IdUsuario { get; set; }

    public virtual Transaccion IdTransaccionNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
