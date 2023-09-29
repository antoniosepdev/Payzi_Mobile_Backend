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

    public bool Bloqueo { get; set; }

    public int RolCodigo { get; set; }

    public virtual Negocio IdNavigation { get; set; } = null!;

    public virtual Rol RolCodigoNavigation { get; set; } = null!;

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
