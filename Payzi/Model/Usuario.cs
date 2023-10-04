using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Usuario
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public bool Aprobado { get; set; }

    public bool Bloqueado { get; set; }

    public int RolCodigo { get; set; }

    public DateTime Creacion { get; set; }

    public DateTime? UltimoAcceso { get; set; }

    public DateTime? UltimoCambioPassword { get; set; }

    public DateTime? FechaIntentoFallido { get; set; }

    public virtual Negocio? Negocio { get; set; }

    public virtual Rol RolCodigoNavigation { get; set; } = null!;
}
