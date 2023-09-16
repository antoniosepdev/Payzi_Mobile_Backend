using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model.Model;

public partial class TesoreriaCustomfield
{
    public Guid IdCustomFields { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public virtual ICollection<TesoreriaTransacciondetalle> TesoreriaTransacciondetalles { get; set; } = new List<TesoreriaTransacciondetalle>();
}
