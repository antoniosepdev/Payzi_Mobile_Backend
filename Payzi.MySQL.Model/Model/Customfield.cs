using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

/// <summary>
/// CustomFields es la información adicional de la transacción, opcional.
/// </summary>
public partial class Customfield
{
    public Guid IdCustomFields { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public virtual ICollection<Transacciondetalle> Transacciondetalles { get; set; } = new List<Transacciondetalle>();
}
