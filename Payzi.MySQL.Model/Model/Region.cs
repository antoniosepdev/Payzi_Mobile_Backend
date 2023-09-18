using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

public partial class Region
{
    public int Codigo { get; set; }

    public int PaisCodigo { get; set; }

    public string? NombreSigla { get; set; }

    public string? NombreOficial { get; set; }

    public virtual ICollection<Ciudad> Ciudads { get; set; } = new List<Ciudad>();

    public virtual Pai PaisCodigoNavigation { get; set; } = null!;
}
