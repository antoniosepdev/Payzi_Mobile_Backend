using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Rol
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public Guid MenuId { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
