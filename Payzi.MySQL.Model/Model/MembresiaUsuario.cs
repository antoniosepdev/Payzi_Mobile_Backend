using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

/// <summary>
/// Tabla usuarios registrados
/// </summary>
public partial class MembresiaUsuario
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Email { get; set; }

    public string? Clave { get; set; }

    public bool Bloqueo { get; set; }

    public int TipoUsuarioCodigo { get; set; }

    public virtual DboPersona IdNavigation { get; set; } = null!;

    public virtual MembresiaTipousuario TipoUsuarioCodigoNavigation { get; set; } = null!;
}
