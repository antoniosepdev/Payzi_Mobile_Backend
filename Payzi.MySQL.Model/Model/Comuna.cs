using System;
using System.Collections.Generic;

namespace Payzi.MySQL.Model;

public partial class Comuna
{
    public int Codigo { get; set; }

    public int CiudadCodigo { get; set; }

    public int RegionCodigo { get; set; }

    public int PaisCodigo { get; set; }

    public string NombreComuna { get; set; } = null!;

    public int? CodigoPostal { get; set; }

    public virtual Ciudad Ciudad { get; set; } = null!;

    public virtual ICollection<Negocio> Negocios { get; set; } = new List<Negocio>();

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
