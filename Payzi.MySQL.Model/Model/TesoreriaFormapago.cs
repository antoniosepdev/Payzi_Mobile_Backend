using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model.Model;

/// <summary>
/// Tabla de formas de pago que realizará el cliente.
/// </summary>
public partial class TesoreriaFormapago
{
    public bool Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<TesoreriaTransaccion> TesoreriaTransaccions { get; set; } = new List<TesoreriaTransaccion>();
}
