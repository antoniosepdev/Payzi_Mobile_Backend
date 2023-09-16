using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

public partial class DboRegion
{
    public int Codigo { get; set; }

    public int PaisCodigo { get; set; }

    public string? NombreSigla { get; set; }

    public string? NombreOficial { get; set; }

    public virtual ICollection<DboCiudad> DboCiudads { get; set; } = new List<DboCiudad>();

    public virtual DboPai PaisCodigoNavigation { get; set; } = null!;
}
