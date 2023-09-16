using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model.Model;

public partial class DboCiudad
{
    public int Codigo { get; set; }

    public int RegionCodigo { get; set; }

    public int PaisCodigo { get; set; }

    public string? NombreCiudad { get; set; }

    public virtual ICollection<DboComuna> DboComunas { get; set; } = new List<DboComuna>();

    public virtual DboRegion DboRegion { get; set; } = null!;
}
