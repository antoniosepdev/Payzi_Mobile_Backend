using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

public partial class Accion
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public Guid MenuItemId { get; set; }

    public virtual Menuitem MenuItem { get; set; } = null!;
}
