using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

/// <summary>
/// Menuitem son todas los items que puede traer un tipo especificado de menu.
/// </summary>
public partial class Menuitem
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Titulo { get; set; } = null!;

    public Guid MenuId { get; set; }

    public virtual ICollection<Accion> Accions { get; set; } = new List<Accion>();

    public virtual Menu Menu { get; set; } = null!;
}
