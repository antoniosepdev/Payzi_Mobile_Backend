using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

public partial class Menu
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public virtual ICollection<Menuitem> Menuitems { get; set; } = new List<Menuitem>();

    public virtual ICollection<Rol> Rols { get; set; } = new List<Rol>();
}
