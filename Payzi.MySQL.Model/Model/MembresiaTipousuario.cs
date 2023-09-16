using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

/// <summary>
/// Tipo de cuenta que podrá elegir el cliente a pagar? tabla en espera de respuesta.
/// </summary>
public partial class MembresiaTipousuario
{
    public int Codigo { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<MembresiaUsuario> MembresiaUsuarios { get; set; } = new List<MembresiaUsuario>();
}
