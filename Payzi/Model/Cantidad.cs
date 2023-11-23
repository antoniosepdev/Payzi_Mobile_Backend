using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Cantidad
{
    public Guid IdUsuario { get; set; }

    public long Cantidad1 { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
