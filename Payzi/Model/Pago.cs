using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Pago
{
    public Guid Id { get; set; }

    public Guid IdTransaccion { get; set; }

    public Guid UsuarioId { get; set; }

    public virtual Transaccion IdTransaccionNavigation { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
