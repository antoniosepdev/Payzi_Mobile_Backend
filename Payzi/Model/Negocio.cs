using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Negocio
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Rut { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public Guid DuenoId { get; set; }

    public int? PaisCodigo { get; set; }

    public int? RegionCodigo { get; set; }

    public int? CiudadCodigo { get; set; }

    public int? ComunaCodigo { get; set; }

    public virtual Comuna? Comuna { get; set; }

    public virtual Persona Dueno { get; set; } = null!;

    public virtual Usuario IdNavigation { get; set; } = null!;
}
