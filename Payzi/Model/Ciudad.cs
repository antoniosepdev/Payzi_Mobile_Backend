using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Ciudad
{
    public int PaisCodigo { get; set; }

    public int RegionCodigo { get; set; }

    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Comuna> Comunas { get; set; } = new List<Comuna>();

    public virtual Region Region { get; set; } = null!;
}
