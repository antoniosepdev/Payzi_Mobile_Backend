using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Region
{
    public int PaisCodigo { get; set; }

    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public string NombreOficial { get; set; } = null!;

    public virtual ICollection<Ciudad> Ciudads { get; set; } = new List<Ciudad>();

    public virtual Pai PaisCodigoNavigation { get; set; } = null!;
}
