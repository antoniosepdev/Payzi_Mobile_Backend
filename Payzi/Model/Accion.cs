using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Accion
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public Guid MenuItemId { get; set; }

    public virtual MenuItem MenuItem { get; set; } = null!;

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
