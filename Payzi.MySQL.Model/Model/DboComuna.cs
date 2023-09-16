using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model.Model;

public partial class DboComuna
{
    public int Codigo { get; set; }

    public int CiudadCodigo { get; set; }

    public int RegionCodigo { get; set; }

    public int PaisCodigo { get; set; }

    public string Nombre { get; set; } = null!;

    public int? CodigoPostal { get; set; }

    public virtual DboCiudad DboCiudad { get; set; } = null!;

    public virtual ICollection<DboPersona> DboPersonas { get; set; } = new List<DboPersona>();
}
