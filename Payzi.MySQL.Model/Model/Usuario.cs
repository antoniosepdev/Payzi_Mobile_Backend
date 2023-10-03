using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

/// <summary>
/// Tabla usuarios registrados
/// </summary>
public partial class Usuario
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public string? Clave { get; set; }

    public bool Aprobado { get; set; }

    public bool Bloqueado { get; set; }

    public int RolCodigo { get; set; }

    public DateTime? Creacion { get; set; }

    public DateTime? UltimoAcceso { get; set; }

    public DateTime? UltimoCambioPassword { get; set; }

    public DateTime? FechaIntentoFallido { get; set; }

    public virtual Negocio IdNavigation { get; set; } = null!;

    public virtual Rol RolCodigoNavigation { get; set; } = null!;

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
