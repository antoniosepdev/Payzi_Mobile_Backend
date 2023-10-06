using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Pago
{
    public Guid Id { get; set; }

    public Guid TransferenciaId { get; set; }

    public virtual ICollection<Accion> Accions { get; set; } = new List<Accion>();

    public virtual Transaccion Transferencia { get; set; } = null!;
}
