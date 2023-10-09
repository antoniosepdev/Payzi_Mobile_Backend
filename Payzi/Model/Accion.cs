using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Accion
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public Guid MenuItemId { get; set; }

    public Guid? ReferenciaId { get; set; }

    public virtual MenuItem? Referencia { get; set; }
}
