using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class MenuItem
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Titulo { get; set; } = null!;

    public Guid MenuId { get; set; }

    public virtual ICollection<Accion> Accions { get; set; } = new List<Accion>();

    public virtual Menu Menu { get; set; } = null!;
}
