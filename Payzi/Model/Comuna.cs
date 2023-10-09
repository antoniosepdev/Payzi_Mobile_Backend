﻿using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Comuna
{
    public int Codigo { get; set; }

    public int PaisCodigo { get; set; }

    public int RegionCodigo { get; set; }

    public int CiudadCodigo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual Ciudad Ciudad { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
