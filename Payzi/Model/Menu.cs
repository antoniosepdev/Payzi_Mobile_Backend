using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Menu
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    public virtual ICollection<Rol> Rols { get; set; } = new List<Rol>();
}
