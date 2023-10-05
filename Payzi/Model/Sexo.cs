using System;
using System.Collections.Generic;

namespace Payzi.Model;

public partial class Sexo
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
